using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validatiors
{
	public class EditStudentValidator : AbstractValidator<EditStudentCommand>
	{
		#region Fields
		private readonly IStudentService _studentService;
		#endregion

		#region Ctor
		public EditStudentValidator(IStudentService studentService)
		{
			_studentService = studentService;
			ApplyValidationRuels();
			ApplyCustomValidationRuels();
		}
		#endregion

		#region Actions
		public void ApplyValidationRuels()
		{
			RuleFor(s => s.Name).NotEmpty().WithMessage($"Name Must not Empty")
								.NotNull().WithMessage($"Name Must not Null");
		}
		public void ApplyCustomValidationRuels()
		{
			RuleFor(s => s.Phone).MustAsync(async (model, Key, CancellationToken) => !await _studentService.IsStudentExistAsync(Key, model.Id)).WithMessage($"This Student Exist");
		}
		#endregion
	}
}
