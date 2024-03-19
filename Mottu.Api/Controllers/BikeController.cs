using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mottu.CrossCutting.Requests;
using Mottu.CrossCutting.Responses;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Mottu.Api.Controllers
{
    [Route("api/Bike")]
    [SwaggerTag("Moto")]
    [ApiController]
        public class BikeController : BaseController
        {
            public BikeController(IUnitOfWork unitOfWork, IMapper? mapper)
                : base(unitOfWork, mapper)
            {
                _nomeEntidade = "Moto";
            }

        [HttpGet]
        [Route(nameof(GetAll))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _unitOfWork!.bikeRepository.GetAll();
            return Ok(list);
        }

        [HttpGet]
        [Route("Get/{id:Guid}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<BikeResponse>.Error(false, "Id inválido!"));

            var entities = await _unitOfWork!.bikeRepository.GetById(id);
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetListByPlate))]
        [Produces("application/json")]
        public async Task<IActionResult> GetListByPlate(string plate)
        {
            if (plate is null)
                return BadRequest(ResponseFactory<BikeResponse>.Error(false, "Placa inválida!"));

            var entities = await _unitOfWork!.bikeRepository.GetList(x => x.Plate!.ToLower() == plate.ToLower());
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetCount))]
        [Produces("application/json")]
        public async Task<IActionResult> GetCount()
        {
            var entities = await _unitOfWork!.bikeRepository.Count();
            return Ok(entities);
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
                if (request is null)
                    return BadRequest(ResponseFactory<BikeResponse>.Error(false, "Request inválido!"));

                var search = _unitOfWork!.bikeRepository.GetAll().Result;

                if (search!.Any(x => x.Plate == request.Plate))
                    return Ok(ResponseFactory<BikeResponse>.Error(false, String.Format("Já existe uma {0} com essa placa.", _nomeEntidade)));

                var entity = _mapper!.Map<Bike>(request);

                entity.Id = Guid.NewGuid();
                var result = _unitOfWork.bikeRepository.Insert(entity);

                _unitOfWork.CommitAsync().Wait();

                if (result != null)
                {
                    var response = _mapper.Map<BikeResponse>(entity);
                    return Ok(ResponseFactory<BikeResponse>.Success(true, String.Format("Inclusão de {0} Realizada Com Sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<BikeResponse>.Error(false, String.Format("Não foi possível incluir a {0}! Verifique os dados enviados.", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<BikeResponse>.Error(false, String.Format("Erro ao inserir a {0} -> ", _nomeEntidade) + ex.Message));
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
        public ActionResult<BikeResponse> Update(BikeRequestUpdate request)
        {
            try
            {
                if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                    return BadRequest(ResponseFactory<BikeResponse>.Error(false, "Id informado inválido!"));

                var bike = _unitOfWork!.bikeRepository.GetById(request.Id).Result;

                if (bike is null)
                    return NotFound(ResponseFactory<BikeResponse>.Error(false, "Id informado inválido!"));

                var search = _unitOfWork!.bikeRepository.GetAll().Result;

                if (search!.Any(x => x.Plate == request.Plate && x.Id != request.Id))
                    return Ok(ResponseFactory<BikeResponse>.Error(false, String.Format("Já existe uma {0} com essa placa.", _nomeEntidade)));

                bike.Plate = request.Plate;

                var result = _unitOfWork.bikeRepository.Update(bike).Result;

                _unitOfWork.CommitAsync().Wait();

                if (result)
                {
                    var response = _mapper!.Map<BikeResponse>(bike);
                    return Ok(ResponseFactory<BikeResponse>.Success(true, String.Format("Atualização da {0} realizada com sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status304NotModified, ResponseFactory<BikeResponse>.Error(false, String.Format("{0} não encontrada para atualização!", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<BikeResponse>.Error(false, String.Format("Erro ao atualizar a {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(BikeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(BikeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(BikeResponse))]
        public IActionResult Delete(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<BikeResponse>.Error(false, "Id informado igual a 0!"));

            var entity = _unitOfWork!.bikeRepository.GetById(id).Result;

            if (entity is null)
                return NotFound(ResponseFactory<BikeResponse>.Error(false, "Id informado inválido!"));

            //Verifico se a moto se encontra locada
            if(entity.IsLeased)
                return BadRequest(ResponseFactory<BikeResponse>.Error(false, String.Format("{0} não pode ser removida, pois se encontra alocada!", _nomeEntidade)));

            //Verifico se existe alguma locação já realizada com essa moto
            var search = _unitOfWork.rentalRepository.GetList(x => x.Bike.Id == id).Result;

            if (search!.Any())
                return BadRequest(ResponseFactory<BikeResponse>.Error(false, String.Format("{0} não pode ser removida, pois existem locações associadas!", _nomeEntidade)));

            var result = _unitOfWork.bikeRepository.Delete(id).Result;

            _unitOfWork.CommitAsync().Wait();

            if (result)
            {
                var response = _mapper!.Map<BikeResponse>(entity);
                return Ok(ResponseFactory<BikeResponse>.Success(true, String.Format("Remoção de {0} realizada com sucesso.", _nomeEntidade), response));
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseFactory<BikeResponse>.Error(false, String.Format("{0} não encontrada para remoção!", _nomeEntidade)));
            }
        }
    }
}
