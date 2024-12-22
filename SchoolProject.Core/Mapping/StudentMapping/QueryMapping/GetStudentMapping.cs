using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
	public partial class DepartmentProfile
	{
		public void GetStudentMapping()
		{
			CreateMap<Student, GetStudentResponse>()
					 .ForMember(dest => dest.DName, opt => opt.MapFrom(src => src.Localize(src.Department.DNameAr, src.Department.DName)))
					 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.Name)));
		}
	}
}
