using System.Diagnostics;
using exam_proctor_system.Filters;
using exam_proctor_system.Models;
using exam_proctor_system.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace exam_proctor_system.Controllers
{
	public class HomeController(ILogger<HomeController> logger) : Controller
	{
		private readonly ILogger<HomeController> _logger = logger;

		public IActionResult Index()
		{
			return View();
		}

		[RoleAuthorize(Role.Admin)]
		public IActionResult Dashboard()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
