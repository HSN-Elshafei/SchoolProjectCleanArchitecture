using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Responses;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
    public class SignInAuthenticationCommand : IRequest<Response<JwtAuthResponse>>
	{
		public string UserNameOrEmail { get; set; }
		public string Password { get; set; }
	}
}
