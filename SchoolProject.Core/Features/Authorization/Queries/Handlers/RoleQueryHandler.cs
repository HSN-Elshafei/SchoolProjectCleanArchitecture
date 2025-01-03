using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Features.Authorization.Queries.Responses;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Responses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler : ResponseHandler,
									IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResponse>>>,
									IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>,
									IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResponse>>
	{
		#region Fields
		private readonly IAuthorizationService _authorizationService;
		private readonly IMapper _mapper;
		#endregion

		#region Ctor
		public RoleQueryHandler(IAuthorizationService authorizationService, IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper) : base(stringLocalizer)
		{
			_authorizationService = authorizationService;
			_mapper = mapper;
		}
		#endregion

		#region Methods
		public async Task<Response<List<GetRolesListResponse>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
		{
			var roles = await _authorizationService.GetRolesListAsync();
			var rolesMapper = _mapper.Map<List<GetRolesListResponse>>(roles);
			return Success(rolesMapper);
		}

		public async Task<Response<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
		{
			var roles = await _authorizationService.GetRoleByIdAsync(request.RoleId);
			var rolesMapper = _mapper.Map<GetRoleByIdResponse>(roles);
			return Success(rolesMapper);
		}

		public async Task<Response<ManageUserRolesResponse>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
		{
			var userRoles = await _authorizationService.GetManageUserRolesAsync(request.UserId);
			return Success(userRoles);
		}
		#endregion

	}
}
