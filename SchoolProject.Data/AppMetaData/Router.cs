namespace SchoolProject.Data.AppMetaData
{
	public static class Router
	{
		public const string Root = "Api";
		public const string Version = "V1";
		public const string Rule = $"{Root}/{Version}/";

		public static class StudentRouting
		{
			public const string Prefix = $"{Rule}Student/";
			public const string List = $"{Prefix}List";
			public const string GetById = $"{Prefix}{{id}}";
			public const string Add = $"{Prefix}Add";
			public const string Edit = $"{Prefix}Edit";
			public const string Delete = $"{Prefix}Delete/{{id}}";
			public const string Paginated = $"{Prefix}Paginated/List";

		}
		public static class DepartmentRouting
		{
			public const string Prefix = $"{Rule}Department/";
			public const string List = $"{Prefix}List";
			public const string GetById = $"{Prefix}id";
			public const string Add = $"{Prefix}Add";
			public const string Edit = $"{Prefix}Edit";
			public const string Delete = $"{Prefix}Delete/{{id}}";
			public const string Paginated = $"{Prefix}Paginated/List";

		}
		public static class ApplicationUser
		{
			public const string Prefix = $"{Rule}User/";
			public const string List = $"{Prefix}List";
			public const string GetById = $"{Prefix}{{id}}";
			public const string Add = $"{Prefix}Add";
			public const string Edit = $"{Prefix}Edit";
			public const string ChangePassword = $"{Prefix}ChangePassword";
			public const string Delete = $"{Prefix}Delete/{{id}}";
			public const string Paginated = $"{Prefix}Paginated/List";
		}
		public static class Authentication
		{
			public const string Prefix = $"{Rule}Authentication/";
			public const string SignIn = $"{Prefix}SignIn";
			public const string RefreshToken = $"{Prefix}RefreshToken";
			public const string ValidateToken = $"{Prefix}ValidateToken";
		}
		public static class Authorization
		{
			public const string Prefix = $"{Rule}Authorization/";
			public const string Role = $"{Prefix}Role/";
			public const string GetRoleList = $"{Role}List";
			public const string GetRoleById = $"{Role}{{id}}";
			public const string ManageUserRoles = $"{Role}ManageUserRoles/{{userId}}";
			public const string UpdateUserRoles = $"{Role}UpdateUserRole";
			public const string AddRole = $"{Role}Add";
			public const string EditRole = $"{Role}Edit";
			public const string DeleteRole = $"{Role}Delete/{{id}}";
			public const string Claim = $"{Prefix}Claim/";
			public const string ManageUserClaims = $"{Claim}ManageUserClaims/{{userId}}";
			public const string UpdateUserClaims = $"{Claim}UpdateUserClaims";



		}
	}
}
