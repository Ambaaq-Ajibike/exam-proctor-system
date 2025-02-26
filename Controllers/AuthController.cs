using exam_proctor_system.Models.DTos.RequestModel;
using exam_proctor_system.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace exam_proctor_system.Controllers
{
	public class AuthController(IAuthService _authService) : Controller
	{

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginRequest loginRequest)
		{
			var response = await _authService.LoginAsync(loginRequest);
			if (!response.IsSuccess)
			{
				TempData["LoginMessage"] = response.Message;
				return View();
			}
			HttpContext.Session.SetString("UserId", response.Data.UserId.ToString());
			HttpContext.Session.SetString("Username", response.Data.Username);
			HttpContext.Session.SetString("Role", response.Data.Role.ToString());
			if (response.Data.Role == Models.Entities.Role.Admin)
			{
				return RedirectToAction("Dashboard", "Home");
			}
			else
			{
				return RedirectToAction("Exams", "Candidates", new { candidateId = response.Data.UserId });

			}

		}
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}
	}
}
