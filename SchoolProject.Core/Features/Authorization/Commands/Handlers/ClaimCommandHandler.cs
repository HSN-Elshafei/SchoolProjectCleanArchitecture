using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;


namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
	public class ClaimCommandHandler : ResponseHandler,
									  IRequestHandler<UpdateUserClaimsCommand, Response<string>>
	{
		#region Fields
		private readonly IAuthorizationService _authorizationService;
		#endregion

		#region Ctor
		public ClaimCommandHandler(IAuthorizationService authorizationService, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_authorizationService = authorizationService;
		}
		#endregion

		#region Methods
		public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.UpdateUserClaimsAsync(request);
			switch (result)
			{
				case "FailedToRemoveOldClaims":
					return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldClaims]);
				case "FailedToAddNewClaims":
					return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewClaims]);
				case "FailedToUpdateUserClaims":
					return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserClaims]);
			}
			return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
		}
		#endregion
	}
}
