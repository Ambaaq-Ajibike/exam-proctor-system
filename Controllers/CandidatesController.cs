using exam_proctor_system.Models.DTos.RequestModel;
using exam_proctor_system.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace exam_proctor_system.Controllers
{
	public class CandidatesController(ICandidateService _candidateService, IExamService _examService) : Controller
	{
		public async Task<IActionResult> Index()
		{
			var exams = await _examService.GetExamsAsync();
			ViewBag.Exams = exams;
			var candidates = await _candidateService.GetAllCandidatesAsync();
			return View(candidates);
		}

		public async Task<IActionResult> Exams(Guid candidateId)
		{
			var exams = await _examService.GetCandidateExams(candidateId);
			return View(exams);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateCandidateRequest request)
		{
			var response = await _candidateService.CreateCandidateAsync(request);
			if (!response.IsSuccess)
			{
				TempData["Message"] = response.Message;
				return View();
			}
			return RedirectToAction("Index");
		}
	}
}
