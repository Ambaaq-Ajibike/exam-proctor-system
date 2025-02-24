using exam_proctor_system.Models.Entities;

namespace exam_proctor_system.Models.DTos.ResponseModel
{
	public class QuestionModel
	{
		public string QuestionText { get; set; }
		public List<Option> Options { get; set; }
	}
	public class QuizModel
	{
		public Guid ExamId { get; set; }
		public string ExamName { get; set; }
		public List<QuestionModel> Questions { get; set; } = [];
		public int Duration { get; set; }
	}
}
