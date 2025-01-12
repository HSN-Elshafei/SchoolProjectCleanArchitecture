﻿namespace SchoolProject.Data.Responses
{
	public class ManageUserClaimsResponse
	{
		public int UserId { get; set; }
		public List<UserClaim> UserClaims { get; set; }
	}
	public class UserClaim
	{
		public string Type { get; set; }
		public bool Value { get; set; }
	}
}
