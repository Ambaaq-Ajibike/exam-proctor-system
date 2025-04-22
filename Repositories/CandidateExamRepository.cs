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
		
		public async Task<Candidate> GetCandidateAsync(Expression<Func<Candidate, bool>> expression)
		{
			return await _dbContext.Candidates.Include(x => x.User).Include(x => x.CandidateExams).SingleOrDefaultAsync(expression);
		}
		public async Task<Exam> GetExamAsync(Expression<Func<Exam, bool>> expression)
		{
			return await _dbContext.Exams.Include(x => x.Questions).SingleOrDefaultAsync(expression);
		}
		
		public async Task RemoveExamsFromCandidate(List<CandidateExam> candidateExams)
		{
			_dbContext.CandidateExams.RemoveRange(candidateExams);
		}
		
		public async Task RemoveQuestionsFromExam(List<Question> questions)
		{
			_dbContext.Questions.RemoveRange(questions);
		}
	}
}
