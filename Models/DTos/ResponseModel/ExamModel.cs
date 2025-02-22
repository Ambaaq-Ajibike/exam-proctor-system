namespace exam_proctor_system.Models.DTos.ResponseModel
{
	public class ExamModel
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int Duration { get; set; }
		public int TotalScore { get; set; }
		public string NumberOfQuestions { get; set; }
	}
}
