namespace Mottu.Api.Controllers
{
    [Route("api/StatusOrder")]
    [SwaggerTag("Status do Pedido")]
    [ApiController]
    public class StatusOrderController : ControllerBase<StatusOrder, StatusOrderResponse>
    {
        public StatusOrderController(IUnitOfWork unitOfWork, IMapper? mapper, IStatusOrderService statusOrderService) 
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Status do Pedido";
            _statusOrderService = statusOrderService;
        }

        [HttpGet]
        [Route(nameof(GetAll))]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var result = _statusOrderService!.GetAll().Result;
            var responseList = _mapper!.Map<IEnumerable<StatusOrder>, IEnumerable<StatusOrderResponse>>(result.Content!);
            return Ok(ResponseFactory<IEnumerable<StatusOrderResponse>>.Success(result.Message!, responseList));
        }

        [HttpGet]
        [Route("Get/{id:Guid}")]
        [Produces("application/json")]
        public IActionResult GetById(Guid id)
        {
            if (!Guid.TryParse(id.ToString(), out _))
                return BadRequest(ResponseFactory<OrderResponse>.Error("Id inválido!"));

            var result = _statusOrderService!.GetById(id).Result;
            var responseEntity = _mapper!.Map<StatusOrder, StatusOrderResponse>(result.Content!);
            return Ok(ResponseFactory<StatusOrderResponse>.Success(result.Message!, responseEntity));
        }

        [HttpGet]
        [Route(nameof(GetListByName))]
        [Produces("application/json")]
        public IActionResult GetListByName(string name)
        {
            var result = _statusOrderService!.GetListByName(name).Result;
            var responseList = _mapper!.Map<IEnumerable<StatusOrder>, IEnumerable<StatusOrderResponse>>(result.Content!);
            return Ok(ResponseFactory<IEnumerable<StatusOrderResponse>>.Success(result.Message!, responseList));
        }

        [HttpGet]
        [Route(nameof(GetCount))]
        [Produces("application/json")]
        public IActionResult GetCount()
        {
            var result = _statusOrderService!.GetCount().Result;
            return Ok(ResponseFactory<int>.Success(result.Message!, result.Content));
        }

        [HttpPost]
        [Route(nameof(Insert))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(StatusOrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(StatusOrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(StatusOrderResponse))]
        public ActionResult<StatusOrderResponse> Insert([FromBody] StatusOrderRequest request)
        {
            try
            {
                var result = _statusOrderService!.Insert(request).Result;
                var responseEntity = _mapper!.Map<StatusOrder, StatusOrderResponse>(result.Content!);
                return Ok(ResponseFactory<StatusOrderResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<StatusOrderResponse>.Error(String.Format("Erro ao inserir o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpPut]
        [Route(nameof(Update))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(StatusOrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status304NotModified, Type = typeof(StatusOrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(StatusOrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(StatusOrderResponse))]
        public ActionResult<StatusOrderResponse> Update(StatusOrderRequest request)
        {
            try
            {
                var result = _statusOrderService!.Update(request).Result;
                var responseEntity = _mapper!.Map<StatusOrder, StatusOrderResponse>(result.Content!);
                return Ok(ResponseFactory<StatusOrderResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<StatusOrderResponse>.Error(String.Format("Erro ao atualizar o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(StatusOrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(StatusOrderResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(StatusOrderResponse))]
        public IActionResult Delete(StatusOrderRequest request)
        {
            try
            {
                var result = _statusOrderService!.Delete(request).Result;
                var responseEntity = _mapper!.Map<StatusOrder, StatusOrderResponse>(result.Content!);
                return Ok(ResponseFactory<StatusOrderResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<StatusOrderResponse>.Error(String.Format("Erro ao remover o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }
    }
}
