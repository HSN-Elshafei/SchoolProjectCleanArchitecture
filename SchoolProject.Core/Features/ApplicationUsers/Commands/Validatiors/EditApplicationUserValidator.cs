﻿using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUsers.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.ApplicationUsers.Commands.Validatiors
{
	internal class EditApplicationUserValidator : ApplicationUserValidator<EditApplicationUserCommand>
	{
		#region Ctor
		public EditApplicationUserValidator(IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			ApplyValidationRuels();
			ApplyCustomValidationRuels();
		}
		#endregion

		#region Actions
		public void ApplyValidationRuels()
		{
			RuleFor(s => s.Name).NotEmpty().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotEmpty]}")
								.NotNull().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotNull]}");

			RuleFor(s => s.Email).NotEmpty().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotEmpty]}")
								.NotNull().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotNull]}");

			RuleFor(s => s.Phone).NotEmpty().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotEmpty]}")
								.NotNull().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotNull]}");

			RuleFor(s => s.Address).NotEmpty().WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotEmpty]}")
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
