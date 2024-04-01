namespace Mottu.Api.Controllers
{
    [Route("api/UserType")]
    [SwaggerTag("Tipo de Usuário")]
    [ApiController]
    public class UserTypeController : ControllerBase<UserType, UserTypeResponse>
    {
        public UserTypeController(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Tipo de Usuário";
            _userTypeService = new UserTypeService(_unitOfWork!, _mapper);
        }

        [HttpGet]
        [Route(nameof(GetAll))]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var result = _userTypeService!.GetAll().Result;
            var responseList = _mapper!.Map<IEnumerable<UserType>, IEnumerable<UserTypeResponse>>(result.Content!);
            return Ok(ResponseFactory<IEnumerable<UserTypeResponse>>.Success(result.Message!, responseList));
        }

        [HttpGet]
        [Route("Get/{id:Guid}")]
        [Produces("application/json")]
        public IActionResult GetById(Guid id)
        {
            if (!Guid.TryParse(id.ToString(), out _))
                return BadRequest(ResponseFactory<OrderResponse>.Error("Id inválido!"));

            var result = _userTypeService!.GetById(id).Result;
            var responseEntity = _mapper!.Map<UserType, UserTypeResponse>(result.Content!);
            return Ok(ResponseFactory<UserTypeResponse>.Success(result.Message!, responseEntity));
        }

        [HttpGet]
        [Route(nameof(GetListByName))]
        [Produces("application/json")]
        public IActionResult GetListByName(string name)
        {
            var result = _userTypeService!.GetListByName(name).Result;
            var responseList = _mapper!.Map<IEnumerable<UserType>, IEnumerable<UserTypeResponse>>(result.Content!);
            return Ok(ResponseFactory<IEnumerable<UserTypeResponse>>.Success(result.Message!, responseList));
        }

        [HttpGet]
        [Route(nameof(GetCount))]
        [Produces("application/json")]
        public IActionResult GetCount()
        {
            var result = _userTypeService!.GetCount().Result;
            return Ok(ResponseFactory<int>.Success(result.Message!, result.Content));
        }

        [HttpPost]
        [Route(nameof(Insert))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(UserTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(UserTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(UserTypeResponse))]
        public ActionResult<UserTypeResponse> Insert([FromBody] UserTypeRequest request)
        {
            try
            {
                var result = _userTypeService!.Insert(request).Result;
                var responseEntity = _mapper!.Map<UserType, UserTypeResponse>(result.Content!);
                return Ok(ResponseFactory<UserTypeResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<UserTypeResponse>.Error(String.Format("Erro ao inserir o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpPut]
        [Route(nameof(Update))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(UserTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status304NotModified, Type = typeof(UserTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(UserTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(UserTypeResponse))]
        public ActionResult<UserTypeResponse> Update(UserTypeRequest request)
        {
            try
            {
                var result = _userTypeService!.Update(request).Result;
                var responseEntity = _mapper!.Map<UserType, UserTypeResponse>(result.Content!);
                return Ok(ResponseFactory<UserTypeResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<UserTypeResponse>.Error(String.Format("Erro ao atualizar o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(UserTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(UserTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(UserTypeResponse))]
        public IActionResult Delete(UserTypeRequest request)
        {
            try
            {
                var result = _userTypeService!.Delete(request).Result;
                var responseEntity = _mapper!.Map<UserType, UserTypeResponse>(result.Content!);
                return Ok(ResponseFactory<UserTypeResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<UserTypeResponse>.Error(String.Format("Erro ao remover o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }
    }
}
