﻿using Mottu.Application.Messaging;

namespace Mottu.Api.Controllers
{
    /// <summary>
    /// Controller usada pelo RabbitMQ Producer,
    /// para envio das mensagens em broadcast
    /// </summary>
    [Route("api/PlaceOrderNotification")]
    [SwaggerTag("ColocarPedido")]
    [ApiController]
    public class PlaceOrderNotificationController : Controller
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PlaceOrderNotificationController(IPublishEndpoint publishEndpoint)
        {
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
