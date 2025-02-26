using exam_proctor_system.Models.DTos.RequestModel;
using exam_proctor_system.Models.DTos.ResponseModel;
using exam_proctor_system.Models.Entities;

namespace exam_proctor_system.Services.Interfaces
{
	public interface ICandidateService
	{
		Task<BaseResponse<CandidateModel>> CreateCandidateAsync(CreateCandidateRequest request);
		Task<BaseResponse<CandidateModel>> GetCandidateByIdAsync(Guid id);
		Task<BaseResponse<CandidateModel>> UpdateCandidateAsync(CandidateModel candidateModel);
		Task<BaseResponse<CandidateModel>> DeleteCandidateAsync(Guid id);
		Task<IEnumerable<CandidateModel>> GetAllCandidatesAsync();
	}
}
