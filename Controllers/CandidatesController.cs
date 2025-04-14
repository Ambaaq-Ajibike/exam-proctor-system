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

		public async Task<IActionResult> Exams(Guid userId)
		{
			var exams = await _examService.GetCandidateExamsByUserId(userId);
			return View(exams);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateCandidateRequest request)
		{
			var response = await _candidateService.CreateCandidateAsync(request);
			TempData["ResponseMessage"] = response.Message;
			return RedirectToAction("Index");
		}
	}
}
