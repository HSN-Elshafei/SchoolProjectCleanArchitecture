using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Data;

namespace SchoolProject.Infrastructure
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddServiceRegistration(this IServiceCollection services)
		{
			services.AddIdentityCore<User>(option =>
			{
				option.SignIn.RequireConfirmedEmail = true;
				// Password settings.
				option.Password.RequireDigit = true;
				option.Password.RequireLowercase = true;
				option.Password.RequireNonAlphanumeric = true;
				option.Password.RequireUppercase = true;
				option.Password.RequiredLength = 6;
				option.Password.RequiredUniqueChars = 1;

				// Lockout settings.
				option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				option.Lockout.MaxFailedAccessAttempts = 5;
				option.Lockout.AllowedForNewUsers = true;

				// User settings.
				option.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				option.User.RequireUniqueEmail = false;
			}).AddEntityFrameworkStores<ApplicationDBContext>();
			return services;
		}
	}
}
