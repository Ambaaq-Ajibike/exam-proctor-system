namespace exam_proctor_system.Models.DTos.RequestModel
{
	public class GradeRequest
	{
		public Guid ExamId { get; set; }
		public List<SelectedOption> SelectedOptions { get; set; } = [];
	}
	public class  SelectedOption
	{
		public Guid OptionId { get; set; }
	}
}
