using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Mottu.Api.Controllers.Base;
using Mottu.Application.Helpers;
using Mottu.Application.Messaging;
using Mottu.Application.Requests;
using Mottu.Application.Responses;
using Mottu.Application.Services;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Annotations;

namespace Mottu.Api.Controllers
{
    [Route("api/Order")]
    [SwaggerTag("Pedido")]
    [ApiController]
    public class OrderController : ControllerBase<Order, OrderResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper? mapper;
        private readonly IBus _bus;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IOrderNotificationService _orderNotificationService;
        private readonly INotificationMessage _notificationMessage;

        public OrderController(IUnitOfWork unitOfWork,
                               IMapper? mapper,
                               IBus bus,
                               IPublishEndpoint publishEndpoint,
                               IOrderNotificationService orderNotificationService,
                               INotificationMessage notificationMessage)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Pedido";
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            _bus = bus;
            _publishEndpoint = publishEndpoint;
            _orderNotificationService = orderNotificationService;
            _notificationMessage = notificationMessage;
            _orderService = new OrderService(_unitOfWork!, _mapper);
        }

        [HttpGet]
        [Route(nameof(GetAll))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _unitOfWork!.orderRepository.GetAll();
            return Ok(list);
        }

        [HttpGet]
        [Route("Get/{id:Guid}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<OrderResponse>.Error(false, "Id inválido!"));

            var entities = await _unitOfWork!.orderRepository.GetById(id);
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetListByDate))]
        [Produces("application/json")]
        public async Task<IActionResult> GetListByDate(DateOnly date)
        {
            if (date == DateOnly.MinValue)
                return BadRequest(ResponseFactory<OrderResponse>.Error(false, "Data inválida!"));

            var entities = await _unitOfWork!.orderRepository.GetList(x => x.DateOrder == date);
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetCount))]
        [Produces("application/json")]
        public async Task<IActionResult> GetCount()
        {
            var entities = await _unitOfWork!.orderRepository.Count();
            return Ok(entities);
        }

        [HttpPost]
        [Route(nameof(Insert))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(OrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(OrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(OrderResponse))]
        public ActionResult<OrderResponse> Insert([FromBody] OrderRequest request)
        {
            try
            {
                if (request is null)
                    return BadRequest(ResponseFactory<OrderResponse>.Error(false, "Request inválido!"));

                //Valido solicitante da requisição
                var requester = _unitOfWork!.userRepository.GetFullById(request.RequestUserId).Result;

                if (requester is null)
                    return BadRequest(ResponseFactory<OrderResponse>.Error(false, "Request inválido!"));

                if (requester.UserType!.Name!.ToLower() != GetDescriptionFromEnum.GetFromUserTypeEnum(EnumUserTypes.Administrador).ToLower())
                    return BadRequest(ResponseFactory<OrderResponse>.Error(false, "Usuário solicitante inválido!"));

                var entity = _mapper!.Map<Order>(request);

                entity.Id = Guid.NewGuid();

                //Por padrão, um novo pedido recebe sempre o status de "disponivel"
                entity.StatusOrder = _unitOfWork!.statusOrderRepository.GetList(x => x.Name!.ToLower() == "disponível").Result!.FirstOrDefault();

                //Registro a data do pedido
                entity.DateOrder = DateOnly.FromDateTime(DateTime.Now);

                var result = _unitOfWork!.orderRepository.Insert(entity);

                _unitOfWork.CommitAsync().Wait();

                _notificationMessage.Id = entity.Id;
                _notificationMessage.Description = entity.Description;
                _notificationMessage.ValueOrder = entity.ValueOrder;
                _notificationMessage.DateOrder = entity.DateOrder;
                _notificationMessage.StatusOrderId = entity.StatusOrder!.Id;

                //================================
                //Notifica o broker de mensageria
                //================================
                _orderNotificationService.OnOrderNotificationAsync(_notificationMessage);

                if (result != null)
                {
                    var response = _mapper.Map<OrderResponse>(entity);
                    return Ok(ResponseFactory<OrderResponse>.Success(true, String.Format("Inclusão de {0} Realizado Com Sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<OrderResponse>.Error(false, String.Format("Não foi possível incluir o {0}! Verifique os dados enviados.", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<OrderResponse>.Error(false, String.Format("Erro ao inserir o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpPut]
        [Route(nameof(Update))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(OrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status304NotModified, Type = typeof(OrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(OrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(OrderResponse))]
        public ActionResult<OrderResponse> Update(OrderRequest request)
        {
            try
            {
                if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                    return BadRequest(ResponseFactory<OrderResponse>.Error(false, "Id informado inválido!"));

                var entity = _unitOfWork!.orderRepository.GetById(request.Id).Result;

                if (entity is null)
                    return NotFound(ResponseFactory<OrderResponse>.Error(false, "Id informado inválido!"));

                _mapper!.Map(request, entity);

                var result = _unitOfWork.orderRepository.Update(entity).Result;

                _unitOfWork.CommitAsync().Wait();

                if (result)
                {
                    var response = _mapper!.Map<OrderResponse>(entity);
                    return Ok(ResponseFactory<OrderResponse>.Success(true, String.Format("Atualização do {0} realizada com sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status304NotModified, ResponseFactory<OrderResponse>.Error(false, String.Format("{0} não encontrado para atualização!", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<OrderResponse>.Error(false, String.Format("Erro ao atualizar a {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpPut]
        [Route(nameof(UpdateStatusOrder))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(OrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status304NotModified, Type = typeof(OrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(OrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(OrderResponse))]
        public ActionResult<OrderResponse> UpdateStatusOrder(OrderRequestUpdate request)
        {
            try
            {
                if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                    return BadRequest(ResponseFactory<OrderResponse>.Error(false, "Id informado inválido!"));

                var entity = _unitOfWork!.orderRepository.GetById(request.Id).Result;

                if (entity is null)
                    return NotFound(ResponseFactory<OrderResponse>.Error(false, "Id informado inválido!"));

                var statusOrder = _unitOfWork!.statusOrderRepository.GetById(request.StatusOrderId).Result;

                if (statusOrder is null)
                    return NotFound(ResponseFactory<OrderResponse>.Error(false, "Id informando da situação do pedido inválido!"));

                entity.StatusOrder = statusOrder;

                var result = _unitOfWork.orderRepository.Update(entity).Result;

                _unitOfWork.CommitAsync().Wait();

                if (result)
                {
                    var response = _mapper!.Map<OrderResponse>(entity);
                    return Ok(ResponseFactory<OrderResponse>.Success(true, String.Format("Atualização do {0} realizada com sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status304NotModified, ResponseFactory<OrderResponse>.Error(false, String.Format("{0} não encontrado para atualização!", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<OrderResponse>.Error(false, String.Format("Erro ao atualizar a {0} -> ", _nomeEntidade) + ex.Message));
            }
        }
    }
}
