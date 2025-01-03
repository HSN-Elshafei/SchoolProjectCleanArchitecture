using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Responses;

namespace SchoolProject.Service.Abstracts
{
	public interface IAuthorizationService
	{
		public Task<string> AddRoleAsync(string roleName);
		public Task<string> EditRoleAsync(EditRoleRequest role);
		public Task<string> DeleteRoleAsync(int id);
		public Task<List<Role>> GetRolesListAsync();
		public Task<Role> GetRoleByIdAsync(int id);
		public Task<ManageUserRolesResponse> GetManageUserRolesAsync(int userId);
		public Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest updateUserRoles);
		public Task<ManageUserClaimsResponse> GetManageUserClaimsAsync(int userId);
		public Task<string> UpdateUserClaimsAsync(UpdateUserClaimsRequest updateUserClaims);
		public Task<bool> IsRoleExistAsync(string roleName);
		public Task<bool> IsRoleExistByIdAsync(int roleId);
		public Task<bool> IsUserExistByIdAsync(int userId);
	}
}
