﻿using AutoMapper;
using SchoolProject.Core.Features.Authorization.Queries.Responses;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.RoleMapping
{
	public partial class RoleProfile : Profile
	{
		public void GetRolesListQueryMapping()
		{
			CreateMap<Role, GetRolesListResponse>()
				.ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name));
		}
	}
}
