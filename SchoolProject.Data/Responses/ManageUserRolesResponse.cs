namespace SchoolProject.Data.Responses
{
    public class ManageUserRolesResponse
    {
        public int UserId { get; set; }
        public List<UserRole> Roles { get; set; }
    }
    public class UserRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool HasRole { get; set; }
    }
}
