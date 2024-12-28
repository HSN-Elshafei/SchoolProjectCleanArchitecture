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
	}
}
