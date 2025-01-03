using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
	public class RoleValidation<T> : AbstractValidator<T>
	{
		#region Fields

		protected readonly IStringLocalizer<SharedResources> _stringLocalizer;
		protected readonly IAuthorizationService _authorizationService;

		#endregion

		#region Ctor
		public RoleValidation(IAuthorizationService authorizationService, IStringLocalizer<SharedResources> stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_authorizationService = authorizationService;
		}
		#endregion
	}
}
