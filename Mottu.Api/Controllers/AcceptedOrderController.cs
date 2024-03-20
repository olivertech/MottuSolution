using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mottu.Application.Services;
using Mottu.CrossCutting.Helpers;
using Mottu.CrossCutting.Responses;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Mottu.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace Mottu.Api.Controllers
{
    [Route("api/AcceptedOrder")]
    [SwaggerTag("PedidosAceitos")]
    [ApiController]
    public class AcceptedOrderController : BaseController
    {
        public AcceptedOrderController(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Pedido";
        }

        [HttpPost]
        [Route(nameof(AcceptOrder))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(AppUserResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(AppUserResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(AppUserResponse))]
        public async Task<IActionResult> AcceptOrder(Guid userId, Guid orderId)
        {
            //var service = new AcceptedOrderService(_unitOfWork!, _mapper).AcceptOrder(userId, orderId);

            //if(!service.Result)
            //{
            //    BadRequestObjectResult badRequest;

            //    switch (service.StatusCode)
            //    {
            //        case EnumStatusCode.Status200OK:
            //            break;
            //        case EnumStatusCode.Status400BadRequest:
            //            badRequest = BadRequest(ResponseFactory<AppUserResponse>.Error(false, service.Message!));
            //            break;
            //        case EnumStatusCode.Status500InternalServerError:
            //            break;
            //    }
            //}
            //Valida usuario
            if (!Guid.TryParse(userId.ToString(), out _))
                return BadRequest(ResponseFactory<AppUserResponse>.Error(false, "Id do usuário inválido!"));

            var user = _unitOfWork!.userRepository.GetById(userId).Result;

            if (user == null)
                return BadRequest(ResponseFactory<AppUserResponse>.Error(false, "Usuário inválido."));

            //Valida pedido
            if (!Guid.TryParse(orderId.ToString(), out _))
                return BadRequest(ResponseFactory<AppUserResponse>.Error(false, "Id do pedido inválido!"));

            var order = _unitOfWork!.orderRepository.GetById(orderId).Result;

            if (order == null)
                return BadRequest(ResponseFactory<AppUserResponse>.Error(false, "Pedido inválido."));

            var today = DateOnly.FromDateTime(DateTime.Now);

            //Recupero da lista de usuários que foram notificados, o usuário entregador que deseja aceitar pedido do dia
            var notificatedUser = await _unitOfWork!.notificatedUserRepository.GetList(x => x.UserId == userId && x.NotificationDate.Equals(today));

            //Caso o usuário entregador esteja na lista dos notificados
            //Registrar que ele aceitou o pedido
            if (notificatedUser!.Any())
            {
                var acceptedOrders = await _unitOfWork.acceptedOrderRepository.GetList(x => x.UserId == userId);

                if (!acceptedOrders!.Any())
                {
                    var acceptedOrder = new AcceptedOrder
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderId,
                        UserId = userId,
                        AcceptedDate = today
                    };

                    //Insere 
                    var acceptedOrderResult = _unitOfWork.acceptedOrderRepository.Insert(acceptedOrder).Result;
                    var newStatusOrder = _unitOfWork!.statusOrderRepository.GetList(x => x.Name!.ToLower() == GetDescriptionFromEnum.GetFromStatusOrderEnum(EnumStatusOrders.Aceito).ToLower()).Result!.FirstOrDefault();

                    //Atualiza o status do pedido para aceito 
                    order.StatusOrder = newStatusOrder;
                    await _unitOfWork.orderRepository.Update(order);

                    //Atualiza o usuario, marcando IsDelivering = true
                    user.IsDelivering = true;
                    await _unitOfWork.userRepository.Update(user);

                    _unitOfWork.CommitAsync().Wait();

                    var response = _mapper!.Map<AcceptedOrderResponse>(acceptedOrderResult);
                    return Ok(ResponseFactory<AcceptedOrderResponse>.Success(true, String.Format("Inclusão de aceite de {0} Realizado Com Sucesso.", _nomeEntidade), response!));
                }
                else
                {
                    return BadRequest(ResponseFactory<AcceptedOrderResponse>.Error(false, "Usuário Já aceitou esse pedido."));
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory<AcceptedOrderResponse>.Error(false, "Não foi possível aceitar o pedido! Verifique os dados enviados."));
            }
        }
    }
}
