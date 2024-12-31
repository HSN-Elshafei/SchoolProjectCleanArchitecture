using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolProject.Data.Entities.Identity
{
	public class User : IdentityUser<int>
	{
		public User()
		{
			UserRefreshTokens = new HashSet<UserRefreshToken>();
		}
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		[InverseProperty(nameof(UserRefreshToken.User))]
		public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
	}
}
