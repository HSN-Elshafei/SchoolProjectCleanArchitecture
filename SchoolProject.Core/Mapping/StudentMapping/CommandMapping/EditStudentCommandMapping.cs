using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
	public partial class DepartmentProfile
	{
		public void EditStudentCommandMapping()
		{
			CreateMap<EditStudentCommand, Student>()
						.ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DeptId))
						.ForMember(dest => dest.StudID, opt => opt.MapFrom(src => src.Id));
		}
	}
}