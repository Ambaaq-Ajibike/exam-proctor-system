using exam_proctor_system.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace exam_proctor_system.Filters
{
	public class RoleAuthorizeAttribute(Role _role) : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var role = context.HttpContext.Session.GetString("Role");
			if (role == null || role != _role.ToString())
			{
				context.Result = new RedirectResult("/auth/login");
			}
		}
	}
}
