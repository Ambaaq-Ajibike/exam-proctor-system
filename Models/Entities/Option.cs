namespace exam_proctor_system.Models.Entities
{
	public class Option : BaseEntity
	{
		public string OptionText { get; set; }
		public bool IsCorrect { get; set; }
		public Guid QuestionId { get; set; }
		public Question Question { get; set; }
	}
}
