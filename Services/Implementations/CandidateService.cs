using exam_proctor_system.Models.DTos.RequestModel;
using exam_proctor_system.Models.DTos.ResponseModel;
using exam_proctor_system.Models.Entities;
using exam_proctor_system.Repositories;
using exam_proctor_system.Services.Interfaces;

namespace exam_proctor_system.Services.Implementations
{
	public class CandidateService(IRepository<CandidateExam> _repository, IRepository<Candidate> _candidateRepository,  ICandidateExamRepository _candidateExamRepository, IRepository<Exam> _examRepository) : ICandidateService
	{
		public async Task<BaseResponse<CandidateModel>> CreateCandidateAsync(CreateCandidateRequest request)
		{
			var candidate = new Candidate
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				User = new User
				{
					Email = request.Email,
					Password = request.Pin,
					Role = Role.Candidate,
					FaceId = "empty"
				}
			};
			await _candidateRepository.AddAsync(candidate);
			foreach (var item in request.ExamIds)
			{
				var exam = await _examRepository.FindAsync(x => x.Id == item);
				if (exam == null)
				{
					continue;
				}
				var candidateExam = new CandidateExam
				{
					Exam = exam,
					ExamId = exam.Id,
					Candidate = candidate,
				};
				await _repository.AddAsync(candidateExam);
			}
			await _repository.SaveChangesAsync();
			return new BaseResponse<CandidateModel>
			{
				IsSuccess = true,
				Message = "Candidate created successfully"
			};
		}

		public Task<BaseResponse<CandidateModel>> DeleteCandidateAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<CandidateModel>> GetAllCandidatesAsync()
		{
			var candidatesExams = await _candidateExamRepository.GetAllAsync(x => true);
			var candidateModels = candidatesExams
								.GroupBy(x => x.CandidateId)
								.Select(g => new CandidateModel
								{
									Id = g.Key,
									FirstName = g.First().Candidate.FirstName,
									LastName = g.First().Candidate.LastName,
									Email = g.First().Candidate.User.Email,
									Pin = g.First().Candidate.User.Password,
									Exams = string.Join(", ", g.Select(e => e.Exam.Name)) // Get all exams for this candidate
								});

			return candidateModels;
		}

		public Task<BaseResponse<CandidateModel>> GetCandidateByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<BaseResponse<CandidateModel>> UpdateCandidateAsync(CandidateModel candidateModel)
		{
			throw new NotImplementedException();
		}
	}
}
