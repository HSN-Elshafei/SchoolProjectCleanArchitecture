using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Bases;
using SchoolProject.Core.Features.ApplicationUsers.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	public class ApplicationUserController : AppControllerBase
	{
		[HttpPost(Router.ApplicationUser.Add)]
		public async Task<IActionResult> AddStudent([FromBody] AddApplicationUserCommand user)
		{
			var response = await Mediator.Send(user);
			return Result(response);
		}
	}
}
