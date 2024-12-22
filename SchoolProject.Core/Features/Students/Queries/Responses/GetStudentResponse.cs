namespace SchoolProject.Core.Features.Students.Queries.Responses
{
	public class GetStudentResponse
	{
		public GetStudentResponse() { }
		public GetStudentResponse(int studID, string name, string address, string phone, string dName)
		{
			StudID = studID;
			Name = name;
			Address = address;
			Phone = phone;
			DName = dName;
		}

		public int StudID { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string? DName { get; set; }
	}
}
