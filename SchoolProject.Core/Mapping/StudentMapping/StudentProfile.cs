using AutoMapper;

namespace SchoolProject.Core.Mapping.StudentMapping
{
	public partial class DepartmentProfile : Profile
	{
		public DepartmentProfile()
		{
			GetStudentMapping();
			AddStudentCommandMapping();
			EditStudentCommandMapping();
		}
	}
}
