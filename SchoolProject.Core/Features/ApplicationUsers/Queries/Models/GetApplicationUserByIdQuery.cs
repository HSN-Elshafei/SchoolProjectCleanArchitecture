using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUsers.Queries.Responses;

namespace SchoolProject.Core.Features.ApplicationUsers.Queries.Models
{
	public class GetApplicationUserByIdQuery : IRequest<Response<GetApplicationUserResponse>>
	{
		public int Id { get; set; }
	}
}
