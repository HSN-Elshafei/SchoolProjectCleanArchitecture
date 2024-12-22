namespace SchoolProject.Core.Features.Students.Queries.Responses
{
	public class GetStudentPaginatedResponse
	{
		public int StudID { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string? DName { get; set; }

	}
}
