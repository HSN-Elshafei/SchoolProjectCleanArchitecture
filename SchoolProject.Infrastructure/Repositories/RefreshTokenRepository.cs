using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
	public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
	{
		#region Fields
		private readonly DbSet<UserRefreshToken> _userRefreshToken;
		#endregion

		#region ctor
		public RefreshTokenRepository(ApplicationDBContext dbContext) : base(dbContext)
		{
			_userRefreshToken = dbContext.Set<UserRefreshToken>();
		}
		#endregion

		#region Methods
		#endregion
	}
}
