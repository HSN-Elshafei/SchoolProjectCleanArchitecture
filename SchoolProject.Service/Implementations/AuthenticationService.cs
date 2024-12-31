using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Implementations
{
	public class AuthenticationService : IAuthenticationService
	{
		#region Fields
		private readonly JwtSettings _jwtSettings;
		private readonly IRefreshTokenRepository _refreshToken;
		#endregion

		#region Ctor
		public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshToken)
		{
			_jwtSettings = jwtSettings;
			_refreshToken = refreshToken;
		}
		#endregion

		#region Handles Functions
		public async Task<JwtAuthResponse> GetJWTToken(User user)
		{
			var (jwtToken, accessToken) = await generateJWTTokenAsync(user);

			var refreshToken = handleRefreshToken(user.UserName);

			var userRefreshToken = new UserRefreshToken()
			{
				UserId = user.Id,
				Token = accessToken,
				RefreshToken = refreshToken.TokenString,
				ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
				AddedTime = DateTime.Now,
				IsRevoked = false,
				IsUsed = true,
				User = user
			};
			await _refreshToken.AddAsync(userRefreshToken);
			return new JwtAuthResponse()
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken
			};
		}

		private async Task<(JwtSecurityToken, string)> generateJWTTokenAsync(User user)
		{
			var claims = await getClaimAsync(user);
			var jwtToken = new JwtSecurityToken(_jwtSettings.Issuer,
												   _jwtSettings.Audience,
												   claims,
												   expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
												   signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256));
			var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
			return (jwtToken, accessToken);
		}

		private string generateRefreshToken()
		{
			var randomNumber = new byte[32];
			var randomNumberGenerate = RandomNumberGenerator.Create();
			randomNumberGenerate.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}

		private RefreshToken handleRefreshToken(string userName)
		{
			var refreshToken = new RefreshToken()
			{
				ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
				UserName = userName,
				TokenString = generateRefreshToken()
			};
			return refreshToken;
		}

		private Task<List<Claim>> getClaimAsync(User user)
		{
			var claims = new List<Claim> {
				new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),
				new Claim(nameof(UserClaimModel.UserName), user.UserName),
				new Claim(nameof(UserClaimModel.Email),user.Email),
				new Claim(nameof(UserClaimModel.PhoneNumber),user.PhoneNumber)
			};

			return Task.FromResult(claims);
		}

		public async Task<JwtAuthResponse> GetRefreshTokenAsync(UserRefreshToken userRefreshToken, string refreshToken)
		{
			var result = await generateJWTTokenAsync(userRefreshToken.User);
			userRefreshToken.Token = result.Item2;
			await _refreshToken.UpdateAsync(userRefreshToken);
			return new JwtAuthResponse()
			{
				AccessToken = result.Item2,
				RefreshToken = new RefreshToken()
				{
					UserName = userRefreshToken.User.UserName,
					ExpireAt = userRefreshToken.ExpiryDate,
					TokenString = refreshToken
				}
			};
		}

		public async Task<JwtSecurityToken> ReadJWTTokenAsync(string accessToken)
		{
			if (string.IsNullOrEmpty(accessToken))
			{
				throw new ArgumentNullException(nameof(accessToken));
			}
			var handler = new JwtSecurityTokenHandler();
			var response = handler.ReadJwtToken(accessToken);
			return response;
		}

		public async Task<string> ValidationAsync(JwtSecurityToken jwtToken, UserRefreshToken userRefreshToken)
		{
			if (jwtToken == null || jwtToken.Header.Alg != SecurityAlgorithms.HmacSha256)
			{
				return "InvalidToken";
			}
			if (jwtToken.ValidTo > DateTime.UtcNow)
			{
				return "TokenIsNotExpired";
			}
			if (userRefreshToken == null)
			{
				return "RefreshTokenIsNotFound";
			}

			if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
			{
				userRefreshToken.IsUsed = false;
				userRefreshToken.IsRevoked = true;
				await _refreshToken.UpdateAsync(userRefreshToken);
				return "RefreshTokenIsExpired";
			}

			if (userRefreshToken.User == null)
			{
				return "UserIsNotFound";
			}

			return "Valid";
		}
		public async Task<UserRefreshToken> GetUserRefreshTokenAsync(string accessToken, string refreshToken, string userId)
		{
			return await _refreshToken.GetTableNoTracking().Include(x => x.User)
										.FirstOrDefaultAsync(rt => rt.Token == accessToken
																&& rt.RefreshToken == refreshToken
																&& rt.UserId.ToString() == userId);
		}

		public async Task<string> ValidateToken(string accessToken)
		{
			var handler = new JwtSecurityTokenHandler();
			var parameters = new TokenValidationParameters
			{
				ValidateIssuer = _jwtSettings.ValidateIssuer,
				ValidIssuers = new[] { _jwtSettings.Issuer },
				ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
				ValidAudience = _jwtSettings.Audience,
				ValidateAudience = _jwtSettings.ValidateAudience,
				ValidateLifetime = _jwtSettings.ValidateLifeTime,
			};
			try
			{
				var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

				if (validator == null)
				{
					return "InvalidToken";
				}

				return "NotExpired";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
		#endregion
	}
}
