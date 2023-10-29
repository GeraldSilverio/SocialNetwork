using AutoMapper;
using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.Dtos.Account;
using SocialNewtwork.Core.Application.ViewModels.PostsViewModels;
using SocialNewtwork.Core.Application.ViewModels.UsersViewModels;

namespace SocialNewtwork.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Users
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
            #endregion

            #region Posts

            CreateMap<Posts, SavePostViewModel>()
                .ForMember(x => x.File, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Comments, opt => opt.Ignore()); 
            
            CreateMap<Posts, PostViewModel>()
                .ForMember(x => x.File, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Comments, opt => opt.Ignore());

            #endregion
        }

    }
}
