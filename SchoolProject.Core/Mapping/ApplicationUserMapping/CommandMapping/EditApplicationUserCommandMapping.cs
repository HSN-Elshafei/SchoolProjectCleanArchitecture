using SchoolProject.Core.Features.ApplicationUsers.Commands.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.ApplicationUserMapping
{
	public partial class ApplicationUserProfile
	{
		void EditApplicationUserCommandMapping()
		{
			CreateMap<EditApplicationUserCommand, User>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
				.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
				.ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
				.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
		}
	}
}