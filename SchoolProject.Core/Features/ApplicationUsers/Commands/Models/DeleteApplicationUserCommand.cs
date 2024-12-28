using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationUsers.Commands.Models
{
	public class DeleteApplicationUserCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
	}
}
