using exam_proctor_system.Models.DTos.ResponseModel;
using exam_proctor_system.Models.Entities;
using exam_proctor_system.Repositories;
using exam_proctor_system.Services.Interfaces;

namespace exam_proctor_system.Services.Implementations
{
	public class QuestionService(IRepository<Question> _questionRepository, IRepository<Exam> _examRepository, IRepository<Option> _optionRepository) : IQuestionService
	{
		public async Task<BaseResponse<QuizModel>> GetQuestionsAsync(Guid examId)
		{
			var exam = await _examRepository.FindAsync(e => e.Id == examId);
			if(exam == null)
			{
				return new BaseResponse<QuizModel>
				{
					Data = null,
					Message = "Exam not found",
					IsSuccess = false
				};
			}
			var questionsFromDb = await _questionRepository.GetAllAsync(q => q.ExamId == examId);
			
			if (questionsFromDb == null || !questionsFromDb.Any())
			{
				return new BaseResponse<QuizModel>
				{
					Data = null,
					Message = "No questions found",
					IsSuccess = false
				};
			}
			var questions = await ShuffleQuestions(questionsFromDb.ToList(), exam.NumberOfQuestions);
			List<QuestionModel> questionModels = [];
			foreach (var item in questions)
			{
				var options = await _optionRepository.GetAllAsync(o => o.QuestionId == item.Id);
				questionModels.Add(new QuestionModel
				{
					QuestionText = item.QuestionText,
					Options = options.ToList()
				});
			}
			
			return new BaseResponse<QuizModel>
			{
				Data = new QuizModel
				{
					ExamName = exam.Name,
					Questions = questionModels,
					Duration = exam.Duration
				},
				Message = "Questions retrieved successfully",
				IsSuccess = true
			};
		}
		private async Task<IList<Question>> ShuffleQuestions(List<Question> questions, int number)
		{
			Random rnd = new();
			List<Question> shuffedQuestions = [];
			for (int i = 0; i < number; i++)
			{
				var randomValue = rnd.Next(0, questions.Count - 1);
				var question = questions[randomValue];

				shuffedQuestions.Add(question);
			}
			return shuffedQuestions;
		}
	}
}
