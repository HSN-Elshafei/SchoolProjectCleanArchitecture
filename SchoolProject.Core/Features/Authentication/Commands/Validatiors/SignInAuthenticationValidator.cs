using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Validatiors
{
	public class ChangePasswordApplicationUserValidator : AuthenticationValidator<SignInAuthenticationCommand>
	{
		#region Ctor
		public ChangePasswordApplicationUserValidator(IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			ApplyValidationRuels();
			ApplyCustomValidationRuels();
		}
		#endregion

		#region Actions
		public void ApplyValidationRuels()
		{
			RuleFor(s => s.UserNameOrEmail).NotEmpty().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotEmpty]}")
								.NotNull().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotNull]}");

			RuleFor(s => s.Password).NotEmpty().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotEmpty]}")
								.NotNull().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotNull]}");

		}
		public void ApplyCustomValidationRuels()
		{
			//RuleFor(s => s.Phone)
			//	.MustAsync(async (Key, CancellationToken) => !await _studentService
			//	.IsStudentExistAsync(Key))
			//	.WithMessage($"{_stringLocalizer[ShearedResourcesKeys.Exist]}");

			//RuleFor(s => s.DeptId)
			//	.MustAsync(async (Key, CancellationToken) => await _departmentService
			//	.IsDepartmentExistAsync(Key))
			//	.WithMessage($"{_stringLocalizer[ShearedResourcesKeys.NotExist]}");
		}
		#endregion
	}
}