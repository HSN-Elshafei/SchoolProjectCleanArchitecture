using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	public class AuthenticationController : AppControllerBase
	{
		[HttpPost(Router.Authentication.SignIn)]
		public async Task<IActionResult> SignIn([FromForm] SignInAuthenticationCommand command)
		{
			var response = await Mediator.Send(command);
			return Result(response);
		}
		[HttpPost(Router.Authentication.RefreshToken)]
		public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
		{
			var response = await Mediator.Send(command);
			return Result(response);
		}
		[HttpGet(Router.Authentication.ValidateToken)]
		public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
		{
			var response = await Mediator.Send(query);
			return Result(response);
		}
	}
}
