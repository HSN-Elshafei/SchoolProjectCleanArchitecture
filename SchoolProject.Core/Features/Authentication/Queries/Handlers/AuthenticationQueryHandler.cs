﻿using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authentication.Queries.Handles
{
	public class AuthenticationQueryHandler : ResponseHandler,
		IRequestHandler<AuthorizeUserQuery, Response<string>>

	{


		#region Fields
		private readonly IAuthenticationService _authenticationService;

		#endregion

		#region Constructors
		public AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
											IAuthenticationService authenticationService) : base(stringLocalizer)
		{
			_authenticationService = authenticationService;
		}


		#endregion

		#region Handle Functions
		public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
		{
			var result = await _authenticationService.ValidateToken(request.AccessToken);
			if (result == "NotExpired")
				return Success<string>(_stringLocalizer[SharedResourcesKeys.TokenIsNotExpired]);
			return Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.TokenIsExpired]);
		}

		#endregion
	}
}
