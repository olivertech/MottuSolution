namespace Mottu.Api.Controllers
{
    [Route("api/Plan")]
    [SwaggerTag("Plano")]
    [ApiController]
    public class PlanController : ControllerBase<Plan, PlanResponse>
    {
        public PlanController(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Plano";
            _planService = new PlanService(_unitOfWork!, _mapper);
        }

        [HttpGet]
        [Route(nameof(GetAll))]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var result = _planService!.GetAll().Result;
            var responseList = _mapper!.Map<IEnumerable<Plan>, IEnumerable<PlanResponse>>(result.Content!);
            return Ok(ResponseFactory<IEnumerable<PlanResponse>>.Success(String.Format("Lista de planos recuperada com sucesso.", _nomeEntidade), responseList));
        }

        [HttpGet]
        [Route("Get/{id:Guid}")]
        [Produces("application/json")]
        public IActionResult GetById(Guid id)
        {
            if (!Guid.TryParse(id.ToString(), out _))
                return BadRequest(ResponseFactory<PlanResponse>.Error("Id inválido!"));

            var result = _planService!.GetById(id).Result;
            var responseEntity = _mapper!.Map<Plan, PlanResponse>(result.Content!);
            return Ok(ResponseFactory<PlanResponse>.Success(result.Message!, responseEntity));
        }

        [HttpGet]
        [Route(nameof(GetListByName))]
        [Produces("application/json")]
        public IActionResult GetListByName(string name)
        {
            var result = _planService!.GetListByName(name).Result;
            var responseList = _mapper!.Map<IEnumerable<Plan>, IEnumerable<PlanResponse>>(result.Content!);
            return Ok(ResponseFactory<IEnumerable<PlanResponse>>.Success(result.Message!, responseList));
        }

        [HttpGet]
        [Route(nameof(GetCount))]
        [Produces("application/json")]
        public async Task<IActionResult> GetCount()
        {
            var result = _planService!.GetCount().Result;
            return Ok(ResponseFactory<int>.Success(result.Message!, result.Content));
        }

        [HttpPost]
        [Route(nameof(Insert))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(PlanResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(PlanResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(PlanResponse))]
        public ActionResult<PlanResponse> Insert([FromBody] PlanRequest request)
        {
            try
            {
                var result = _planService!.Insert(request).Result;
                var responseEntity = _mapper!.Map<Plan, PlanResponse>(result.Content!);
                return Ok(ResponseFactory<PlanResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<PlanResponse>.Error(String.Format("Erro ao inserir o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpPut]
        [Route(nameof(Update))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(PlanResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status304NotModified, Type = typeof(PlanResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(PlanResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(PlanResponse))]
        public ActionResult<PlanResponse> Update(PlanRequest request)
        {
            try
            {
                var result = _planService!.Update(request).Result;
                var responseEntity = _mapper!.Map<Plan, PlanResponse>(result.Content!);
                return Ok(ResponseFactory<PlanResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<PlanResponse>.Error(String.Format("Erro ao atualizar o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(PlanResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(PlanResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(PlanResponse))]
        public IActionResult Delete(PlanRequest request)
        {
            try
            {
                var result = _planService!.Delete(request).Result;
                var responseEntity = _mapper!.Map<Plan, PlanResponse>(result.Content!);
                return Ok(ResponseFactory<PlanResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<UserTypeResponse>.Error(String.Format("Erro ao remover o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }
    }
}
