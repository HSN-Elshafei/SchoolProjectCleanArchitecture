using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.DataSeeding
{
	public static class UserSeeder
	{
		public static async Task SeedAsync(UserManager<User> _userManager)
		{
			var usersCount = await _userManager.Users.CountAsync();
			if (usersCount <= 0)
			{
				var defaultuser = new User()
				{
					UserName = "admin",
					Name = "Hassan Elshafei",
					Email = "hsnelshafei@gmail.com",
					PhoneNumber = "01097493856",
					Phone = "01097493856",
					Address = "Egypt",
					EmailConfirmed = true,
					PhoneNumberConfirmed = true
				};
				await _userManager.CreateAsync(defaultuser, "Ha@456456");
				await _userManager.AddToRoleAsync(defaultuser, "Admin");
			}
		}
	}
}
