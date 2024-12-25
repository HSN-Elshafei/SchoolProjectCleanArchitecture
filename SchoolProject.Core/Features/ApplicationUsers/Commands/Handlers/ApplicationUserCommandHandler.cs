using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUsers.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUsers.Commands.Handlers
{
	public class ApplicationUserCommandHandler : ResponseHandler,
												 IRequestHandler<AddApplicationUserCommand, Response<string>>
	{
		#region Fields
		private UserManager<User> _userManager { get; set; }
		private IMapper _mapper { get; set; }
		#endregion

		#region Ctor
		public ApplicationUserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, UserManager<User> userManager, IMapper mapper) : base(stringLocalizer)
		{
			_userManager = userManager;
			_mapper = mapper;
		}
		#endregion

		#region Action
		public async Task<Response<string>> Handle(AddApplicationUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user != null)
			{
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailExist]);
			}

			var userByUserName = await _userManager.FindByNameAsync(request.UserName);
			if (userByUserName != null)
			{
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserNameExist]);
			}
			var identityUser = _mapper.Map<User>(request);
			var result = await _userManager.CreateAsync(identityUser, request.Password);
			if (!result.Succeeded)
			{
				return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FaildToAddUser]);
			}
			return Created("");
		}
		#endregion
	}
}
