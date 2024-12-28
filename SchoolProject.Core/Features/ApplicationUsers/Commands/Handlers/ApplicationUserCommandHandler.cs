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
												 IRequestHandler<AddApplicationUserCommand, Response<string>>,
												 IRequestHandler<EditApplicationUserCommand, Response<string>>,
												 IRequestHandler<DeleteApplicationUserCommand, Response<string>>,
												 IRequestHandler<ChangePasswordApplicationUserCommand, Response<string>>
	{
		#region Fields
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
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

		public async Task<Response<string>> Handle(EditApplicationUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.Id.ToString());
			if (user == null) return NotFound<string>();

			var userByName = _userManager.Users.Where(u => u.UserName == request.UserName && u.Id != user.Id).FirstOrDefault();
			if (userByName != null) return Unauthorized<string>();

			_mapper.Map(request, user);
			// Ensure SecurityStamp is preserved or set
			if (string.IsNullOrEmpty(user.SecurityStamp))
			{
				user.SecurityStamp = Guid.NewGuid().ToString();
			}

			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
			{
				return Updated<string>();
			}
			else
			{
				return BadRequest<string>();
			}
		}

		public async Task<Response<string>> Handle(DeleteApplicationUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.Id.ToString());
			if (user == null) return NotFound<string>();
			var result = await _userManager.DeleteAsync(user);
			if (result.Succeeded)
			{
				return Deleted<string>();
			}
			else return BadRequest<string>();
		}

		public async Task<Response<string>> Handle(ChangePasswordApplicationUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.Id.ToString());
			if (user == null) return NotFound<string>();
			var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
			if (result.Succeeded)
			{
				return Updated<string>();
			}
			else
			{
				return BadRequest<string>();
			}

		}
		#endregion
	}
}
