using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Queries.Validators
{
	public class GetRoleByIdValidation : AuthorizationValidation<GetRoleByIdQuery>
	{
		#region Fields

		#endregion

		#region Ctor
		public GetRoleByIdValidation(IAuthorizationService authorizationService, IStringLocalizer<SharedResources> stringLocalizer) : base(authorizationService, stringLocalizer)
		{
			ApplyValidationRuels();
			ApplyCustomValidationRuels();
		}
		#endregion

		#region Actions
		public void ApplyValidationRuels()
		{
			RuleFor(x => x.RoleId)
				 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
		}
		public void ApplyCustomValidationRuels()
		{
			RuleFor(s => s.RoleId)
				.MustAsync(async (Key, CancellationToken) => await _authorizationService.IsRoleExistByIdAsync(Key))
				.WithMessage(_stringLocalizer[SharedResourcesKeys.NotFound]);
		}
		#endregion
	}
}
