using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Bases;
using SchoolProject.Core.Features.ApplicationUsers.Commands.Models;
using SchoolProject.Core.Features.ApplicationUsers.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class ApplicationUserController : AppControllerBase
	{
		[HttpGet(Router.ApplicationUser.Paginated)]
		public async Task<IActionResult> GetUser([FromQuery] GetApplicationUserPaginatedListQuery query)
		{
			var response = await Mediator.Send(query);
			return Ok(response);
		}
		[HttpGet(Router.ApplicationUser.GetById)]
		public async Task<IActionResult> GetUserById([FromRoute] int id)
		{
			var response = await Mediator.Send(new GetApplicationUserByIdQuery() { Id = id });
			return Result(response);
		}
		[HttpPost(Router.ApplicationUser.Add)]
		public async Task<IActionResult> AddUser([FromBody] AddApplicationUserCommand user)
		{
			var response = await Mediator.Send(user);
			return Result(response);
		}
		[HttpPut(Router.ApplicationUser.Edit)]
		public async Task<IActionResult> EditUser([FromBody] EditApplicationUserCommand user)
		{
			var response = await Mediator.Send(user);
			return Result(response);
		}
		[HttpPut(Router.ApplicationUser.ChangePassword)]
		public async Task<IActionResult> ChangePasswordUser([FromBody] ChangePasswordApplicationUserCommand user)
		{
			var response = await Mediator.Send(user);
			return Result(response);
		}
		[HttpDelete(Router.ApplicationUser.Delete)]
		public async Task<IActionResult> DeleteUser([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteApplicationUserCommand() { Id = id });
			return Result(response);
		}
	}
}
