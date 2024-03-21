﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mottu.Api.Controllers.Base;
using Mottu.Application.Helpers;
using Mottu.Application.Requests;
using Mottu.Application.Requests.Base;
using Mottu.Application.Responses;
using Mottu.Application.Services;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.Eventing.Reader;

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
        public async Task<IActionResult> GetAll(BaseRequest request)
        {
            var service = await _bikeService!.GetAll(request);

            if (!service.Result)
            {
                BadRequestObjectResult badRequest = BadRequest(ResponseFactory<BikeResponse>.Error(service.Message!));
                return badRequest;
            }
            else
            {
                return Ok(service.Content);
            }
        }

        [HttpGet]
        [Route("Get/{id:Guid}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetById(BikeRequest request)
        {
            //Valido solicitante da requisição
            var requester = _unitOfWork!.userRepository.GetFullById(request.RequestUserId).Result;

            if (requester is null)
                return BadRequest(ResponseFactory<OrderResponse>.Error("Request inválido!"));

            if (requester.UserType!.Name!.ToLower() != GetDescriptionFromEnum.GetFromUserTypeEnum(EnumUserTypes.Administrador).ToLower())
                return BadRequest(ResponseFactory<OrderResponse>.Error("Usuário solicitante inválido!"));

            if (request.Id.ToString().Length == 0)
                return BadRequest(ResponseFactory<BikeResponse>.Error("Id inválido!"));

            var entities = await _unitOfWork!.bikeRepository.GetById(request.Id);

            return Ok(entities);
        }

        [HttpPost]
        [Route(nameof(GetListByPlate))]
        [Produces("application/json")]
        public async Task<IActionResult> GetListByPlate(BikeByPlateRequest request)
        {
            //Valido solicitante da requisição
            var requester = _unitOfWork!.userRepository.GetFullById(request.RequestUserId).Result;

            if (requester is null)
                return BadRequest(ResponseFactory<OrderResponse>.Error("Request inválido!"));

            if (requester.UserType!.Name!.ToLower() != GetDescriptionFromEnum.GetFromUserTypeEnum(EnumUserTypes.Administrador).ToLower())
                return BadRequest(ResponseFactory<OrderResponse>.Error("Usuário solicitante inválido!"));

            if (request.Plate is null)
                return BadRequest(ResponseFactory<BikeResponse>.Error("Placa inválida!"));

            var entities = await _unitOfWork!.bikeRepository.GetList(x => x.Plate!.ToLower() == request.Plate.ToLower());

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
                    return BadRequest(ResponseFactory<BikeResponse>.Error("Request inválido!"));

                //Valido solicitante da requisição
                var requester = _unitOfWork!.userRepository.GetFullById(request.RequestUserId).Result;

                if (requester is null)
                    return BadRequest(ResponseFactory<OrderResponse>.Error("Request inválido!"));

                if (requester.UserType!.Name!.ToLower() != GetDescriptionFromEnum.GetFromUserTypeEnum(EnumUserTypes.Administrador).ToLower())
                    return BadRequest(ResponseFactory<OrderResponse>.Error("Usuário solicitante inválido!"));

                var search = _unitOfWork!.bikeRepository.GetAll().Result;

                if (search!.Any(x => x.Plate == request.Plate))
                    return Ok(ResponseFactory<BikeResponse>.Error(String.Format("Já existe uma {0} com essa placa.", _nomeEntidade)));

                var entity = _mapper!.Map<Bike>(request);

                entity.Id = Guid.NewGuid();
                var result = _unitOfWork.bikeRepository.Insert(entity);

                _unitOfWork.CommitAsync().Wait();

                if (result != null)
                {
                    var response = _mapper.Map<BikeResponse>(entity);
                    return Ok(ResponseFactory<BikeResponse>.Success(String.Format("Inclusão de {0} Realizada Com Sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<BikeResponse>.Error(String.Format("Não foi possível incluir a {0}! Verifique os dados enviados.", _nomeEntidade)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ResponseFactory<BikeResponse>.Error(String.Format("Erro ao inserir a {0} -> ", _nomeEntidade) + ex.Message));
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
                    return BadRequest(ResponseFactory<BikeResponse>.Error("Id informado inválido!"));

                //Valido solicitante da requisição
                var requester = _unitOfWork!.userRepository.GetFullById(request.RequestUserId).Result;

                if (requester is null)
                    return BadRequest(ResponseFactory<OrderResponse>.Error("Request inválido!"));

                if (requester.UserType!.Name!.ToLower() != GetDescriptionFromEnum.GetFromUserTypeEnum(EnumUserTypes.Administrador).ToLower())
                    return BadRequest(ResponseFactory<OrderResponse>.Error("Usuário solicitante inválido!"));

                var bike = _unitOfWork!.bikeRepository.GetById(request.Id).Result;

                if (bike is null)
                    return NotFound(ResponseFactory<BikeResponse>.Error("Id informado inválido!"));

                var search = _unitOfWork!.bikeRepository.GetAll().Result;

                if (search!.Any(x => x.Plate == request.Plate && x.Id != request.Id))
                    return Ok(ResponseFactory<BikeResponse>.Error(String.Format("Já existe uma {0} com essa placa.", _nomeEntidade)));

                bike.Plate = request.Plate;

                var result = _unitOfWork.bikeRepository.Update(bike).Result;

                _unitOfWork.CommitAsync().Wait();

                if (result)
                {
                    var response = _mapper!.Map<BikeResponse>(bike);
                    return Ok(ResponseFactory<BikeResponse>.Success(String.Format("Atualização da {0} realizada com sucesso.", _nomeEntidade), response));
                }
                else
                {
                    return StatusCode(StatusCodes.Status304NotModified, ResponseFactory<BikeResponse>.Error(String.Format("{0} não encontrada para atualização!", _nomeEntidade)));
                }
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
        public IActionResult Delete(BikeRequestDelete request)
        {
            if (request.Id.ToString().Length == 0)
                return BadRequest(ResponseFactory<BikeResponse>.Error("Id informado igual a 0!"));

            //Valido solicitante da requisição
            var requester = _unitOfWork!.userRepository.GetFullById(request.RequestUserId).Result;

            if (requester is null)
                return BadRequest(ResponseFactory<OrderResponse>.Error("Request inválido!"));

            if (requester.UserType!.Name!.ToLower() != GetDescriptionFromEnum.GetFromUserTypeEnum(EnumUserTypes.Administrador).ToLower())
                return BadRequest(ResponseFactory<OrderResponse>.Error("Usuário solicitante inválido!"));

            var entity = _unitOfWork!.bikeRepository.GetById(request.Id).Result;

            if (entity is null)
                return NotFound(ResponseFactory<BikeResponse>.Error("Id informado inválido!"));

            //Verifico se a moto se encontra locada
            if (entity.IsLeased)
                return BadRequest(ResponseFactory<BikeResponse>.Error(String.Format("{0} não pode ser removida, pois se encontra alocada!", _nomeEntidade)));

            //Verifico se existe alguma locação já realizada com essa moto
            var search = _unitOfWork.rentalRepository.GetList(x => x.Bike.Id == request.Id).Result;

            if (search!.Any())
                return BadRequest(ResponseFactory<BikeResponse>.Error(String.Format("{0} não pode ser removida, pois existem locações associadas!", _nomeEntidade)));

            var result = _unitOfWork.bikeRepository.Delete(request.Id).Result;

            _unitOfWork.CommitAsync().Wait();

            if (result)
            {
                var response = _mapper!.Map<BikeResponse>(entity);
                return Ok(ResponseFactory<BikeResponse>.Success(String.Format("Remoção de {0} realizada com sucesso.", _nomeEntidade), response));
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, ResponseFactory<BikeResponse>.Error(String.Format("{0} não encontrada para remoção!", _nomeEntidade)));
            }
        }
    }
}
