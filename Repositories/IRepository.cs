using exam_proctor_system.Models.Entities;
using System.Linq.Expressions;

namespace exam_proctor_system.Repositories
{
	public interface IRepository<T> 
	{
		Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
		Task<T> FindAsync(Expression<Func<T, bool>> predicate);
		Task AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task RemoveAsync(T entity);
		Task<int> SaveChangesAsync();
	}
}
