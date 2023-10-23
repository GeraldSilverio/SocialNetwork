using AutoMapper;
using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.ViewModels.UsersViewModels;

namespace SocialNewtwork.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Users, RegisterUserViewModel>()
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());
        }

    }
}
