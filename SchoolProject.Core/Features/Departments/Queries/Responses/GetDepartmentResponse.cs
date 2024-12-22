using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.Departments.Queries.Responses
{
	public class GetDepartmentResponse
	{
		public int DID { get; set; }
		public string DName { get; set; }
		public string ManagerName { get; set; }
		public PaginatedResult<StudentResponse>? StudentsList { get; set; }
		public List<SubjectResponse>? SubjectsList { get; set; }
		public List<InstructorResponse>? InstructorsList { get; set; }
	}
	public class StudentResponse
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public StudentResponse(int id, string name)
		{
			ID = id;
			Name = name;
		}
	}
	public class SubjectResponse
	{
		public int ID { get; set; }
		public string Name { get; set; }
	}
	public class InstructorResponse
	{
		public int ID { get; set; }
		public string Name { get; set; }
	}
}
