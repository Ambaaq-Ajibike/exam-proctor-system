namespace exam_proctor_system.Models.Entities
{
	public class Question : BaseEntity
	{
		public string QuestionText { get; set; }
		public List<Option> Options { get; set; }
		public int ExamId { get; set; }
		public Exam Exam { get; set; }
	}
}
