using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Queries.Validators
{
	public class AuthorizationValidation<T> : AbstractValidator<T>
	{
		#region Fields

		protected readonly IStringLocalizer<SharedResources> _stringLocalizer;
		protected readonly IAuthorizationService _authorizationService;

		#endregion

		#region Ctor
		public AuthorizationValidation(IAuthorizationService authorizationService, IStringLocalizer<SharedResources> stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_authorizationService = authorizationService;
		}
		#endregion
	}
}
