using exam_proctor_system.ApplicationContext;
using exam_proctor_system.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace exam_proctor_system.Repositories
{
	public class Repository<T>(ApplicationDbContext _context) : IRepository<T> where T : BaseEntity
	{
		public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
		{
			return await _context.Set<T>().Where(predicate).ToListAsync();
		}

		public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
		{
			return await _context.Set<T>().FirstOrDefaultAsync(predicate);
		}

		public async Task AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
		}
		public async Task AddRangeAsync(List<T> entity)
		{
			await _context.Set<T>().AddRangeAsync(entity);
		}

		public async Task UpdateAsync(T entity)
		{
			_context.Set<T>().Update(entity);
			await Task.CompletedTask;
		}

		public async Task RemoveAsync(T entity)
		{
			_context.Set<T>().Remove(entity);
			await Task.CompletedTask;
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
