using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Bases;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
	//[Route("api/[controller]")]
	[ApiController]
	public class DepartmentController : AppControllerBase
	{
		#region Fields
		#endregion

		#region Ctor

		#endregion

		#region Methods
		[HttpGet(Router.DepartmentRouting.GetById)]
		public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
		{
			var response = await Mediator.Send(query);
			return Result(response);
		}
		#endregion
	}
}
