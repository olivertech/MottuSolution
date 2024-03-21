using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Mottu.Api.Controllers.Base;
using Mottu.Application.Interfaces;
using Mottu.Application.Messaging;
using Mottu.Application.Services;
using Mottu.Domain.Entities.Base;
using Mottu.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Mottu.Api.Controllers
{
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
