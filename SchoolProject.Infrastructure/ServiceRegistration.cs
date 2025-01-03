using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Infrastructure.Data;
using System.Text;

namespace SchoolProject.Infrustructure
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration)
		{
			// Identity configuration
			services.AddIdentity<User, Role>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;

				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;

				options.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = true;
				options.SignIn.RequireConfirmedEmail = true;

			}).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

			// JWT Authentication
			var jwtSettings = new JwtSettings();
			configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);

			services.AddSingleton(jwtSettings);

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = jwtSettings.ValidateIssuer,
					ValidIssuers = new[] { jwtSettings.Issuer },
					ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
					ValidAudience = jwtSettings.Audience,
					ValidateAudience = jwtSettings.ValidateAudience,
					ValidateLifetime = jwtSettings.ValidateLifeTime,
				};
			});

			// Swagger configuration
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "School Project", Version = "v1" });
				c.EnableAnnotations();

				c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = JwtBearerDefaults.AuthenticationScheme
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = JwtBearerDefaults.AuthenticationScheme
							}
						},
						Array.Empty<string>()
					}
				});
			});

			// Authorization policies
			services.AddAuthorization(options =>
			{
				options.AddPolicy("CreateStudent", policy => policy.RequireClaim("Create Student", "True"));
				options.AddPolicy("DeleteStudent", policy => policy.RequireClaim("Delete Student", "True"));
				options.AddPolicy("EditStudent", policy => policy.RequireClaim("Edit Student", "True"));
			});

			return services;
		}
	}
}
