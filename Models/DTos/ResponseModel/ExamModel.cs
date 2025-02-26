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
	public class ExamResponseModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public string Status { get; set; }
		public int Duration { get; set; }
		public int Progress { get; set; }
	}
}
