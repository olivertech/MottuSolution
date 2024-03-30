namespace Mottu.Api.Controllers
{
    [Route("api/User")]
    [SwaggerTag("Usuário")]
    [ApiController]
    public class AppUserController : ControllerBase<AppUser, AppUserResponse>
    {
        public AppUserController(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Usuário";
            _appUserService = new AppUserService(_unitOfWork!, _mapper);
        }

        [HttpGet]
        [Route(nameof(GetAll))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var convert = new ConvertModelToResponse<AppUser, AppUserResponse>(_mapper);

            var list = await _unitOfWork!.userRepository.GetFullAll();
            List<AppUserResponse> result = convert.GetResponsList(list!);
            
            return Ok(result);
        }

        [HttpGet]
        [Route("Get/{id:Guid}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<AppUserResponse>.Error("Id inválido!"));

            var entities = await _unitOfWork!.userRepository.GetFullById(id);
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetListByName))]
        [Produces("application/json")]
        public async Task<IActionResult> GetListByName(string name)
        {
            if (name is null)
                return BadRequest(ResponseFactory<AppUserResponse>.Error("Nome inválido!"));

            var entities = await _unitOfWork!.userRepository.GetFullList(name);
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetCount))]
        [Produces("application/json")]
        public async Task<IActionResult> GetCount()
        {
            var entities = await _unitOfWork!.userRepository.Count();
            return Ok(entities);
        }

        [HttpPost]
        [Route(nameof(Insert))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(AppUserResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(AppUserResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(AppUserResponse))]
        public ActionResult<AppUserResponse> Insert([FromBody] AppUserRequest request)
        {
            try
            {
                if (request is null)
                    return BadRequest(ResponseFactory<AppUserResponse>.Error("Request inválido!"));

                var search = _unitOfWork!.userRepository.GetAll().Result;

                //Verifica se já existe o cnpj informado
                if (search!.Any(x => x.Cnpj == request.Cnpj))
                    return Ok(ResponseFactory<AppUserResponse>.Error(String.Format("Já existe um {0} com o CNPJ informado.", _nomeEntidade)));

                //Verifica se já existe o cnh informado
                if (search!.Any(x => x.Cnh == request.Cnh))
                    return Ok(ResponseFactory<AppUserResponse>.Error(String.Format("Já existe um {0} com o CNH informado.", _nomeEntidade)));

                //Verifica se o cnh é de um tipo válido
                var cnhType = _unitOfWork.cnhTypeRepository.GetById(request.CnhTypeId).Result;

                if (cnhType == null)
                    return Ok(ResponseFactory<AppUserResponse>.Error(String.Format("Tipo de CNH inválido.", _nomeEntidade)));

                //Verifica se o tipo do usuário é válido
                var userType = _unitOfWork.userTypeRepository.GetById(request.UserTypeId).Result;

                if(userType == null)
                    return Ok(ResponseFactory<AppUserResponse>.Error(String.Format("Tipo de Usuário inválido.", _nomeEntidade)));

                var entity = _mapper!.Map<AppUser>(request);

                entity.Id = Guid.NewGuid();
                entity.UserType = userType;
                entity.CnhType = cnhType!;
                entity.IsActive = true;

                var result = _unitOfWork.userRepository.Insert(entity).Result;

                _unitOfWork.CommitAsync().Wait();

                if (result != null)
                {
                    var response = _mapper.Map<AppUserResponse>(entity);
                    return Ok(ResponseFactory<AppUserResponse>.Success(String.Format("Inclusão de {0} Realizado Com Sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<AppUserResponse>.Error(String.Format("Não foi possível incluir o {0}! Verifique os dados enviados.", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<AppUserResponse>.Error(String.Format("Erro ao inserir o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpPut]
        [Route(nameof(Update))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(AppUserResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status304NotModified, Type = typeof(AppUserResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(AppUserResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(AppUserResponse))]
        public ActionResult<AppUserResponse> Update(AppUserRequest request)
        {
            try
            {
                if (request is null)
                    return BadRequest(ResponseFactory<AppUserResponse>.Error("Request inválido!"));

                var appUser = _unitOfWork!.userRepository.GetById(request.Id).Result;

                if (appUser == null)
                    return BadRequest(ResponseFactory<AppUserResponse>.Error(String.Format("Request inválido.", _nomeEntidade)));

                var search = _unitOfWork!.userRepository.GetAll().Result;

                //Verifica se já existe o cnpj informado
                if (search!.Any(x => x.Cnpj == request.Cnpj && x.Id != request.Id))
                    return Ok(ResponseFactory<AppUserResponse>.Error(String.Format("Já existe um {0} com o CNPJ informado.", _nomeEntidade)));

                //Verifica se já existe o cnh informado
                if (search!.Any(x => x.Cnh == request.Cnh && x.Id != request.Id))
                    return Ok(ResponseFactory<AppUserResponse>.Error(String.Format("Já existe um {0} com o CNH informado.", _nomeEntidade)));

                //Verifica se o cnh é de um tipo válido
                var cnhType = _unitOfWork.cnhTypeRepository.GetById(request.CnhTypeId).Result;

                if (cnhType == null)
                    return Ok(ResponseFactory<AppUserResponse>.Error(String.Format("Tipo de CNH inválido.", _nomeEntidade)));

                //Verifica se o tipo do usuário é válido
                var userType = _unitOfWork.userTypeRepository.GetById(request.UserTypeId).Result;

                if (userType == null)
                    return Ok(ResponseFactory<AppUserResponse>.Error(String.Format("Tipo de Usuário inválido.", _nomeEntidade)));

                _mapper!.Map(request, appUser);

                appUser.UserType = userType;
                appUser.CnhType = cnhType!;

                var result = _unitOfWork.userRepository.Update(appUser).Result;

                _unitOfWork.CommitAsync().Wait();

                if (result)
                {
                    var response = _mapper!.Map<AppUserResponse>(appUser);
                    return Ok(ResponseFactory<AppUserResponse>.Success(String.Format("Atualização do {0} realizada com sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status304NotModified, ResponseFactory<AppUserResponse>.Error(String.Format("{0} não encontrado para atualização!", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<AppUserResponse>.Error(String.Format("Erro ao atualizar a {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(AppUserResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(AppUserResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(AppUserResponse))]
        public IActionResult Delete(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<AppUserResponse>.Error("Id informado igual a 0!"));

            var entity = _unitOfWork!.userRepository.GetById(id).Result;

            if (entity is null)
                return NotFound(ResponseFactory<AppUserResponse>.Error("Id informado inválido!"));

            var result = _unitOfWork.userRepository.Delete(id).Result;

            _unitOfWork.CommitAsync().Wait();

            if (result)
            {
                var response = _mapper!.Map<AppUserResponse>(entity);
                return Ok(ResponseFactory<AppUserResponse>.Success(String.Format("Remoção de {0} realizada com sucesso.", _nomeEntidade), response));
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseFactory<AppUserResponse>.Error(String.Format("{0} não encontrada para remoção!", _nomeEntidade)));
            }
        }
    }
}
