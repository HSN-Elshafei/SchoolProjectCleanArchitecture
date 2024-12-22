using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validatiors
{
	public class EditStudentValidator : StudentValidator<EditStudentCommand>
	{
		#region Fields
		//private readonly IStudentService _studentService;
		//private readonly IDepartmentService _departmentService;
		//private readonly IStringLocalizer<ShearedResources> _stringLocalizer;
		#endregion

		#region Ctor
		public EditStudentValidator(IStudentService studentService, IStringLocalizer<ShearedResources> stringLocalizer, IDepartmentService departmentService) : base(studentService, stringLocalizer, departmentService)
		{
			ApplyValidationRuels();
			ApplyCustomValidationRuels();
		}
		#endregion


		#region Actions
		public void ApplyValidationRuels()
		{
			RuleFor(s => s.Name).NotEmpty().WithMessage($"{_stringLocalizer[ShearedResourcesKeys.NotEmpty]}")
								.NotNull().WithMessage($"{_stringLocalizer[ShearedResourcesKeys.NotNull]}");
		}
		public void ApplyCustomValidationRuels()
		{
			RuleFor(s => s.Phone)
				.MustAsync(async (Key, CancellationToken) => !await _studentService
				.IsStudentExistAsync(Key))
				.WithMessage($"{_stringLocalizer[ShearedResourcesKeys.Exist]}");

			RuleFor(s => s.DeptId)
				.MustAsync(async (Key, CancellationToken) => await _departmentService
				.IsDepartmentExistAsync(Key))
				.WithMessage($"{_stringLocalizer[ShearedResourcesKeys.NotExist]}");
		}
		#endregion
	}
}
