using exam_proctor_system.Models.DTos.RequestModel;
using exam_proctor_system.Models.DTos.ResponseModel;
using exam_proctor_system.Models.Entities;
using exam_proctor_system.Repositories;
using exam_proctor_system.Services.Interfaces;
using Mapster;
using OfficeOpenXml;
namespace exam_proctor_system.Services.Implementations
{
	public class ExamService(IRepository<Exam> _examRepository, IRepository<Question> _questionRepository) : IExamService
	{
		public async Task<BaseResponse<ExamModel>> CreateExamAsync(CreateExamRequestModel request)
		{
			var examExists = await _examRepository.FindAsync(e => e.Name == request.Name);
			if (examExists != null)
			{
				return new BaseResponse<ExamModel>
				{
					IsSuccess = false,
					Message = "Exam with the same name already exists"
				};
			}
			else if (request.StartTime < DateTime.Now)
			{
				return new BaseResponse<ExamModel>
				{
					IsSuccess = false,
					Message = "Start time cannot be in the past"
				};
			}
			else if (request.EndTime < request.StartTime)
			{ 
				return new BaseResponse<ExamModel>
				{
					IsSuccess = false,
					Message = "End time cannot be before start time"
				};
			}

			var exam = request.Adapt<Exam>();

			await _examRepository.AddAsync(exam);
			var (success, message) = await ProcessQuestions(request.Questions, exam.Id);
			if (!success)
			{
				return new BaseResponse<ExamModel>
				{
					IsSuccess = false,
					Message = message
				};
			}
			await _examRepository.SaveChangesAsync();
			return new BaseResponse<ExamModel>
			{
				IsSuccess = true,
				Message = "Exam created successfully",
				Data = exam.Adapt<ExamModel>()
			};
		}
		private async Task<(bool, string)> ProcessQuestions(IFormFile file, Guid examId)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			using (var package = new ExcelPackage(file.OpenReadStream()))
			{
				var worksheet = package.Workbook.Worksheets[0]; // Assuming your data is in the first sheet

				var expectedColumns = new List<string> { "Question Text", "Option A", "Option B", "Option C", "Option D", "Correct Answer" };
				var actualColumns = Enumerable.Range(1, worksheet.Dimension.Columns)
											  .Select(col => worksheet.Cells[1, col].Value?.ToString())
											  .ToList();

				if (!expectedColumns.SequenceEqual(actualColumns))
				{
					return (false, "The file head is not whats expected");
				}
				int rowCount = worksheet.Dimension.Rows;

				for (int row = 2; row <= rowCount; row++)
				{
					if (worksheet.Cells[row, 1].Value != null && worksheet.Cells[row, 2].Value != null)
					{
						var correctAnswer = worksheet.Cells[row, 6].Value.ToString().ToLower();

						var question = new Question
						{
							QuestionText = worksheet.Cells[row, 1].Value.ToString(),
							ExamId = examId,
							Options =
							[
								 new Option { OptionText = worksheet.Cells[row, 2].Value.ToString(), IsCorrect = worksheet.Cells[row, 2].Value.ToString().ToLower() == correctAnswer },
								new Option { OptionText = worksheet.Cells[row, 3].Value.ToString(), IsCorrect = worksheet.Cells[row, 3].Value.ToString().ToLower() == correctAnswer },
								new Option { OptionText = worksheet.Cells[row, 4].Value.ToString(), IsCorrect = worksheet.Cells[row, 4].Value.ToString().ToLower() == correctAnswer },
								new Option { OptionText = worksheet.Cells[row, 5].Value.ToString(), IsCorrect = worksheet.Cells[row, 5].Value.ToString().ToLower() == correctAnswer }
							]
						};
						await _questionRepository.AddAsync(question);
					}

				}
			}

			return (true, "Upload successful");
		}


		public Task<BaseResponse<ExamModel>> DeleteExamAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResponse<ExamModel>> GetExamAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<ExamResponseModel>> GetExamsAsync()
		{
			var exams = await _examRepository.GetAllAsync(e => true);
			var now = DateTime.Now;
			var examsResponse = exams.Select(e => new ExamResponseModel
			{
				Id = e.Id,
				Name = e.Name,
				StartTime = e.StartTime.ToString("d MMM, yyyy h:mm tt"),
				EndTime = e.EndTime.ToString("d MMM, yyyy h:mm tt"),
				Status = e.StartTime < DateTime.Now && e.EndTime > DateTime.Now ? "Active" : "Inactive",
				Progress = e.StartTime > now ? 0 : // Exam not started
			  e.EndTime <= now ? 100 : // Exam ended
			  (int)(((now - e.StartTime).TotalMinutes / (e.EndTime - e.StartTime).TotalMinutes) * 100)
			});
			return examsResponse;
		}

		public Task<BaseResponse<ExamModel>> UpdateExamAsync(int id, CreateExamRequestModel request)
		{
			throw new NotImplementedException();
		}
	}
}
