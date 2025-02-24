namespace exam_proctor_system.Models.Entities
{
	public class User : BaseEntity
	{
		public string Email {  get; set; }
		public string Password { get; set; }
		public string FaceId { get; set; }
		public Role Role { get; set; }
		public Candidate Candidate { get; set; }
	}
	public enum Role
	{
		Admin,
		Candidate
	}
}
