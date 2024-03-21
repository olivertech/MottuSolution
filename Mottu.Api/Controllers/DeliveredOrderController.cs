using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Minio.DataModel.Notification;
using Mottu.Api.Controllers.Base;
using Mottu.Application.Helpers;
using Mottu.Application.Interfaces;
using Mottu.Application.Responses;
using Mottu.Application.Services;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Mottu.Api.Controllers
{
    [Route("api/DeliveredOrder")]
    [SwaggerTag("PedidosEntregues")]
    [ApiController]
    public class DeliveredOrderController : ControllerBase<DeliveredOrder, DeliveredOrderResponse>
    {
        public DeliveredOrderController(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Pedido";
            _deliveredOrderService = new DeliveredOrderService(_unitOfWork!, _mapper);
        }

        [HttpPost]
        [Route(nameof(DeliverOrder))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(DeliveredOrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(DeliveredOrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(DeliveredOrderResponse))]
        public async Task<IActionResult> DeliverOrder(Guid userId, Guid orderId)
        {
            //Valida usuario
            if (!Guid.TryParse(userId.ToString(), out _))
                return BadRequest(ResponseFactory<DeliveredOrderResponse>.Error("Id do usuário inválido!"));

            var user = _unitOfWork!.userRepository.GetById(userId).Result;

            if (user == null)
                return BadRequest(ResponseFactory<DeliveredOrderResponse>.Error("Usuário inválido."));

            //Valida pedido
            if (!Guid.TryParse(orderId.ToString(), out _))
                return BadRequest(ResponseFactory<DeliveredOrderResponse>.Error("Id do pedido inválido!"));

            var order = _unitOfWork!.orderRepository.GetById(orderId).Result;

            if (order == null)
                return BadRequest(ResponseFactory<DeliveredOrderResponse>.Error("Pedido inválido."));

            var today = DateOnly.FromDateTime(DateTime.Now);

            //Recupero da lista de usuários que foram notificados, o usuário entregador que deseja aceitar pedido do dia
            var notificatedUser = await _unitOfWork!.notificatedUserRepository.GetList(x => x.UserId == userId && x.NotificationDate.Equals(today));

            //Caso o usuário entregador esteja na lista dos notificados
            //Registrar que ele aceitou o pedido
            if (notificatedUser!.Any())
            {
                var deliveredOrders = await _unitOfWork.deliveredOrderRepository.GetList(x => x.UserId == userId);

                if (!deliveredOrders!.Any())
                {
                    var deliveredOrder = new DeliveredOrder
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderId,
                        UserId = userId,
                        DeliveredDate = today
                    };

                    //Insere 
                    var deliveredOrderResult = _unitOfWork.deliveredOrderRepository.Insert(deliveredOrder).Result;
                    var newStatusOrder = _unitOfWork!.statusOrderRepository.GetList(x => x.Name!.ToLower() == GetDescriptionFromEnum.GetFromStatusOrderEnum(EnumStatusOrders.Entregue).ToLower()).Result!.FirstOrDefault();

                    //Atualiza o status do pedido para entregue
                    order.StatusOrder = newStatusOrder;
                    await _unitOfWork.orderRepository.Update(order);

                    //Atualiza o usuario, marcando IsDelivering = false
                    user.IsDelivering = false;
                    await _unitOfWork.userRepository.Update(user);

                    _unitOfWork.CommitAsync().Wait();

                    var response = _mapper!.Map<DeliveredOrderResponse>(deliveredOrderResult);
                    return Ok(ResponseFactory<DeliveredOrderResponse>.Success(String.Format("Inclusão de entrega de {0} Realizado Com Sucesso.", _nomeEntidade), response!));
                }
                else
                {
                    return BadRequest(ResponseFactory<DeliveredOrderResponse>.Error("Usuário Já entregou esse pedido."));
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, ResponseFactory<DeliveredOrderResponse>.Error("Não foi possível entregar o pedido! Verifique os dados enviados."));
            }
        }
    }
}
