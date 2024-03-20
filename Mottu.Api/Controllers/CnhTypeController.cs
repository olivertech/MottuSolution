using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mottu.Application.Services;
using Mottu.CrossCutting.Requests;
using Mottu.CrossCutting.Responses;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Xml.Linq;

namespace Mottu.Api.Controllers
{
    [Route("api/CnhType")]
    [SwaggerTag("Tipo de CNH")]
    [ApiController]
    public class CnhTypeController : BaseController
    {
        public CnhTypeController(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Tipo de CNH";
        }

        [HttpGet]
        [Route(nameof(GetAll))]
        [Produces("application/json")]
        public IActionResult GetAll()
        {
            var service = new CnHTypeService(_unitOfWork!, _mapper).GetAll();
            return Ok(service.Result);
        }

        [HttpGet]
        [Route("Get/{id:Guid}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<CnhTypeResponse>.Error(false, "Id inválido!"));

            var entities = await _unitOfWork!.cnhTypeRepository.GetById(id);
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetListByPlate))]
        [Produces("application/json")]
        public async Task<IActionResult> GetListByPlate(string name)
        {
            if (name is null)
                return BadRequest(ResponseFactory<CnhTypeResponse>.Error(false, "Nome inválido!"));

            var entities = await _unitOfWork!.cnhTypeRepository.GetList(x => x.Name!.ToLower() == name.ToLower());
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetCount))]
        [Produces("application/json")]
        public async Task<IActionResult> GetCount()
        {
            var entities = await _unitOfWork!.cnhTypeRepository.Count();
            return Ok(entities);
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
                if (request is null)
                    return BadRequest(ResponseFactory<CnhTypeResponse>.Error(false, "Request inválido!"));

                var search = _unitOfWork!.cnhTypeRepository.GetAll().Result;

                if (search!.Any(x => x.Name == request.Name))
                    return Ok(ResponseFactory<CnhTypeResponse>.Error(false, String.Format("Já existe um {0} com essa letra.", _nomeEntidade)));

                var entity = _mapper!.Map<CnhType>(request);

                entity.Id = Guid.NewGuid();
                var result = _unitOfWork.cnhTypeRepository.Insert(entity);

                _unitOfWork.CommitAsync().Wait();

                if (result != null)
                {
                    var response = _mapper.Map<CnhTypeResponse>(entity);
                    return Ok(ResponseFactory<CnhTypeResponse>.Success(true, String.Format("Inclusão de {0} Realizada Com Sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<CnhTypeResponse>.Error(false, String.Format("Não foi possível incluir a {0}! Verifique os dados enviados.", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<CnhTypeResponse>.Error(false, String.Format("Erro ao inserir a {0} -> ", _nomeEntidade) + ex.Message));
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
                if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                    return BadRequest(ResponseFactory<CnhTypeResponse>.Error(false, "Id informado inválido!"));

                var entity = _unitOfWork!.cnhTypeRepository.GetById(request.Id).Result;

                if (entity is null)
                    return NotFound(ResponseFactory<CnhTypeResponse>.Error(false, "Id informado inválido!"));

                var search = _unitOfWork!.cnhTypeRepository.GetAll().Result;

                if (search!.Any(x => x.Name == request.Name && x.Id != request.Id))
                    return Ok(ResponseFactory<CnhTypeResponse>.Error(false, String.Format("Já existe um {0} com essa letra.", _nomeEntidade)));

                _mapper!.Map(request, entity);

                var result = _unitOfWork.cnhTypeRepository.Update(entity).Result;

                _unitOfWork.CommitAsync().Wait();

                if (result)
                {
                    var response = _mapper!.Map<CnhTypeResponse>(entity);
                    return Ok(ResponseFactory<CnhTypeResponse>.Success(true, String.Format("Atualização da {0} realizada com sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status304NotModified, ResponseFactory<CnhTypeResponse>.Error(false, String.Format("{0} não encontrada para atualização!", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<CnhTypeResponse>.Error(false, String.Format("Erro ao atualizar a {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(CnhTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(CnhTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(CnhTypeResponse))]
        public IActionResult Delete(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<CnhTypeResponse>.Error(false, "Id informado igual a 0!"));

            var entity = _unitOfWork!.cnhTypeRepository.GetById(id).Result;

            if (entity is null)
                return NotFound(ResponseFactory<CnhTypeResponse>.Error(false, "Id informado inválido!"));

            var result = _unitOfWork.cnhTypeRepository.Delete(id).Result;

            _unitOfWork.CommitAsync().Wait();

            if (result)
            {
                var response = _mapper!.Map<CnhTypeResponse>(entity);
                return Ok(ResponseFactory<CnhTypeResponse>.Success(true, String.Format("Remoção de {0} realizada com sucesso.", _nomeEntidade), response));
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseFactory<CnhTypeResponse>.Error(false, String.Format("{0} não encontrada para remoção!", _nomeEntidade)));
            }
        }
    }
}
