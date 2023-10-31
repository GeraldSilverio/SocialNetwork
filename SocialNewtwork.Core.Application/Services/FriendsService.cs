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

        public FriendsService(IMapper mapper, IFriendRepositotyAsync friendRepository, IAccountService accountService) : base(mapper, friendRepository)
        {
            _accountService = accountService;
            _friendRepository = friendRepository;
        }

        public override async Task<AddFriendViewModel> Add(AddFriendViewModel model)
        {
            var user = await _accountService.GetById(model.IdUser);
            var friend = await _accountService.GetByUsername(model.UserName);

            //Validando que el usuario exista.
            if (friend == null)
            {
                model.HasError = true;
                model.Error = "ESTE USUARIO NO EXISTE";
                return model;
            }
            //Validando que no sean amigos.
            var areFriend = await IsFriendAdd(model.IdUser, friend.Id);

            if (areFriend == false)
            {
                model.HasError = true;
                model.Error = "YA SON AMIGOS, NO LO PUEDES VOLVER A AGREGAR";
                return model;
            }
            //Validando que no se quiere agregar el mismo.
            if (model.UserName == user.UserName)
            {
                model.HasError = true;
                model.Error = "NO PUEDES AGREGARTE A TI MISMO COMO AMIGO";
                return model;
            }
            model.IdFriend = friend.Id;


            return await base.Add(model);
        }

        public async Task<List<FriendViewModel>> GetAllByUser(string userName)
        {
            List<FriendViewModel> friendsViewModels = new List<FriendViewModel>();

            //Obtengo el usuario que esta online.
            var userExis = await _accountService.GetByUsername(userName);
            //Mando a buscar sus amigos por su id.
            var friends = await _friendRepository.GetAllByUser(userExis.Id);
            //Mapeo el listado de friends y los retorno.

            foreach (var friend in friends)
            {
                //Buscando el amigo que se esta iterando.
                var friendExisted = await _accountService.GetById(friend.IdFriend);
                //Creando el objeto del tipo de la lista para ir agregandolos.
                var friendView = new FriendViewModel()
                {
                    Id = friend.Id,
                    IdFriend = friend.IdFriend,
                    UserName = friend.UserName,
                    Name = friendExisted.Name,
                    LastName = friendExisted.LastName,
                    IdUser = friend.IdUser
                };
                friendsViewModels.Add(friendView);
            }
            return friendsViewModels;
        }

        public async Task<bool> IsFriendAdd(string idUser, string idFriend)
        {
            return await _friendRepository.IsFriendAdd(idUser, idFriend);
        }
    }
}
