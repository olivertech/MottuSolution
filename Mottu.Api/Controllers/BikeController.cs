namespace Mottu.Api.Controllers
{
    [Route("api/Bike")]
    [SwaggerTag("Moto")]
    [ApiController]
    public class BikeController : ControllerBase<Bike, BikeResponse>
    {
        public BikeController(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Moto";
            _bikeService = new BikeService(_unitOfWork!, _mapper);
        }

        [HttpPost]
        [Route(nameof(GetAll))]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var result = _bikeService!.GetAll().Result;
            var responseList = _mapper!.Map<IEnumerable<Bike>, IEnumerable<BikeResponse>>(result.Content!);
            return Ok(ResponseFactory<IEnumerable<BikeResponse>>.Success(result.Message!, responseList));
        }

        [HttpGet]
        [Route("Get/{id:Guid}")]
        [Produces("application/json")]
        public IActionResult GetById(Guid id)
        {
            var result = _bikeService!.GetById(id).Result;
            var responseEntity = _mapper!.Map<Bike, BikeResponse>(result.Content!);
            return Ok(ResponseFactory<BikeResponse>.Success(result.Message!, responseEntity));
        }

        [HttpPost]
        [Route(nameof(GetListByPlate))]
        [Produces("application/json")]
        public IActionResult GetListByPlate(BikeByPlateRequest request)
        {
            var result = _bikeService!.GetListByPlate(request).Result;
            var responseList = _mapper!.Map<IEnumerable<Bike>, IEnumerable<BikeResponse>>(result.Content!);
            return Ok(ResponseFactory<IEnumerable<BikeResponse>>.Success(result.Message!, responseList));
        }

        [HttpGet]
        [Route(nameof(GetCount))]
        [Produces("application/json")]
        public IActionResult GetCount()
        {
            var result = _bikeService!.GetCount().Result;
            return Ok(ResponseFactory<int>.Success(result.Message!, result.Content));
        }

        [HttpPost]
        [Route(nameof(Insert))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(BikeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(BikeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(BikeResponse))]
        public ActionResult<BikeResponse> Insert([FromBody] BikeRequest request)
        {
            try
            {
                var result = _bikeService!.Insert(request).Result;
                var responseEntity = _mapper!.Map<Bike, BikeResponse>(result.Content!);
                return Ok(ResponseFactory<BikeResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<BikeResponse>.Error(String.Format("Erro ao inserir o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpPut]
        [Route(nameof(Update))]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(BikeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status304NotModified, Type = typeof(BikeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(BikeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(BikeResponse))]
        public ActionResult<BikeResponse> Update(BikeRequestUpdate requestUpdate)
        {
            try
            {
                BikeRequest request = new BikeRequest
                {
                    Id = requestUpdate.Id,
                    Model = requestUpdate.Model,
                    Plate = requestUpdate.Plate,
                    Year = requestUpdate.Year,
                    RequestUserId = requestUpdate.RequestUserId
                };

                var result = _bikeService!.Update(request).Result;
                var responseEntity = _mapper!.Map<Bike, BikeResponse>(result.Content!);
                return Ok(ResponseFactory<BikeResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<BikeResponse>.Error(String.Format("Erro ao atualizar a {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(BikeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(BikeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(BikeResponse))]
        public IActionResult Delete(BikeRequestDelete requestDelete)
        {
            try
            {
                BikeRequest request = new BikeRequest
                {
                    Id = requestDelete.Id,
                    RequestUserId = requestDelete.RequestUserId
                };

                var result = _bikeService!.Delete(request).Result;
                var responseEntity = _mapper!.Map<Bike, BikeResponse>(result.Content!);
                return Ok(ResponseFactory<BikeResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<BikeResponse>.Error(String.Format("Erro ao remover o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }
    }
}
