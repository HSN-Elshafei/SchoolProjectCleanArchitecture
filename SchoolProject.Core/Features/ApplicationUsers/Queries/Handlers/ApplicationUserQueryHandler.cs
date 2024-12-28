using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUsers.Queries.Models;
using SchoolProject.Core.Features.ApplicationUsers.Queries.Responses;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUsers.Queries.Handlers
{
	internal class ApplicationUserQueryHandler : ResponseHandler,
												 IRequestHandler<GetApplicationUserPaginatedListQuery, PaginatedResult<GetApplicationUserResponse>>
												 , IRequestHandler<GetApplicationUserByIdQuery, Response<GetApplicationUserResponse>>
	{
		#region Fields
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		#endregion

		#region Ctor
		public ApplicationUserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, UserManager<User> userManager, IMapper mapper) : base(stringLocalizer)
		{
			_userManager = userManager;
			_mapper = mapper;
		}


		#endregion

		#region Actions
		public async Task<PaginatedResult<GetApplicationUserResponse>> Handle(GetApplicationUserPaginatedListQuery request, CancellationToken cancellationToken)
		{
			var users = _userManager.Users.AsQueryable();
			if (request.Search != null)
			{
				users = _userManager.Users.Where(u => u.Email.Contains(request.Search)
												   || u.Name.Contains(request.Search)
												   || u.Phone.Contains(request.Search)
												   || u.UserName.Contains(request.Search)).AsQueryable();
			}
			var paginatedList = await _mapper.ProjectTo<GetApplicationUserResponse>(users).ToPaginatedResult(request.PageNumber, request.PageSize);
			return paginatedList;
		}

		public async Task<Response<GetApplicationUserResponse>> Handle(GetApplicationUserByIdQuery request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.Id.ToString());
			if (user == null)
			{
				return NotFound<GetApplicationUserResponse>();
			}
			var mapper = _mapper.Map<GetApplicationUserResponse>(user);
			return Success(mapper);
		}
		#endregion
	}
}
