using AutoMapper;
using SocialNewtwork.Core.Application.Dtos.Account;
using SocialNewtwork.Core.Application.ViewModels.UsersViewModels;

namespace SocialNewtwork.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x=> x.Error, opt => opt.Ignore())
                .ForMember(x=> x.HasError, opt => opt.Ignore())
                .ReverseMap(); 
            
            CreateMap<RegisterRequest, RegisterUserViewModel>()
                .ForMember(x=> x.Error, opt => opt.Ignore())
                .ForMember(x=> x.HasError, opt => opt.Ignore())
                .ForMember(x=> x.File, opt => opt.Ignore())
                .ReverseMap();
            
            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x=> x.Error, opt => opt.Ignore())
                .ForMember(x=> x.HasError, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();

        }

    }
}
