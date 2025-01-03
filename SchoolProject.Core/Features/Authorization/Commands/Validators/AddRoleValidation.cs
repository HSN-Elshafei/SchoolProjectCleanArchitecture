using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
	public class AddRoleValidation : RoleValidation<AddRoleCommand>
	{
		#region Fields

		#endregion

		#region Ctor
		public AddRoleValidation(IAuthorizationService authorizationService, IStringLocalizer<SharedResources> stringLocalizer) : base(authorizationService, stringLocalizer)
		{
			ApplyValidationRuels();
			ApplyCustomValidationRuels();
		}
		#endregion

		#region Actions
		public void ApplyValidationRuels()
		{
			RuleFor(x => x.RoleName)
				 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
		}
		public void ApplyCustomValidationRuels()
		{
			RuleFor(s => s.RoleName)
				.MustAsync(async (Key, CancellationToken) => !await _authorizationService.IsRoleExistAsync(Key))
				.WithMessage(_stringLocalizer[SharedResourcesKeys.RoleIsExist]);
		}
		#endregion
	}
}
