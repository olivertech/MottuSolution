namespace Mottu.Application.Services
{
    public class BikeService : ServiceBase<Bike, BikeRequest>, IBikeService
    {
        protected readonly IMapper? _mapper;

        public BikeService(IUnitOfWork unitOfWork, IMapper? mapper)
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponseFactory<IEnumerable<BikeResponse>>> GetAll(BaseRequest request)
        {
            //Valido solicitante da requisição
            var requester = _unitOfWork!.userRepository.GetFullById(request.RequestUserId).Result;

            if (requester is null)
                return ServiceResponseFactory<IEnumerable<BikeResponse>>.Return(false, EnumStatusCode.Status400BadRequest, "Request inválido!");

            if (requester.UserType!.Name!.ToLower() != GetDescriptionFromEnum.GetFromUserTypeEnum(EnumUserTypes.Administrador).ToLower())
                return ServiceResponseFactory<IEnumerable<BikeResponse>>.Return(false, EnumStatusCode.Status400BadRequest, "Usuário solicitante inválido!");

            var list = await _unitOfWork!.bikeRepository.GetAll();

            var convert = new ConvertModelToResponse<Bike, BikeResponse>(_mapper);
            var content = convert.GetResponsList(list!);

            return ServiceResponseFactory<IEnumerable<BikeResponse>>.Return(true, EnumStatusCode.Status200OK, "OK", content);
        }
    }
}
