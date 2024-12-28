using Microsoft.AspNetCore.Identity;
namespace SchoolProject.Data.Entities.Identity
{
	public class User : IdentityUser<int>
	{
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
	}
}
