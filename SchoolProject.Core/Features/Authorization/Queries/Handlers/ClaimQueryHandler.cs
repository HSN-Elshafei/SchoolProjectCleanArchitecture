using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Responses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
	public class ClaimQueryHandler : ResponseHandler,
									IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResponse>>
	{
		#region Fields
		private readonly IAuthorizationService _authorizationService;
		private readonly IMapper _mapper;
		#endregion

		#region Ctor
		public ClaimQueryHandler(IAuthorizationService authorizationService, IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper) : base(stringLocalizer)
		{
			_authorizationService = authorizationService;
			_mapper = mapper;
		}
		#endregion

		#region Methods
		public async Task<Response<ManageUserClaimsResponse>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
		{
			var userClaims = await _authorizationService.GetManageUserClaimsAsync(request.UserId);
			return Success(userClaims);
		}
		#endregion

	}
}