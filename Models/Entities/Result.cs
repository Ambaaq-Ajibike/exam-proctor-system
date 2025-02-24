namespace exam_proctor_system.Models.Entities
{
	public class Result : BaseEntity
	{
		public Candidate Candidate { get; set; }
		public Guid CandidateId { get; set; }
		public double Score { get; set; }
		public double TotalScore { get; set; }
		public double Percentage { get; set; }
	}
}
