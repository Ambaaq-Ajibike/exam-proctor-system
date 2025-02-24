using exam_proctor_system.Models.Entities;

namespace exam_proctor_system.Repositories
{
	public interface IOptionRepository
	{
		Task<Option> GetOptionAsync(Guid optionId);
	}
}
