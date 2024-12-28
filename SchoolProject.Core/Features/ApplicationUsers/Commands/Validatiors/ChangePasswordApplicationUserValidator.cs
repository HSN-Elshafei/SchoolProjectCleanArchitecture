using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUsers.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.ApplicationUsers.Commands.Validatiors
{
	public class ChangePasswordApplicationUserValidator : ApplicationUserValidator<ChangePasswordApplicationUserCommand>
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
			RuleFor(s => s.CurrentPassword).NotEmpty().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotEmpty]}")
								.NotNull().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotNull]}");

			RuleFor(s => s.NewPassword).NotEmpty().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotEmpty]}")
								.NotNull().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotNull]}");

			RuleFor(s => s.ConfirmPassword).NotEmpty().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotEmpty]}")
								.NotNull().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotNull]}")
								.Equal(s => s.NewPassword).WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotNull]}");
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