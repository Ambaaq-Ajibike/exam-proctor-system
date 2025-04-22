using Azure.Core;
using exam_proctor_system.Models.DTos.RequestModel;
using exam_proctor_system.Models.DTos.ResponseModel;
using exam_proctor_system.Models.Entities;
using exam_proctor_system.Repositories;
using exam_proctor_system.Services.Interfaces;

namespace exam_proctor_system.Services.Implementations
{
	public class CandidateService(IRepository<CandidateExam> _repository, IRepository<Candidate> _candidateRepository,  ICandidateExamRepository _candidateExamRepository, IRepository<Exam> _examRepository, IEmailService _emailService) : ICandidateService
	{
		public async Task<BaseResponse<CandidateModel>> CreateCandidateAsync(CreateCandidateRequest request)
		{
			var random = new Random();
			var pin = random.Next(1000, 9999);
			var existingExam = await _candidateExamRepository.FindAsync(x => x.ExamId == request.ExamIds.FirstOrDefault() && x.Candidate.User.Email == request.Email);
			if (existingExam != null)
			{
				return new BaseResponse<CandidateModel>
				{
					IsSuccess = false,
					Message = "Candidate already exists for this exam"
				};
			}
			var candidate = new Candidate
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				User = new User
				{
					Email = request.Email,
					Password = $"{pin}",
					Role = Role.Candidate,
					FaceId = "empty"
				}
			};
			await _candidateRepository.AddAsync(candidate);
			var exams = new List<Exam>();
			var candidateExams = new List<CandidateExam>();
			foreach (var item in request.ExamIds)
			{
				var exam = await _examRepository.FindAsync(x => x.Id == item);
				if (exam == null)
				{
					continue;
				}
				candidateExams.Add(new CandidateExam
				{
					ExamId = exam.Id,
					CandidateId = candidate.Id,
				});
				exams.Add(exam);
			}
			await _candidateExamRepository.AddRangeAsync(candidateExams);
			await _repository.SaveChangesAsync();
			var name = $"{request.FirstName} {request.LastName}";
			var htmlContent = _emailService.BuildVerificationPasscodeEmail($"{pin}", name, exams);
			var subject = "SecureExam";
			await _emailService.SendEmailAsync(request.Email, name, subject, htmlContent);	
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

		public async Task<BaseResponse<CandidateModel>> UpdateCandidateAsync(UpdateCandidateRequest request)
		{
			var candidate = await _candidateExamRepository.GetCandidateAsync(x => x.Id == request.Id);
			candidate.FirstName = request.FirstName;
			candidate.LastName = request.LastName;
			candidate.User.Email = request.Email;

			await _candidateExamRepository.RemoveExamsFromCandidate(candidate.CandidateExams);
			var exams = new List<Exam>();
			var candidateExams = new List<CandidateExam>();
			foreach (var item in request.ExamIds)
			{
				var exam = await _examRepository.FindAsync(x => x.Id == item);
				if (exam == null)
				{
					continue;
				}
				candidateExams.Add(new CandidateExam
				{
					ExamId = exam.Id,
					CandidateId = candidate.Id,
				});
				exams.Add(exam);
			}
			await _candidateExamRepository.AddRangeAsync(candidateExams);
			await _candidateRepository.UpdateAsync(candidate);
			await _repository.SaveChangesAsync();
			var candidateName = $"{request.FirstName} {request.LastName}";
			var htmlContent = _emailService.BuildVerificationPasscodeEmail($"{candidate.User.Password}", candidateName, exams);
			var subject = "SecureExam - Update";
			await _emailService.SendEmailAsync(request.Email, candidateName, subject, htmlContent);
			return new BaseResponse<CandidateModel>
			{
				IsSuccess = true,
				Message = "Candidate updated successfully"
			};
		}
	}
}
