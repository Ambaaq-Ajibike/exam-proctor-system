namespace exam_proctor_system.Models.Entities
{
	public class CandidateExam : BaseEntity
	{
		public Guid CandidateId { get; set; }
		public Candidate Candidate { get; set; }
		public Guid ExamId { get; set; }
		public Exam Exam { get; set; }
	}
}
