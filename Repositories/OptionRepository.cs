using exam_proctor_system.ApplicationContext;
using exam_proctor_system.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace exam_proctor_system.Repositories
{
	public class OptionRepository(ApplicationDbContext _dbContext) : IOptionRepository
	{
		public async Task<Option> GetOptionAsync(Guid optionId)
		{
			return await _dbContext.Options.Include(x => x.Question).ThenInclude(x => x.Exam).FirstOrDefaultAsync(x => x.Id == optionId);
		}
	}
}
