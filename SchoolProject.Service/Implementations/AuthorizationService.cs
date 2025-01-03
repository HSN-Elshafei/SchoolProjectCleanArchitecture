using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Responses;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstracts;
using System.Security.Claims;

namespace SchoolProject.Service.Implementations
{
	public class AuthorizationService : IAuthorizationService
	{
		#region Fields
		private readonly RoleManager<Role> _roleManager;
		private readonly UserManager<User> _userManager;
		private readonly ApplicationDBContext _dbContext;
		#endregion

		#region Ctor
		public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager, ApplicationDBContext dbContext)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_dbContext = dbContext;
		}


		#endregion

		#region Handles Functions
		public async Task<string> AddRoleAsync(string roleName)
		{
			var identityRole = new Role();
			identityRole.Name = roleName;
			var result = await _roleManager.CreateAsync(identityRole);
			if (result.Succeeded)
				return "Success";
			return "Failed";
		}

		public async Task<string> EditRoleAsync(EditRoleRequest role)
		{
			var roleF = await _roleManager.FindByIdAsync(role.Id.ToString());
			roleF.Name = role.RoleName;
			var result = await _roleManager.UpdateAsync(roleF);
			if (result.Succeeded)
			{
				return "Success";
			}
			else
			{
				var errors = string.Join("-", result.Errors);
				return errors;
			}

		}

		public async Task<string> DeleteRoleAsync(int id)
		{
			var role = await _roleManager.FindByIdAsync(id.ToString());
			var user = await _userManager.GetUsersInRoleAsync(role.Name);
			if (user != null && user.Count() > 0)
			{
				return "Used";
			}
			var result = await _roleManager.DeleteAsync(role);
			if (result.Succeeded)
			{
				return "Success";
			}
			else
			{
				var errors = string.Join("-", result.Errors);
				return errors;
			}
		}

		public async Task<bool> IsRoleExistAsync(string roleName)
		{
			return await _roleManager.RoleExistsAsync(roleName);
		}

		public async Task<bool> IsRoleExistByIdAsync(int roleId)
		{
			var role = await _roleManager.FindByIdAsync(roleId.ToString());
			if (role == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public async Task<bool> IsUserExistByIdAsync(int userId)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());
			if (user == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public async Task<List<Role>> GetRolesListAsync()
		{
			return await _roleManager.Roles.ToListAsync();
		}

		public async Task<Role> GetRoleByIdAsync(int id)
		{
			return await _roleManager.FindByIdAsync(id.ToString());
		}

		public async Task<ManageUserRolesResponse> GetManageUserRolesAsync(int userId)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());
			var userRoles = await _userManager.GetRolesAsync(user);
			var roles = await _roleManager.Roles.ToListAsync();
			var response = new ManageUserRolesResponse();
			response.UserId = userId;
			List<UserRole> newUserRoles = new List<UserRole>();
			foreach (var role in roles)
			{
				newUserRoles.Add(new UserRole()
				{
					RoleId = role.Id,
					RoleName = role.Name,
					HasRole = userRoles.Contains(role.Name)
				});
			}
			response.Roles = newUserRoles;
			return response;
		}

		public async Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest updateUserRoles)
		{
			var transact = await _dbContext.Database.BeginTransactionAsync();
			try
			{
				var user = await _userManager.FindByIdAsync(updateUserRoles.UserId.ToString());
				//get user Old Roles
				var userRoles = await _userManager.GetRolesAsync(user);
				var deleteUserRole = await _userManager.RemoveFromRolesAsync(user, userRoles);
				if (!deleteUserRole.Succeeded)
				{
					return "FailedToRemoveOldRoles";
				}
				var selectedRoles = updateUserRoles.Roles.Where(x => x.HasRole == true).Select(x => x.RoleName);
				var addRoleResult = await _userManager.AddToRolesAsync(user, selectedRoles);
				if (!addRoleResult.Succeeded)
					return "FailedToAddNewRoles";
				await transact.CommitAsync();
				//return Result
				return "Success";
			}
			catch (Exception ex)
			{
				await transact.RollbackAsync();
				return "FailedToUpdateUserRoles";
			}
		}

		public async Task<ManageUserClaimsResponse> GetManageUserClaimsAsync(int userId)
		{
			var user = await _userManager.FindByIdAsync(userId.ToString());
			var userClaims = await _userManager.GetClaimsAsync(user);
			var response = new ManageUserClaimsResponse();
			response.UserId = userId;
			List<UserClaim> newUserClaims = new List<UserClaim>();
			foreach (var claim in ClaimsStore.Claims)
			{
				newUserClaims.Add(new UserClaim()
				{
					Type = claim.Type,
					Value = userClaims.Any(x => x.Type == claim.Type)
				});
			}
			response.UserClaims = newUserClaims;
			return response;
		}

		public async Task<string> UpdateUserClaimsAsync(UpdateUserClaimsRequest updateUserClaims)
		{
			var transact = await _dbContext.Database.BeginTransactionAsync();
			try
			{
				var user = await _userManager.FindByIdAsync(updateUserClaims.UserId.ToString());
				//get user Old Claims
				var userClaims = await _userManager.GetClaimsAsync(user);
				var deleteUserClaimsResult = await _userManager.RemoveClaimsAsync(user, userClaims);
				if (!deleteUserClaimsResult.Succeeded)
				{
					return "FailedToRemoveOldClaims";
				}

				var selectedClaims = updateUserClaims.UserClaims.Where(x => x.Value == true)
																.Select(x => new Claim(x.Type, x.Value.ToString()));
				var addClaimResult = await _userManager.AddClaimsAsync(user, selectedClaims);
				if (!addClaimResult.Succeeded)
					return "FailedToAddNewClaims";
				await transact.CommitAsync();
				//return Result
				return "Success";
			}
			catch (Exception ex)
			{
				await transact.RollbackAsync();
				return "FailedToUpdateUserClaims";
			}
		}
		#endregion
	}
}
