using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validatiors
{
	public class AddStudentValidator : AbstractValidator<AddStudentCommand>
	{
		#region Fields
		private readonly IStudentService _studentService;
		private readonly IStringLocalizer<ShearedResources> _stringLocalizer;

		#endregion

		#region Ctor
		public AddStudentValidator(IStudentService studentService, IStringLocalizer<ShearedResources> stringLocalizer)
		{
			_studentService = studentService;
			_stringLocalizer = stringLocalizer;
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
			RuleFor(s => s.Phone).MustAsync(async (Key, CancellationToken) => !await _studentService.IsStudentExistAsync(Key)).WithMessage($"This Student Exist");
		}
		#endregion
	}
}
