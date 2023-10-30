using AutoMapper;
using SocialNetwork.Core.Domain.Entities;
using SocialNewtwork.Core.Application.Interfaces.Repositories;
using SocialNewtwork.Core.Application.Interfaces.Services;
using SocialNewtwork.Core.Application.ViewModels.FriendViewModels;

namespace SocialNewtwork.Core.Application.Services
{
    public class FriendsService : GenericService<AddFriendViewModel, FriendViewModel, Friends>, IFriendsService
    {
        private readonly IFriendRepositotyAsync _friendRepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public FriendsService(IMapper mapper, IFriendRepositotyAsync friendRepository, IAccountService accountService) : base(mapper, friendRepository)
        {
            _accountService = accountService;
            _friendRepository = friendRepository;
            _mapper = mapper;
        }

        public override async Task<AddFriendViewModel> Add(AddFriendViewModel model)
        {
            var friend = await _accountService.GetByUsername(model.UserName);
            if (friend == null)
            {
                model.HasError = true;
                model.Error = "ESTE USUARIO NO EXISTE";
                return model;
            }
            model.IdFriend = friend.Id;


            return await base.Add(model);
        }

        public async Task<List<FriendViewModel>> GetAllByUser(string userName)
        {
            //Obtengo el usuario que esta online.
            var userExis = await _accountService.GetByUsername(userName);
            //Mando a buscar sus amigos por su id.
            var friends = await _friendRepository.GetAllByUser(userExis.Id);
            //Mapeo el listado de friends y los retorno.
            var friendsViewModel = _mapper.Map<List<FriendViewModel>>(friends);
            return friendsViewModel;
        }
    }
}
