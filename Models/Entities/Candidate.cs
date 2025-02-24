namespace exam_proctor_system.Models.Entities
{
	public class Candidate : BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public User User { get; set; }
		public Guid UserId { get; set; }
	}
}
