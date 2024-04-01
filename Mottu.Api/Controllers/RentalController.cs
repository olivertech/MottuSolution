namespace Mottu.Api.Controllers
{
    [Route("api/Rental")]
    [SwaggerTag("Locação")]
    [ApiController]
    public class RentalController : ControllerBase<Rental, RentalResponse>
    {
        public static IConfiguration? _configuration;

        public RentalController(IUnitOfWork unitOfWork, IMapper? mapper, IConfiguration configuration, IRentalService rentalService)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Locação";
            _configuration = configuration;
            _rentalService = rentalService;
        }

        [HttpGet]
        [Route(nameof(GetAll))]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var result = _rentalService!.GetAll().Result;
            var responseList = _mapper!.Map<IEnumerable<Rental>, IEnumerable<RentalResponse>>(result.Content!);
            return Ok(ResponseFactory<IEnumerable<RentalResponse>>.Success(result.Message!, responseList));
        }

        [HttpGet]
        [Route("Get/{id:Guid}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<RentalResponse>.Error("Id inválido!"));

            var entities = await _unitOfWork!.rentalRepository.GetFullById(id);
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetListByBike))]
        [Produces("application/json")]
        public async Task<IActionResult> GetListByBike(Guid bikeId)
        {
            if (!Guid.TryParse(bikeId.ToString(), out _))
                return BadRequest(ResponseFactory<RentalResponse>.Error("Id da Moto inválida!"));

            //Recupera qualquer locação que tenha o id da moto recebida e que conste como em locação
            var entities = await _unitOfWork!.rentalRepository.GetList(x => x.Bike!.Id == bikeId && x.Bike.IsLeased);
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetCount))]
        [Produces("application/json")]
        public async Task<IActionResult> GetCount()
        {
            var entities = await _unitOfWork!.rentalRepository.Count();
            return Ok(entities);
        }

        [HttpPost]
        [Route(nameof(FinishRental))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(FinishRentalResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(FinishRentalResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(FinishRentalResponse))]
        public ActionResult<FinishRentalResponse> FinishRental([FromBody] FinishRentalRequest request)
        {
            try
            {
                if (request is null)
                    return BadRequest(ResponseFactory<RentalResponse>.Error("Request inválido!"));

                var rental = _unitOfWork!.rentalRepository.GetFullById(request.RentalId).Result;

                if (rental is null)
                    return Ok(ResponseFactory<RentalResponse>.Error(String.Format("{0} não encontrada!", _nomeEntidade)));

                if (!DateOnly.TryParse(request.FinishRentalDate.ToString(), out _))
                    return Ok(ResponseFactory<RentalResponse>.Error(String.Format("Data inválida para fechar {0}!", _nomeEntidade)));

                ///===========================
                /// Calcular valor da locação
                ///===========================
                var rentalValue = CalculateTotalRental.GetRentalValue(rental);

                ///================
                /// Calcular multa
                ///================
                var fine = CalculateTotalRental.GetFineValue(ref rental, request, _configuration!);

                rental.TotalValue = rentalValue + fine;

                //================================================================================================
                //TODO: LINHAS ABAIXO FICARAM COMENTADAS, POR NÃO TER FICADO CLARO SE NA REGRA DE DEVOLUÇÃO DA
                //MOTO, SE JÁ SERÁ ENCERRADA A LOCAÇÃO, OU SE É APENAS UMA CONSULTA DO VALOR TOTAL DA LOCAÇÃO
                //================================================================================================
                //rental.IsActive = false;
                //_unitOfWork.rentalRepository.Update(rental);
                //_unitOfWork.CommitAsync().Wait();

                var response = new FinishRentalResponse(rental, rentalValue + fine);

                return Ok(ResponseFactory<FinishRentalResponse>.Success(String.Format("Total a ser pago pela {0}.", _nomeEntidade), response));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<RentalResponse>.Error(String.Format("Erro ao encerrar {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpPost]
        [Route(nameof(Insert))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(RentalResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(RentalResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(RentalResponse))]
        public ActionResult<RentalResponse> Insert([FromBody] RentalRequest request)
        {
            try
            {
                if (request is null)
                    return BadRequest(ResponseFactory<RentalResponse>.Error("Request inválido!"));

                //Verifico se existem motos livres
                var bikesFree = _unitOfWork!.bikeRepository.GetList(x => x.IsLeased == false).Result;

                if (!bikesFree!.Any())
                    return Ok(ResponseFactory<RentalResponse>.Error(String.Format("Não existem motos disponíveis no momento. Tente mais tarde.", _nomeEntidade)));

                //Verifico se a moto em questão enviada se encontra livre
                var search = _unitOfWork!.rentalRepository.GetList(x => x.Bike!.Id == request.BikeId && x.Bike.IsLeased).Result;

                if (search!.Any())
                    return Ok(ResponseFactory<RentalResponse>.Error(String.Format("Já existe uma {0} em andamento (ativa) com essa moto. Informe outra moto.", _nomeEntidade)));

                //Verifico se o usuário existe
                var entity = _mapper!.Map<Rental>(request);

                var user = _unitOfWork.userRepository.GetFullById(request.UserId).Result;

                if (user == null)
                    return Ok(ResponseFactory<RentalResponse>.Error(String.Format("Usuário informado para {0} não existe. Verifique o id do usuário.", _nomeEntidade)));

                //Verifico se o usuário possui cnh tipo A
                if (user!.CnhType!.Name!.ToLower() != GetDescriptionFromEnum.GetFromStatusCnhType(EnumCnhTypes.A).ToLower())
                    return Ok(ResponseFactory<RentalResponse>.Error("Usuário não possui carteira de motorista do tipo A"));

                //Recupera a moto a ser usada na locação
                var bike = _unitOfWork.bikeRepository.GetById(request.BikeId).Result;

                if (bike == null)
                    return Ok(ResponseFactory<RentalResponse>.Error(String.Format("Moto informada para {0} não existe. Verifique o id da moto.", _nomeEntidade)));

                //Verifico se o plano solicitado existe
                var plan = _unitOfWork.planRepository.GetById(request.PlanId).Result;

                if (plan == null)
                    return Ok(ResponseFactory<RentalResponse>.Error(String.Format("Plano informado para {0} não existe. Verifique o id da plano.", _nomeEntidade)));

                bike!.IsLeased = true;

                entity.Id = Guid.NewGuid();
                entity.Bike = bike;
                entity.User = user;
                entity.Plan = plan;

                entity.CreationDate = DateOnly.FromDateTime(DateTime.Now);
                entity.InitialDate = entity.CreationDate.AddDays(1);
                entity.PredictionDate = DateOnly.FromDateTime(DateTime.Now.AddDays(plan.NumDays));
                entity.EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(plan.NumDays));

                var result = _unitOfWork.rentalRepository.Insert(entity).Result;

                _unitOfWork.CommitAsync().Wait();

                if (result != null)
                {
                    var response = _mapper.Map<RentalResponse>(result);
                    return Ok(ResponseFactory<RentalResponse>.Success(String.Format("Inclusão de {0} Realizada Com Sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<RentalResponse>.Error(String.Format("Não foi possível incluir a {0}! Verifique os dados enviados.", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<RentalResponse>.Error(String.Format("Erro ao inserir a {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpPut]
        [Route(nameof(Update))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(RentalResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status304NotModified, Type = typeof(RentalResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(RentalResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(RentalResponse))]
        public ActionResult<RentalResponse> Update(RentalRequestUpdate request)
        {
            try
            {
                if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                    return BadRequest(ResponseFactory<RentalResponse>.Error("Id informado inválido!"));

                var entity = _unitOfWork!.rentalRepository.GetById(request.Id).Result;

                if (entity is null)
                    return NotFound(ResponseFactory<RentalResponse>.Error("Id informado inválido!"));

                var bike = _unitOfWork!.bikeRepository.GetById(request.BikeId).Result;

                if (bike is null)
                    return NotFound(ResponseFactory<RentalResponse>.Error("Id da moto inválido!"));

                bike.IsLeased = false;

                _mapper!.Map(request, entity);

                var result = _unitOfWork.rentalRepository.Update(entity).Result;

                _unitOfWork.CommitAsync().Wait();

                if (result)
                {
                    var response = _mapper!.Map<RentalResponse>(entity);
                    return Ok(ResponseFactory<RentalResponse>.Success(String.Format("Atualização da {0} realizada com sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status304NotModified, ResponseFactory<RentalResponse>.Error(String.Format("{0} não encontrada para atualização!", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<RentalResponse>.Error(String.Format("Erro ao atualizar a {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(RentalResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(RentalResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(RentalResponse))]
        public IActionResult Delete(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<RentalResponse>.Error("Id inválido!"));

            var entity = _unitOfWork!.rentalRepository.GetById(id).Result;

            if (entity is null)
                return NotFound(ResponseFactory<RentalResponse>.Error("Id informado inválido!"));

            entity.EndDate = DateOnly.FromDateTime(DateTime.Now);
            entity.IsActive = false;

            var result = _unitOfWork.rentalRepository.Update(entity).Result;

            //Recupero a bike usada na locação e atualizo o seu status
            var bike = _unitOfWork!.bikeRepository.GetById(entity.Bike.Id).Result;
            bike!.IsLeased = false;
            _unitOfWork.bikeRepository.Update(bike);

            _unitOfWork.CommitAsync().Wait();

            if (result)
            {
                var response = _mapper!.Map<RentalResponse>(entity);
                return Ok(ResponseFactory<RentalResponse>.Success(String.Format("Remoção de {0} realizada com sucesso.", _nomeEntidade), response));
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseFactory<RentalResponse>.Error(String.Format("{0} não encontrada para remoção!", _nomeEntidade)));
            }
        }
    }
}
