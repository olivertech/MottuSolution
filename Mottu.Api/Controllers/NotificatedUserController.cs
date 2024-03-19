using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mottu.CrossCutting.Helpers;
using Mottu.CrossCutting.Responses;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Mottu.Api.Controllers
{
    [Route("api/NotificatedUser")]
    [SwaggerTag("UsuarioNotificado")]
    [ApiController]
    public class NotificatedUserController : BaseController
    {
        public NotificatedUserController(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Usuário Notificado";
        }

        [HttpGet]
        [Route(nameof(GetListOfNotificatedUsers))]
        [Produces("application/json")]
        public async Task<IActionResult> GetListOfNotificatedUsers(Guid orderId)
        {
            var convert = new ConvertModelToResponse<AppUser, AppUserResponse>(_mapper);

            var list = await _unitOfWork!.userRepository.GetFullListOfNotificatedUsers(orderId);
            List<AppUserResponse> result = convert.GetResponsList(list!);

            var order = _unitOfWork.orderRepository.GetById(orderId).Result;

            var orderResponse = _mapper!.Map<OrderResponse>(order);

            var responseListNotificatedUsers = new ListNotificatedUsersResponse
            {
                ListNotificatedUsers = result,
                Order = orderResponse
            };

            return Ok(ResponseFactory<ListNotificatedUsersResponse>.Success(true, "Listagem retornada com sucesso.", responseListNotificatedUsers));
        }
    }
}
