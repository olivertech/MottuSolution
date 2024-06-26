﻿namespace Mottu.Api.Controllers
{
    [Route("api/AcceptedOrder")]
    [SwaggerTag("PedidosAceitos")]
    [ApiController]
    public class AcceptedOrderController : ControllerBase<AcceptedOrder, AcceptedOrderResponse>
    {
        public AcceptedOrderController(IUnitOfWork unitOfWork, IMapper? mapper, IAcceptedOrderService acceptedOrderService)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Pedido";
            _acceptedOrderService = acceptedOrderService;
        }

        [HttpPost]
        [Route(nameof(AcceptOrder))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(AppUserResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(AppUserResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(AppUserResponse))]
        public IActionResult AcceptOrder(AcceptedOrderRequest request)
        {
            var service = _acceptedOrderService!.AcceptOrder(request).Result;

            if (service.StatusCode != EnumStatusCode.Status200OK)
            {
                return BadRequest(ResponseFactory<ListNotificatedUsersResponse>.Error(service.Message!));
            }
            else
            {
                return Ok(ResponseFactory<AcceptedOrderResponse>.Success(service.Message!, service.Content!));
            }
        }
    }
}
