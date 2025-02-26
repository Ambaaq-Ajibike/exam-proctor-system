using exam_proctor_system.Models.Entities;
using System.Linq.Expressions;

namespace exam_proctor_system.Repositories
{
	public interface ICandidateExamRepository
	{
		Task<IEnumerable<CandidateExam>> GetAllAsync(Expression<Func<CandidateExam, bool>> expression);
	}
}
