using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Authentication.Commands.Validatiors
{
	public class AuthenticationValidator<T> : AbstractValidator<T>
	{
		#region Fields
		protected readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion

		#region Ctor
		public AuthenticationValidator(IStringLocalizer<SharedResources> stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
		}
		#endregion
	}
}
