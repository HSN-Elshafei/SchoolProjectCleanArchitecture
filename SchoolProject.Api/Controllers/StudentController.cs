using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin,User")]
	public class StudentController : AppControllerBase
	{
		#region Fields
		#endregion

		#region Ctor

		#endregion

		#region Methods
		[HttpGet(Router.StudentRouting.List)]
		public async Task<IActionResult> GetStudentList()
		{
			var response = await Mediator.Send(new GetStudentListQuery());
			return Result(response);
		}

		[HttpGet(Router.StudentRouting.Paginated)]
		public async Task<IActionResult> GetStudentPaginatedList([FromQuery] GetStudentPaginatedListQuery query)
		{
			var response = await Mediator.Send(query);
			return Ok(response);
		}

		[HttpGet(Router.StudentRouting.GetById)]
		public async Task<IActionResult> GetStudentById([FromRoute] int id)
		{
			var response = await Mediator.Send(new GetStudentByIdQuery() { Id = id });
			return Result(response);
		}

		[HttpPost(Router.StudentRouting.Add)]
		public async Task<IActionResult> AddStudent([FromBody] AddStudentCommand std)
		{
			var response = await Mediator.Send(std);
			return Result(response);
		}

		[HttpPut(Router.StudentRouting.Edit)]
		public async Task<IActionResult> EditStudent([FromBody] EditStudentCommand std)
		{
			var response = await Mediator.Send(std);
			return Result(response);
		}
		[HttpDelete(Router.StudentRouting.Delete)]
		public async Task<IActionResult> DeleteStudent([FromRoute] int id)
		{
			var response = await Mediator.Send(new DeleteStudentCommand(id));
			return Result(response);
		}

		#endregion
	}
}
