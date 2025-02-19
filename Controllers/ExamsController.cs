using Microsoft.AspNetCore.Mvc;

namespace exam_proctor_system.Controllers
{
	public class ExamsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
