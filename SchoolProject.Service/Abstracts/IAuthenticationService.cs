using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
	{
		public Task<JwtAuthResponse> GetJWTToken(User user);
		public Task<JwtSecurityToken> ReadJWTTokenAsync(string accessToken);
		public Task<UserRefreshToken> GetUserRefreshTokenAsync(string accessToken, string refreshToken, string userId);
		public Task<string> ValidateTokenAsync(JwtSecurityToken jwtToken, UserRefreshToken userRefreshToken);
		public Task<JwtAuthResponse> GetRefreshTokenAsync(UserRefreshToken userRefreshToken, string refreshToken);
		public Task<string> ValidateToken(string accessToken);
	}
}
