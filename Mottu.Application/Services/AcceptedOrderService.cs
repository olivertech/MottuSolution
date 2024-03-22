using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mottu.Application.Helpers;
using Mottu.Application.Interfaces;
using Mottu.Application.Interfaces.Base;
using Mottu.Application.Requests;
using Mottu.Application.Responses;
using Mottu.Application.Services;
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
    public class AcceptedOrderService : ServiceBase<AcceptedOrder, AcceptedOrderResponse>, IAcceptedOrderService
    {
        protected readonly IMapper? _mapper;

        public AcceptedOrderService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponseFactory<AcceptedOrderResponse>> AcceptOrder(AcceptedOrderRequest request)
        {
            //Valida usuario
            if (!Guid.TryParse(request.UserId.ToString(), out _))
                return ServiceResponseFactory<AcceptedOrderResponse>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id do usuário inválido!");

            var user = _unitOfWork!.userRepository.GetById(request.UserId).Result;

            if (user == null)
                return ServiceResponseFactory<AcceptedOrderResponse>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Usuário inválido.");

            //Valida pedido
            if (!Guid.TryParse(request.OrderId.ToString(), out _))
                return ServiceResponseFactory<AcceptedOrderResponse>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id do pedido inválido!");

            var order = _unitOfWork!.orderRepository.GetById(request.OrderId).Result;

            if (order == null)
                ServiceResponseFactory<AcceptedOrderResponse>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Pedido inválido.");

            var today = DateOnly.FromDateTime(DateTime.Now);

            //Recupero a partir da lista de usuários que foram notificados na data corrente,
            //aquele que é o usuário entregador que deseja aceitar pedido
            var notificatedUser = (await _unitOfWork.notificatedUserRepository.GetList(x => x.UserId! == request.UserId && x.NotificationDate.Equals(today)))!.ToList();

            //Se o usuário entregador foi localizado
            if (notificatedUser.Any())
            {
                //Recupera a lista de pedidos aceitos
                var acceptedOrders = await _unitOfWork.acceptedOrderRepository.GetList(x => x.UserId == request.UserId);

                //Se o usuário entregador solicitante não tiver nenhuma entrega aceita
                if (!acceptedOrders!.Any())
                {
                    var acceptedOrder = new AcceptedOrder
                    {
                        Id = Guid.NewGuid(),
                        OrderId = request.OrderId,
                        UserId = request.UserId,
                        AcceptedDate = today
                    };

                    //Insere no sistema o aceite de entrega do pedido 
                    var acceptedOrderResult = _unitOfWork.acceptedOrderRepository.Insert(acceptedOrder).Result;
                    var newStatusOrder = _unitOfWork!.statusOrderRepository.GetList(x => x.Name!.ToLower() == GetDescriptionFromEnum.GetFromStatusOrderEnum(EnumStatusOrders.Aceito).ToLower()).Result!.FirstOrDefault();

                    //Atualiza o status do pedido para aceito 
                    order!.StatusOrder = newStatusOrder;
                    await _unitOfWork.orderRepository.Update(order);

                    //Atualiza o usuario, marcando IsDelivering = true
                    user.IsDelivering = true;
                    await _unitOfWork.userRepository.Update(user);

                    _unitOfWork.CommitAsync().Wait();

                    var response = _mapper!.Map<AcceptedOrderResponse>(acceptedOrderResult);

                    return ServiceResponseFactory<AcceptedOrderResponse>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Inclusão de aceite de Pedido Realizado Com Sucesso.", response);
                }
                else
                {
                    return ServiceResponseFactory<AcceptedOrderResponse>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Usuário Já aceitou esse pedido.");
                }
            }
            else
            {
                return ServiceResponseFactory<AcceptedOrderResponse>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não foi possível aceitar o pedido! Verifique os dados enviados.");
            }
        }
    }
}
