using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mottu.Api.Controllers.Base;
using Mottu.Application.Helpers;
using Mottu.Application.Interfaces;
using Mottu.Application.Requests;
using Mottu.Application.Responses;
using Mottu.Application.Services;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Mottu.Api.Controllers
{
    [Route("api/NotificatedUser")]
    [SwaggerTag("UsuarioNotificado")]
    [ApiController]
    public class NotificatedUserController : ControllerBase<NotificatedUser, OrderResponse>
    {
        public NotificatedUserController(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Usuário Notificado";
            _notificatedUserService = new NotificatedUserService(_unitOfWork!, _mapper);
        }

        [HttpPost]
        [Route(nameof(GetListOfNotificatedUsers))]
        [Produces("application/json")]
        public async Task<IActionResult> GetListOfNotificatedUsers(NotificatedUserRequest request)
        {
            if (request is null)
                return BadRequest(ResponseFactory<OrderResponse>.Error("Request inválido!"));

            //Valido solicitante da requisição
            var requester = _unitOfWork!.userRepository.GetFullById(request.RequestUserId).Result;

            if (requester is null)
                return BadRequest(ResponseFactory<OrderResponse>.Error("Request inválido!"));

            if (requester.UserType!.Name!.ToLower() != GetDescriptionFromEnum.GetFromUserTypeEnum(EnumUserTypes.Administrador).ToLower())
                return BadRequest(ResponseFactory<OrderResponse>.Error("Usuário solicitante inválido!"));

            var convert = new ConvertModelToResponse<AppUser, AppUserResponse>(_mapper);

            var list = await _unitOfWork!.userRepository.GetFullListOfNotificatedUsers(request.OrderId);
            List<AppUserResponse> result = convert.GetResponsList(list!);

            var order = _unitOfWork.orderRepository.GetById(request.OrderId).Result;

            var orderResponse = _mapper!.Map<OrderResponse>(order);

            var responseListNotificatedUsers = new ListNotificatedUsersResponse
            {
                ListNotificatedUsers = result,
                Order = orderResponse
            };

            return Ok(ResponseFactory<ListNotificatedUsersResponse>.Success("Listagem retornada com sucesso.", responseListNotificatedUsers));
        }
    }
}
