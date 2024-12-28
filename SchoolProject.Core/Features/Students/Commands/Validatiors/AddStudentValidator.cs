using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validatiors
{
	public class AddStudentValidator : StudentValidator<AddStudentCommand>//AbstractValidator<AddStudentCommand>
	{
		#region Fields
		//private readonly IStudentService _studentService;
		//private readonly IDepartmentService _departmentService;
		//private readonly IStringLocalizer<ShearedResources> _stringLocalizer;
		#endregion

		#region Ctor
		public AddStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> stringLocalizer, IDepartmentService departmentService) : base(studentService, stringLocalizer, departmentService)
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
		}
		public void ApplyCustomValidationRuels()
		{
			RuleFor(s => s.Phone)
				.MustAsync(async (Key, CancellationToken) => !await _studentService
				.IsStudentExistAsync(Key))
				.WithMessage($"{_stringLocalizer[SharedResourcesKeys.Exist]}");

			RuleFor(s => s.DeptId)
				.MustAsync(async (Key, CancellationToken) => await _departmentService
				.IsDepartmentExistAsync(Key))
				.WithMessage($"{_stringLocalizer[SharedResourcesKeys.NotExist]}");
		}
		#endregion
	}
}
