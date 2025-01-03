using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
	public class EditRoleValidation : RoleValidation<EditRoleCommand>
	{
		#region Fields

		#endregion

		#region Ctor
		public EditRoleValidation(IAuthorizationService authorizationService, IStringLocalizer<SharedResources> stringLocalizer) : base(authorizationService, stringLocalizer)
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
			RuleFor(x => x.Id)
				 .NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotEmpty])
				 .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.Required]);
		}
		public void ApplyCustomValidationRuels()
		{
			RuleFor(s => s.Id)
				.MustAsync(async (Key, CancellationToken) => await _authorizationService.IsRoleExistByIdAsync(Key))
				.WithMessage(_stringLocalizer[SharedResourcesKeys.NotFound]);
		}
		#endregion
	}
}