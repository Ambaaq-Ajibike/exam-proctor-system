using exam_proctor_system.Models.DTos.RequestModel;
using exam_proctor_system.Models.DTos.ResponseModel;

namespace exam_proctor_system.Services.Interfaces
{
	public interface IExamService
	{
		Task<BaseResponse<ExamModel>> CreateExamAsync(CreateExamRequestModel request);
		Task<BaseResponse<ExamModel>> GetExamAsync(int id);
		Task<IEnumerable<ExamResponseModel>> GetExamsAsync();
		Task<IEnumerable<ExamResponseModel>> GetCandidateExams(Guid candidateId);
		Task<BaseResponse<ExamModel>> UpdateExamAsync(int id, CreateExamRequestModel request);
		Task<BaseResponse<ExamModel>> DeleteExamAsync(int id);
	}
}
