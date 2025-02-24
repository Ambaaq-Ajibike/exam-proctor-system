using exam_proctor_system.Models.DTos.ResponseModel;

namespace exam_proctor_system.Services.Interfaces
{
	public interface IQuestionService
	{
		public Task<BaseResponse<QuizModel>> GetQuestionsAsync(Guid examId);
	}
}
