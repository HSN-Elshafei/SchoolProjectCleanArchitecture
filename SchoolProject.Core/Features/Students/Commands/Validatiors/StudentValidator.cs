using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Validatiors
{
	public class StudentValidator<T> : AbstractValidator<T>
	{
		#region Fields
		protected readonly IStudentService _studentService;
		protected readonly IDepartmentService _departmentService;
		protected readonly IStringLocalizer<SharedResources> _stringLocalizer;

		#endregion

		#region Ctor
		public StudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> stringLocalizer, IDepartmentService departmentService)
		{
			_studentService = studentService;
			_stringLocalizer = stringLocalizer;
			_departmentService = departmentService;
		}
		#endregion
	}
}
