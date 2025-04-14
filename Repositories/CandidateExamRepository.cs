using exam_proctor_system.ApplicationContext;
using exam_proctor_system.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace exam_proctor_system.Repositories
{
	public class CandidateExamRepository(ApplicationDbContext _dbContext) : Repository<CandidateExam>(_dbContext), ICandidateExamRepository
	{
		public async Task<IEnumerable<CandidateExam>> GetAllAsync(Expression<Func<CandidateExam, bool>> expression)
		{
			return await _dbContext.CandidateExams.Include(x => x.Exam).Include(x => x.Candidate).ThenInclude(x => x.User).Where(expression).ToListAsync();
		}
	}
}
