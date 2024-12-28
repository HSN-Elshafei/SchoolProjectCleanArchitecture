using MediatR;
using SchoolProject.Core.Features.ApplicationUsers.Queries.Responses;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.ApplicationUsers.Queries.Models
{
	public class GetApplicationUserPaginatedListQuery : IRequest<PaginatedResult<GetApplicationUserResponse>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string? Search { get; set; }
	}
}
