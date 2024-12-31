using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Helper;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
	public class SignInAuthenticationCommand : IRequest<Response<JwtAuthResponse>>
	{
		public string UserNameOrEmail { get; set; }
		public string Password { get; set; }
	}
}
