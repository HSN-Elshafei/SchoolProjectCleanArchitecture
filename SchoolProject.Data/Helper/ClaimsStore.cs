using System.Security.Claims;

namespace SchoolProject.Data.Helper
{
	public static class ClaimsStore
	{
		public static List<Claim> Claims = new List<Claim>()
		{
			new Claim ("Create","false"),
			new Claim ("Edit","false"),
			new Claim ("Delete","false"),
		};
	}
}
