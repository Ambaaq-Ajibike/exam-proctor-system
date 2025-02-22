using exam_proctor_system.Models.DTos.RequestModel;
using exam_proctor_system.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace exam_proctor_system.Controllers
{
	public class ExamsController(IExamService _examService, IWebHostEnvironment _hostenv) : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create([FromForm]CreateExamRequestModel createExamRequest)
		{
			var response = await _examService.CreateExamAsync(createExamRequest);
			TempData["CreateExamResponseMessage"] = response.Message;
			return View("Index");
		}

		[HttpGet]
		public async Task<IActionResult> QuestionTemplateDownload()
		{
			var contentRoot = _hostenv.WebRootPath;
			var path = Path.Combine(contentRoot, "QuestionsTemplate.xlsx");
			return File(System.IO.File.OpenRead(path), contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName: $"ExamTemplate.xlsx");
		}
	}
}
