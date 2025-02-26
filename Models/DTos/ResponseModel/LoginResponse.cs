using exam_proctor_system.Models.Entities;

namespace exam_proctor_system.Models.DTos.ResponseModel
{
	public class LoginResponse
	{
		public Guid UserId { get; set; }
		public string Username { get; set; }
		public Role Role { get; set; }
	}
}
