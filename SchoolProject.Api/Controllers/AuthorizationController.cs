using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class AuthorizationController : AppControllerBase
	{
		[HttpPost(Router.Authorization.AddRole)]
		public async Task<IActionResult> AddRole([FromForm] AddRoleCommand command)
		{
			var response = await Mediator.Send(command);
			return Result(response);
		}

		[HttpPost(Router.Authorization.EditRole)]
		public async Task<IActionResult> EditRole([FromForm] EditRoleCommand command)
		{
			var response = await Mediator.Send(command);
			return Result(response);
		}

		[HttpDelete(Router.Authorization.DeleteRole)]
		public async Task<IActionResult> DeleteRole([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteRoleCommand(id));
			return Result(response);
		}

		[HttpGet(Router.Authorization.GetRoleList)]
		public async Task<IActionResult> GetRolesList()
		{
			var response = await Mediator.Send(new GetRolesListQuery());
			return Result(response);
		}

		[HttpGet(Router.Authorization.GetRoleById)]
		public async Task<IActionResult> GetRoleById([FromRoute] int id)
		{
			var response = await Mediator.Send(new GetRoleByIdQuery() { RoleId = id });
			return Result(response);
		}

		[HttpGet(Router.Authorization.ManageUserRoles)]
		public async Task<IActionResult> ManageUserRoles([FromRoute] int userId)
		{
			var response = await Mediator.Send(new ManageUserRolesQuery() { UserId = userId });
			return Result(response);
		}

		[HttpPut(Router.Authorization.UpdateUserRoles)]
		public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
		{
			var response = await Mediator.Send(command);
			return Result(response);
		}

		[HttpGet(Router.Authorization.ManageUserClaims)]
		public async Task<IActionResult> ManageUserClaims([FromRoute] int userId)
		{
			var response = await Mediator.Send(new ManageUserClaimsQuery() { UserId = userId });
			return Result(response);
		}

		[HttpPut(Router.Authorization.UpdateUserClaims)]
		public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
		{
			var response = await Mediator.Send(command);
			return Result(response);
		}

	}
}
