namespace exam_proctor_system.Models.Entities
{
	public class BaseEntity
	{
		public Guid Id { get; set; } = Guid.NewGuid();
	}
}
