using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mottu.CrossCutting.Requests;
using Mottu.CrossCutting.Responses;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Mottu.Api.Controllers
{
    [Route("api/Plan")]
    [SwaggerTag("Plano")]
    [ApiController]
    public class PlanController : BaseController
    {
        public PlanController(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Plano";
        }

        [HttpGet]
        [Route(nameof(GetAll))]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _unitOfWork!.planRepository.GetAll();
            return Ok(list);
        }

        [HttpGet]
        [Route("Get/{id:Guid}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<PlanResponse>.Error(false, "Id inválido!"));

            var entities = await _unitOfWork!.planRepository.GetById(id);
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetListByName))]
        [Produces("application/json")]
        public async Task<IActionResult> GetListByName(string name)
        {
            if (name is null)
                return BadRequest(ResponseFactory<PlanResponse>.Error(false, "Nome inválido!"));

            var entities = await _unitOfWork!.planRepository.GetList(x => x.Name!.ToLower() == name.ToLower());
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetCount))]
        [Produces("application/json")]
        public async Task<IActionResult> GetCount()
        {
            var entities = await _unitOfWork!.planRepository.Count();
            return Ok(entities);
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
                if (request is null)
                    return BadRequest(ResponseFactory<PlanResponse>.Error(false, "Request inválido!"));

                var search = _unitOfWork!.planRepository.GetAll().Result;

                //TODO: REVER ESSAS REGRAS AQUI NA CONTROLLER
                if (search!.Any(x => x.Name == request.Name) ||
                    search!.Any(x => x.DailyValue == request.DailyValue) ||
                    search!.Any(x => x.NumDays == request.NumDays))
                    return Ok(ResponseFactory<PlanResponse>.Error(false, String.Format("Já existe um {0} com esses valores de diária e dias de locação.", _nomeEntidade)));
                
                var entity = _mapper!.Map<Plan>(request);

                entity.Id = Guid.NewGuid();
                var result = _unitOfWork.planRepository.Insert(entity);

                _unitOfWork.CommitAsync().Wait();

                if (result != null)
                {
                    var response = _mapper.Map<PlanResponse>(entity);
                    return Ok(ResponseFactory<PlanResponse>.Success(true, String.Format("Inclusão de {0} Realizado Com Sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<PlanResponse>.Error(false, String.Format("Não foi possível incluir o {0}! Verifique os dados enviados.", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<PlanResponse>.Error(false, String.Format("Erro ao inserir o {0} -> ", _nomeEntidade) + ex.Message));
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
                if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                    return BadRequest(ResponseFactory<PlanResponse>.Error(false, "Id informado inválido!"));

                var entity = _unitOfWork!.planRepository.GetById(request.Id).Result;

                if (entity is null)
                    return NotFound(ResponseFactory<PlanResponse>.Error(false, "Id informado inválido!"));

                var search = _unitOfWork!.planRepository.GetAll().Result;

                //TODO: REVER ESSAS REGRAS AQUI NA CONTROLLER
                if (search!.Any(x => x.Name == request.Name && x.Id != request.Id) ||
                    search!.Any(x => x.DailyValue == request.DailyValue && x.Id != request.Id) ||
                    search!.Any(x => x.NumDays == request.NumDays && x.Id != request.Id))
                    return Ok(ResponseFactory<PlanResponse>.Error(false, String.Format("Já existe um {0} com esses valores de diária e dias de locação.", _nomeEntidade)));

                _mapper!.Map(request, entity);

                var result = _unitOfWork.planRepository.Update(entity).Result;

                _unitOfWork.CommitAsync().Wait();

                if (result)
                {
                    var response = _mapper!.Map<PlanResponse>(entity);
                    return Ok(ResponseFactory<PlanResponse>.Success(true, String.Format("Atualização do {0} realizada com sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status304NotModified, ResponseFactory<PlanResponse>.Error(false, String.Format("{0} não encontrado para atualização!", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<PlanResponse>.Error(false, String.Format("Erro ao atualizar a {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(PlanResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(PlanResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(PlanResponse))]
        public IActionResult Delete(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<PlanResponse>.Error(false, "Id informado igual a 0!"));

            var entity = _unitOfWork!.planRepository.GetById(id).Result;

            if (entity is null)
                return NotFound(ResponseFactory<PlanResponse>.Error(false, "Id informado inválido!"));

            var result = _unitOfWork.planRepository.Delete(id).Result;

            _unitOfWork.CommitAsync().Wait();

            if (result)
            {
                var response = _mapper!.Map<PlanResponse>(entity);
                return Ok(ResponseFactory<PlanResponse>.Success(true, String.Format("Remoção de {0} realizada com sucesso.", _nomeEntidade), response));
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseFactory<PlanResponse>.Error(false, String.Format("{0} não encontrada para remoção!", _nomeEntidade)));
            }
        }
    }
}
