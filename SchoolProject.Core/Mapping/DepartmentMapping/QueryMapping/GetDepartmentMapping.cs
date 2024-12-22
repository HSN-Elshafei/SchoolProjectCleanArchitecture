using SchoolProject.Core.Features.Departments.Queries.Responses;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.DepartmentMapping
{
	public partial class DepartmentProfile
	{
		public void GetDepartmentMapping()
		{
			CreateMap<Department, GetDepartmentResponse>()
					 .ForMember(dest => dest.DName, opt => opt.MapFrom(src => src.Localize(src.DNameAr, src.DName)))
					 .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Localize(src.Instructor.INameAr, src.Instructor.IName)))
					 //.ForMember(dest => dest.StudentsList, opt => opt.MapFrom(src => src.Students))
					 .ForMember(dest => dest.SubjectsList, opt => opt.MapFrom(src => src.DepartmentSubjects))
					 .ForMember(dest => dest.InstructorsList, opt => opt.MapFrom(src => src.Instructors));

			//CreateMap<Student, StudentResponse>()
			//		 .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.StudID))
			//		 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.Name)));

			CreateMap<DepartmetSubject, SubjectResponse>()
					 .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.SubID))
					 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.Subject.SubjectNameAr, src.Subject.SubjectName)));

			CreateMap<Instructor, InstructorResponse>()
					 .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.InsId))
					 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.INameAr, src.IName)));
		}
	}
}
