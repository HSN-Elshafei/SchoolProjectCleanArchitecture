using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.ApplicationUsers.Commands.Models
{
	public class EditApplicationUserCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
	}
}
