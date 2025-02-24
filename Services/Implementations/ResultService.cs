using exam_proctor_system.Models.DTos.RequestModel;
using exam_proctor_system.Models.DTos.ResponseModel;
using exam_proctor_system.Models.Entities;
using exam_proctor_system.Repositories;
using exam_proctor_system.Services.Interfaces;

namespace exam_proctor_system.Services.Implementations
{
	public class ResultService(IRepository<Result> _resultRepository, IRepository<Option> _optionRepository, IRepository<Exam> _examRepository) : IResultService
	{
		public async Task<ResultResponse> CalculateResult(GradeRequest gradeRequest)
		{
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
			double scorePerQuestion = exam.TotalScore / exam.NumberOfQuestions;
			double normalScore = score * scorePerQuestion;
			double percentage = (normalScore / exam.TotalScore) * 100;

			//await _resultRepository.AddAsync(new()
			//{
			//	Percentage = percentage,
			//	Score = score,
			//	TotalScore = exam.TotalScore,
			//	CandidateId = Guid.NewGuid(),
			//});
			//await _resultRepository.SaveChangesAsync();
			return new ResultResponse
			{
				TotalScore = exam.TotalScore,
				Score = normalScore,
				Percentage = percentage
			};
		}
	}
}
