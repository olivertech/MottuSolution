using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Mottu.CrossCutting.Messaging;
using Mottu.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Mottu.Api.Controllers
{
    [Route("api/PlaceOrderNotification")]
    [SwaggerTag("ColocarPedido")]
    [ApiController]
    public class PlaceOrderNotificationController : BaseController
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PlaceOrderNotificationController(IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Notificação de Pedido";
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        [Route(nameof(Send))]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult Send([FromBody] NotificationMessage request)
        {
            _publishEndpoint.Publish(request);

            return Ok();
        }
    }
}
