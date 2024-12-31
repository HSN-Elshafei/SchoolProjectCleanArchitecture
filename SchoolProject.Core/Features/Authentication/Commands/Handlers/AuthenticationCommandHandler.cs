using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Commands.Handlers
{
	public class AuthenticationCommandHandler : ResponseHandler,
												 IRequestHandler<SignInAuthenticationCommand, Response<JwtAuthResponse>>,
												 IRequestHandler<RefreshTokenCommand, Response<JwtAuthResponse>>
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IMapper _mapper;
		private readonly IAuthenticationService _authenticationService;

		public AuthenticationCommandHandler(
			IStringLocalizer<SharedResources> stringLocalizer,
			UserManager<User> userManager,
			IMapper mapper,
			SignInManager<User> signInManager,
			IAuthenticationService authenticationService) : base(stringLocalizer)
		{
			_userManager = userManager;
			_mapper = mapper;
			_signInManager = signInManager;
			_authenticationService = authenticationService;
		}

		public async Task<Response<JwtAuthResponse>> Handle(SignInAuthenticationCommand request, CancellationToken cancellationToken)
		{
			var user = _userManager.Users.FirstOrDefault(u =>
				u.Email == request.UserNameOrEmail || u.UserName == request.UserNameOrEmail);

			if (user == null)
			{
				return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.UserNotExist]);
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
			if (!result.Succeeded)
			{
				return BadRequest<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.FaildInUserNameOrPassword]);
			}

			return Success<JwtAuthResponse>(await _authenticationService.GetJWTToken(user));
		}

		public async Task<Response<JwtAuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var jwtToken = await _authenticationService.ReadJWTTokenAsync(request.AccessToken);
			var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == nameof(UserClaimModel.Id)).Value;
			var userRefreshToken = await _authenticationService.GetUserRefreshTokenAsync(request.AccessToken, request.RefreshToken, userId);
			string validation = await _authenticationService.ValidationAsync(jwtToken, userRefreshToken);

			switch (validation)
			{
				case "InvalidToken":
					return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.InvalidToken]);
				case "TokenIsNotExpired":
					return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.TokenIsNotExpired]);
				case "RefreshTokenIsNotFound":
					return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsNotFound]);
				case "RefreshTokenIsExpired":
					return Unauthorized<JwtAuthResponse>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsExpired]);
				case "UserIsNotFound":
					return NotFound<JwtAuthResponse>();
			}

			var result = await _authenticationService.GetRefreshTokenAsync(userRefreshToken, request.RefreshToken);
			return Success<JwtAuthResponse>(result);
		}
	}
}
