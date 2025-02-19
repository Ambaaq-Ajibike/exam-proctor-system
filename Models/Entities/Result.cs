namespace exam_proctor_system.Models.Entities
{
	public class Result : BaseEntity
	{
		public Candidate Candidate { get; set; }
		public int CandidateId { get; set; }
		public double Score { get; set; }
	}
}
