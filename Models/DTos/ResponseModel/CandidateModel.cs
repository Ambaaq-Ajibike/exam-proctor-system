namespace exam_proctor_system.Models.DTos.ResponseModel
{
	public class CandidateModel
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Pin { get; set; }
		public string Exams { get; set; }
	}
}
