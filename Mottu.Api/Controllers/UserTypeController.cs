﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mottu.Application.Services;
using Mottu.Application.Requests;
using Mottu.Application.Responses;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Mottu.Application.Interfaces;
using Mottu.Api.Controllers.Base;

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
            var result = _userTypeService!.GetAll();
            return Ok(result.Result);
        }

        [HttpGet]
        [Route("Get/{id:Guid}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<UserTypeResponse>.Error(false, "Id inválido!"));

            var entities = await _unitOfWork!.userTypeRepository.GetById(id);
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetListByName))]
        [Produces("application/json")]
        public async Task<IActionResult> GetListByName(string name)
        {
            if(name is null)
                return BadRequest(ResponseFactory<UserTypeResponse>.Error(false, "Nome inválido!"));

            var entities = await _unitOfWork!.userTypeRepository.GetList(x => x.Name!.ToLower() == name.ToLower());
            return Ok(entities);
        }

        [HttpGet]
        [Route(nameof(GetCount))]
        [Produces("application/json")]
        public async Task<IActionResult> GetCount()
        {
            var entities = await _unitOfWork!.userTypeRepository.Count();
            return Ok(entities);
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
                if (request is null)
                    return BadRequest(ResponseFactory<UserTypeResponse>.Error(false, "Request inválido!"));

                var search = _unitOfWork!.userTypeRepository.GetAll().Result;

                if (search!.Any(x => x.Name == request.Name))
                    return Ok(ResponseFactory<UserTypeResponse>.Error(false, String.Format("Já existe um {0} com esse nome.", _nomeEntidade)));

                var entity = _mapper!.Map<UserType>(request);

                entity.Id = Guid.NewGuid();
                var result = _unitOfWork.userTypeRepository.Insert(entity);

                _unitOfWork.CommitAsync().Wait();

                if (result != null)
                {
                    var response = _mapper.Map<UserTypeResponse>(entity);
                    return Ok(ResponseFactory<UserTypeResponse>.Success(true, String.Format("Inclusão de {0} Realizada Com Sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<UserTypeResponse>.Error(false, String.Format("Não foi possível incluir o {0}! Verifique os dados enviados.", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<UserTypeResponse>.Error(false, String.Format("Erro ao inserir o {0} -> ", _nomeEntidade) + ex.Message));
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
                if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                    return BadRequest(ResponseFactory<UserTypeResponse>.Error(false, "Id informado inválido!"));

                var entity = _unitOfWork!.userTypeRepository.GetById(request.Id).Result;

                if (entity is null)
                    return NotFound(ResponseFactory<UserTypeResponse>.Error(false, "Id informado inválido!"));

                var search = _unitOfWork!.statusOrderRepository.GetAll().Result;

                if (search!.Any(x => x.Name == request.Name && x.Id != request.Id))
                    return Ok(ResponseFactory<UserTypeResponse>.Error(false, String.Format("Já existe um {0} com esse nome.", _nomeEntidade)));

                _mapper!.Map(request, entity);

                var result = _unitOfWork.userTypeRepository.Update(entity).Result;

                _unitOfWork.CommitAsync().Wait();

                if (result)
                {
                    var response = _mapper!.Map<UserTypeResponse>(entity);
                    return Ok(ResponseFactory<UserTypeResponse>.Success(true, String.Format("Atualização do {0} realizada com sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status304NotModified, ResponseFactory<UserTypeResponse>.Error(false, String.Format("{0} não encontrado para atualização!", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<UserTypeResponse>.Error(false, String.Format("Erro ao atualizar o {0} -> ", _nomeEntidade) + ex.Message));
            }
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK, Type = typeof(UserTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound, Type = typeof(UserTypeResponse))]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest, Type = typeof(UserTypeResponse))]
        public IActionResult Delete(Guid id)
        {
            if (id.ToString().Length == 0)
                return BadRequest(ResponseFactory<UserTypeResponse>.Error(false, "Id informado igual a 0!"));

            var entity = _unitOfWork!.userTypeRepository.GetById(id).Result;

            if (entity is null)
                return NotFound(ResponseFactory<UserTypeResponse>.Error(false, "Id informado inválido!"));

            var result = _unitOfWork.userTypeRepository.Delete(id).Result;

            _unitOfWork.CommitAsync().Wait();

            if (result)
            {
                var response = _mapper!.Map<UserTypeResponse>(entity);
                return Ok(ResponseFactory<UserTypeResponse>.Success(true, String.Format("Remoção de {0} realizado com sucesso.", _nomeEntidade), response));
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseFactory<UserTypeResponse>.Error(false, String.Format("{0} não encontrado para remoção!", _nomeEntidade)));
            }
        }
    }
}
