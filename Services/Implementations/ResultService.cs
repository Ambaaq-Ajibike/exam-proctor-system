using exam_proctor_system.Models.DTos.RequestModel;
using exam_proctor_system.Models.DTos.ResponseModel;
using exam_proctor_system.Models.Entities;
using exam_proctor_system.Repositories;
using exam_proctor_system.Services.Interfaces;

namespace exam_proctor_system.Services.Implementations
{
	public class ResultService(IRepository<Result> _resultRepository, IRepository<Candidate> _candidateRepository, IRepository<Option> _optionRepository, IRepository<Exam> _examRepository, IHttpContextAccessor _httpContextAccessor) : IResultService
	{
		public async Task<BaseResponse<ResultResponse>> CalculateResult(GradeRequest gradeRequest)
		{
			var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
			if (userId == null)
			{
				return new BaseResponse<ResultResponse>
				{
					IsSuccess = false,
					Message = "User not found"
				};
			}
			var candidate = await _candidateRepository.FindAsync(c => c.UserId == new Guid(userId));
			if (candidate == null)
			{
				return new BaseResponse<ResultResponse>
				{
					IsSuccess = false,
					Message = "Candidate not found"
				};
			}
			int score = 0;
			var exam = await _examRepository.FindAsync(e => e.Id == gradeRequest.ExamId);
			foreach (var selectedOption in gradeRequest.SelectedOptions)
			{
				var option = await _optionRepository.FindAsync(o => o.Id == selectedOption.OptionId);
				if (option.IsCorrect)
				{
					score++;
				}
			}
			double scorePerQuestion = exam.TotalScore / (double)exam.NumberOfQuestions;
			double normalScore = score * scorePerQuestion;
			double percentage = (normalScore / exam.TotalScore) * 100;

			await _resultRepository.AddAsync(new Result
			{
				Percentage = percentage,
				Score = score,
				TotalScore = exam.TotalScore,
				CandidateId = candidate.Id,
				ExamId = gradeRequest.ExamId
			});
			await _resultRepository.SaveChangesAsync();
			return new BaseResponse<ResultResponse>
			{
				IsSuccess = true,
				Message = "Result calculated successfully",
				Data = new ResultResponse
				{
					TotalScore = exam.TotalScore,
					Score = normalScore,
					Percentage = percentage
				}
			};
		}
	}
}
