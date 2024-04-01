namespace Mottu.Api.Controllers
{
    [Route("api/NotificatedUser")]
    [SwaggerTag("UsuarioNotificado")]
    [ApiController]
    public class NotificatedUserController : ControllerBase<NotificatedUser, OrderResponse>
    {
        public NotificatedUserController(IUnitOfWork unitOfWork, IMapper? mapper, INotificatedUserService notificatedUserService)
            : base(unitOfWork, mapper)
        {
            _nomeEntidade = "Usuário Notificado";
            _notificatedUserService = notificatedUserService;
        }

        [HttpPost]
        [Route(nameof(GetListOfNotificatedUsers))]
        [Produces("application/json")]
        public IActionResult GetListOfNotificatedUsers(NotificatedUserRequest request)
        {
            var service = _notificatedUserService!.GetListOfNotificatedUsers(request).Result;

            if (service.StatusCode != EnumStatusCode.Status200OK)
            {
                return BadRequest(ResponseFactory<ListNotificatedUsersResponse>.Error(service.Message!));
            }
            else
            {
                return Ok(service.Content); 
            }
        }
    }
}
