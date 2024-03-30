using AutoMapper;
using Mottu.Application.Helpers;
using Mottu.Application.Interfaces;
using Mottu.Application.Interfaces.Base;
using Mottu.Application.Requests;
using Mottu.Application.Responses;
using Mottu.Application.Services.Base;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Application.Services
{
    public class NotificatedUserService : ServiceBase<NotificatedUser, NotificatedUserRequest>, INotificatedUserService
    {
        protected readonly IMapper? _mapper;

        public NotificatedUserService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponseFactory<ListNotificatedUsersResponse>> GetListOfNotificatedUsers(NotificatedUserRequest request)
        {
            //Valida o request
            if (request is null)
                return ServiceResponseFactory<ListNotificatedUsersResponse>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            //Valida o requester
            var validation = await ValidateRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<ListNotificatedUsersResponse>.Return(false, validation.StatusCode, validation.Message!);

            //Recupera a lista de usuários que foram notificados para o pedido recebido
            var list = await _unitOfWork!.userRepository.GetFullListOfNotificatedUsers(request.OrderId);

            var convert = new ConvertModelToResponse<AppUser, AppUserResponse>(_mapper);
            List<AppUserResponse> result = convert.GetResponsList(list!);

            //Recupera o pedido
            var order = _unitOfWork.orderRepository.GetById(request.OrderId).Result;
            var orderResponse = _mapper!.Map<OrderResponse>(order);

            var responseListNotificatedUsers = new ListNotificatedUsersResponse
            {
                ListNotificatedUsers = result,
                Order = orderResponse
            };

            return ServiceResponseFactory<ListNotificatedUsersResponse>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Usuário solicitante inválido!", responseListNotificatedUsers);
        }
    }
}
