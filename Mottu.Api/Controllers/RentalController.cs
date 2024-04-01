using System.Xml.Linq;
using static MassTransit.ValidationResultExtensions;

namespace Mottu.Api.Controllers
{
    [Route("api/Rental")]
    [SwaggerTag("Locação")]
    [ApiController]
    public class RentalController : ControllerBase<Rental, RentalResponse>
    {
        public RentalController(IUnitOfWork unitOfWork, IMapper? mapper, IRentalService rentalService)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Locação";
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
        public IActionResult GetById(Guid id)
        {
            if (!Guid.TryParse(id.ToString(), out _))
                return BadRequest(ResponseFactory<RentalResponse>.Error("Id inválido!"));

            var result = _rentalService!.GetById(id).Result;
            var responseEntity = _mapper!.Map<Rental, RentalResponse>(result.Content!);
            return Ok(ResponseFactory<RentalResponse>.Success(result.Message!, responseEntity));
        }

        [HttpGet]
        [Route(nameof(GetListByBike))]
        [Produces("application/json")]
        public IActionResult GetListByBike(Guid bikeId)
        {
            if (!Guid.TryParse(bikeId.ToString(), out _))
                return BadRequest(ResponseFactory<RentalResponse>.Error("Id inválido!"));

            var result = _rentalService!.GetListByBike(bikeId).Result;
            var responseList = _mapper!.Map<IEnumerable<Rental>, IEnumerable<RentalResponse>>(result.Content!);
            return Ok(ResponseFactory<IEnumerable<RentalResponse>>.Success(result.Message!, responseList));
        }

        [HttpGet]
        [Route(nameof(GetCount))]
        [Produces("application/json")]
        public IActionResult GetCount()
        {
            var result = _rentalService!.GetCount().Result;
            return Ok(ResponseFactory<int>.Success(result.Message!, result.Content));
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
                var result = _rentalService!.FinishRental(request);
                return Ok(ResponseFactory<FinishRentalResponse>.Success(result.Message!, result.Content!));
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
                var result = _rentalService!.Insert(request).Result;
                var responseEntity = _mapper!.Map<Rental, RentalResponse>(result.Content!);
                return Ok(ResponseFactory<RentalResponse>.Success(result.Message!, responseEntity));
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
        public ActionResult<RentalResponse> Update(RentalRequestUpdate requestUpdate)
        {
            try
            {
                var request = new RentalRequest
                {
                    Id = requestUpdate.Id,
                    BikeId = requestUpdate.BikeId,
                    IsActive = requestUpdate.IsActive,
                    PlanId = requestUpdate.PlanId,
                    RequestUserId = requestUpdate.RequestUserId,
                    UserId = requestUpdate.UserId
                };

                var result = _rentalService!.Update(request).Result;
                var responseEntity = _mapper!.Map<Rental, RentalResponse>(result.Content!);
                return Ok(ResponseFactory<RentalResponse>.Success(result.Message!, responseEntity));
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
        public IActionResult Delete(RentalRequest request)
        {
            try
            {
                var result = _rentalService!.Delete(request).Result;
                var responseEntity = _mapper!.Map<Rental, RentalResponse>(result.Content!);
                return Ok(ResponseFactory<RentalResponse>.Success(result.Message!, responseEntity));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<UserTypeResponse>.Error(String.Format("Erro ao remover o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }
    }
}
