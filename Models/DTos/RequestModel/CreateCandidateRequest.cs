namespace exam_proctor_system.Models.DTos.RequestModel
{
	public class CreateCandidateRequest
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Pin { get; set; }
		public List<Guid> ExamIds { get; set; } = [];
	}
}
