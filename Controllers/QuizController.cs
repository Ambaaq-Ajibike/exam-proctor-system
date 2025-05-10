using Azure;
using exam_proctor_system.Models.DTos.RequestModel;
using exam_proctor_system.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace exam_proctor_system.Controllers
{
	public class QuizController(IQuestionService _questionService, IResultService _resultService) : Controller
	{
		public async Task<IActionResult> Index(Guid examId)
		{
			var quiz = await _questionService.GetQuestionsAsync(examId);
			return View(quiz);
		}
		public async Task<IActionResult> Result(GradeRequest gradeRequest)
		{
			var result = await _resultService.CalculateResult(gradeRequest);
			if(!result.IsSuccess)
			{
				TempData["ResponseMessage"] = result.Message;
				return RedirectToAction("Index", new {examId = gradeRequest.ExamId});
			}
			return View(result.Data);
		}
		
	}
}
