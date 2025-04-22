using exam_proctor_system.Models.Entities;
using System.Linq.Expressions;

namespace exam_proctor_system.Repositories
{
	public interface ICandidateExamRepository : IRepository<CandidateExam>
	{
		Task<IEnumerable<CandidateExam>> GetAllAsync(Expression<Func<CandidateExam, bool>> expression);
		Task<Candidate> GetCandidateAsync(Expression<Func<Candidate, bool>> expression);
		Task<Exam> GetExamAsync(Expression<Func<Exam, bool>> expression);
		Task RemoveExamsFromCandidate(List<CandidateExam> candidateExams);
		Task RemoveQuestionsFromExam(List<Question> questions);
	}
}
