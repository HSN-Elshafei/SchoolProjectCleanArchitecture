using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.ApplicationUsers.Commands.Validatiors
{
	public class ApplicationUserValidator<T> : AbstractValidator<T>
	{
		#region Fields
		protected readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion

		#region Ctor
		public ApplicationUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
		}
		#endregion
	}
}
