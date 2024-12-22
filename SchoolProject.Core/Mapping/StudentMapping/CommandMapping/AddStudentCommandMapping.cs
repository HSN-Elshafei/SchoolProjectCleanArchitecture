using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
	public partial class DepartmentProfile
	{
		public void AddStudentCommandMapping()
		{
			CreateMap<AddStudentCommand, Student>()
						.ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DeptId));
		}
	}
}
