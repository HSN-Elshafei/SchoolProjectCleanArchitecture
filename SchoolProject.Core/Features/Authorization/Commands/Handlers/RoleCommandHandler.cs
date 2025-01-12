﻿using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
	public class RoleCommandHandler : ResponseHandler,
									  IRequestHandler<AddRoleCommand, Response<string>>,
									  IRequestHandler<EditRoleCommand, Response<string>>,
									  IRequestHandler<DeleteRoleCommand, Response<string>>,
									  IRequestHandler<UpdateUserRolesCommand, Response<string>>
	{
		#region Fields
		private readonly IAuthorizationService _authorizationService;
		#endregion

		#region Ctor
		public RoleCommandHandler(IAuthorizationService authorizationService, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_authorizationService = authorizationService;
		}
		#endregion

		#region Methods
		public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.AddRoleAsync(request.RoleName);
			if (result == "Success") return Success("");
			return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);
		}

		public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.EditRoleAsync(request);

			if (result == "Success") return Success("");

			return BadRequest<string>(_stringLocalizer[result]);
		}

		public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.DeleteRoleAsync(request.Id);

			if (result == "Used") return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.RoleIsUsed]);
			if (result == "Success") return Success("");

			return BadRequest<string>(_stringLocalizer[result]);
		}

		public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.UpdateUserRolesAsync(request);
			switch (result)
			{
				case "FailedToRemoveOldRoles":
					return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
				case "FailedToAddNewRoles":
					return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]);
				case "FailedToUpdateUserRoles":
					return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
			}
			return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
		}
		#endregion
	}
}
