using exam_proctor_system.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace exam_proctor_system.Controllers
{
	public class QuizController(IQuestionService _questionService) : Controller
	{
		public async Task<IActionResult> Index(Guid examId)
		{
			var quiz = await _questionService.GetQuestionsAsync(examId);
			return View(quiz);
		}
	}
}
