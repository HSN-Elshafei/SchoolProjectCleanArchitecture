using SchoolProject.Core.Features.Departments.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
	public partial class DepartmentProfile
	{
		public void GetDepartmentMapping()
		{
			CreateMap<Department, GetDepartmentResponse>()
					 .ForMember(dest => dest.DName, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DName)));
		}
	}
}
