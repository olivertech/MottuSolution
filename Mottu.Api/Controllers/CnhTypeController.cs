using Mottu.Infrastructure.Repositories;

namespace Mottu.Api.Controllers
{
    [Route("api/CnhType")]
    [SwaggerTag("Tipo de CNH")]
    [ApiController]
    public class CnhTypeController : ControllerBase<CnhType, CnhTypeResponse>
    {
        public CnhTypeController(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Tipo de CNH";
            _cnhTypeService = new CnhTypeService(_unitOfWork!, _mapper);
        }

        [HttpGet]
        [Route(nameof(GetAll))]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var result = _cnhTypeService!.GetAll().Result;
            var responseList = _mapper!.Map<IEnumerable<CnhType>, IEnumerable<CnhTypeResponse>>(result.Content!);
            return Ok(ResponseFactory<IEnumerable<CnhTypeResponse>>.Success(result.Message!, responseList));
        }

        [HttpGet]
        [Route("Get/{id:Guid}")]
        [Produces("application/json")]
        public IActionResult GetById(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<CnhTypeResponse>.Error("Id inválido!"));

            var result = _cnhTypeService!.GetById(id).Result;
            var responseEntity = _mapper!.Map<CnhType, CnhTypeResponse>(result.Content!);
            return Ok(ResponseFactory<CnhTypeResponse>.Success(result.Message!, responseEntity)); 
        }

        [HttpGet]
        [Route(nameof(GetListByName))]
        [Produces("application/json")]
        public IActionResult GetListByName(string name)
        {
            var result = _cnhTypeService!.GetListByName(name).Result;
            var responseList = _mapper!.Map<IEnumerable<CnhType>, IEnumerable<CnhTypeResponse>>(result.Content!);
            return Ok(ResponseFactory<IEnumerable<CnhTypeResponse>>.Success(result.Message!, responseList));
        }

        [HttpGet]
        [Route(nameof(GetCount))]
        [Produces("application/json")]
        public IActionResult GetCount()
        {
            var result = _cnhTypeService!.GetCount().Result;
            return Ok(ResponseFactory<int>.Success(result.Message!, result.Content));
        }

        [HttpPost]
        [Route(nameof(Insert))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(CnhTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(CnhTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(CnhTypeResponse))]
        public ActionResult<CnhTypeResponse> Insert([FromBody] CnhTypeRequest request)
        {
            try
            {
                var result = _cnhTypeService!.Insert(request).Result;
                var responseEntity = _mapper!.Map<CnhType, CnhTypeResponse>(result.Content!);
                return Ok(ResponseFactory<CnhTypeResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<CnhTypeResponse>.Error(String.Format("Erro ao inserir a {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpPut]
        [Route(nameof(Update))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(CnhTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status304NotModified, Type = typeof(CnhTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(CnhTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(CnhTypeResponse))]
        public ActionResult<CnhTypeResponse> Update(CnhTypeRequest request)
        {
            try
            {
                var result = _cnhTypeService!.Update(request).Result;
                var responseEntity = _mapper!.Map<CnhType, CnhTypeResponse>(result.Content!);
                return Ok(ResponseFactory<CnhTypeResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<CnhTypeResponse>.Error(String.Format("Erro ao atualizar a {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(CnhTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(CnhTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(CnhTypeResponse))]
        public IActionResult Delete(CnhTypeRequest request)
        {
            try
            {
                var result = _cnhTypeService!.Delete(request).Result;
                var responseEntity = _mapper!.Map<CnhType, CnhTypeResponse>(result.Content!);
                return Ok(ResponseFactory<CnhTypeResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<StatusOrderResponse>.Error(String.Format("Erro ao remover o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }
    }
}
