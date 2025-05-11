using exam_proctor_system.Models.DTos.ResponseModel;
using exam_proctor_system.Models.Entities;
using exam_proctor_system.Repositories;
using exam_proctor_system.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace exam_proctor_system.Services.Implementations
{
	public class DashboardService(IRepository<Exam> _examRepository) : IDashboardService
	{
		public async Task<DashboardDto> GetDashboardDataAsync()
		{
			var exams = await _examRepository.GetAllAsync(x => true);
			return new DashboardDto
			{
				Metrics =
				[
					new() {
						Key = "Total Exams",
						Value = exams.Count()
					},
					new() {
						Key = "Active Exams",
						Value = exams.Count(x => x.StartTime < DateTime.Now && x.EndTime > DateTime.Now)
					},
					new() {
						Key = "Inactive Exams",
						Value = exams.Count(x => x.StartTime > DateTime.Now || x.EndTime < DateTime.Now)
					},
					new() {
						Key = "Total Questions",
						Value = exams.Sum(x => x.NumberOfQuestions)
					}
				]
			};
		}
	}
}
