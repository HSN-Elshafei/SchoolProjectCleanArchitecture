using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Responses;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
	public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdResponse>>
	{
		public int RoleId { get; set; }
	}
}
