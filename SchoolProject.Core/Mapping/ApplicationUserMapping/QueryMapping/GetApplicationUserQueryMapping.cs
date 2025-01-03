using SchoolProject.Core.Features.ApplicationUsers.Queries.Responses;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.ApplicationUserMapping
{
	public partial class ApplicationUserProfile
	{
		public void GetApplicationUserCommandMapping()
		{
			CreateMap<User, GetApplicationUserResponse>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
				.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
				.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
		}
	}
}
