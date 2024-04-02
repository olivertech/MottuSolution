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

        public override async Task<ServiceResponseFactory<IEnumerable<Bike>>> GetAll()
        {
            var list = await _unitOfWork!.bikeRepository!.GetAll();
            return ServiceResponseFactory<IEnumerable<Bike>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Lista de Motos recuperada com sucesso.", list!);
        }

        public override async Task<ServiceResponseFactory<Bike>> GetById(Guid id)
        {
            if (!Guid.TryParse(id.ToString(), out _))
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            var entity = await _unitOfWork!.bikeRepository.GetById(id);

            if (entity == null)
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            return ServiceResponseFactory<Bike>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Moto recuperada com sucesso.", entity!);
        }

        public async Task<ServiceResponseFactory<IEnumerable<Bike>>> GetListByPlate(BikeByPlateRequest request)
        {
            //Valida o requester
            var validation = await ValidateAdminRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<IEnumerable<Bike>>.Return(false, validation.StatusCode, validation.Message!);

            if (request.Plate is null)
                return ServiceResponseFactory<IEnumerable<Bike>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Placa inválida!");

            var entities = await _unitOfWork!.bikeRepository.GetList(x => x.Plate!.ToLower() == request.Plate.ToLower());

            if (entities != null)
                return ServiceResponseFactory<IEnumerable<Bike>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Moto recuperada com base na placa com sucesso.", entities);
            else
                return ServiceResponseFactory<IEnumerable<Bike>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não existe moto para a placa informada!");
        }

        public override async Task<ServiceResponseFactory<int>> GetCount()
        {
            var numEntities = await _unitOfWork!.bikeRepository.Count();
            return ServiceResponseFactory<int>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Total de Motos recuperado com sucesso.", numEntities);
        }

        public override async Task<ServiceResponseFactory<Bike>> Insert(BikeRequest request)
        {
            if (request is null)
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            //Valida o requester
            var validation = await ValidateAdminRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<Bike>.Return(false, validation.StatusCode, validation.Message!);

            var search = _unitOfWork!.bikeRepository.GetAll().Result;

            if (search!.Any(x => x.Plate == request.Plate))
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe uma moto com essa placa!");

            var entity = _mapper!.Map<Bike>(request);

            entity.Id = Guid.NewGuid();
            var result = _unitOfWork.bikeRepository.Insert(entity);

            _unitOfWork.CommitAsync().Wait();

            if (result != null)
                return ServiceResponseFactory<Bike>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Moto incluída com sucesso.", entity);
            else
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não foi possível incluir a moto!", entity);
        }

        public override async Task<ServiceResponseFactory<Bike>> Update(BikeRequest request)
        {
            if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            //Valida o requester
            var validation = await ValidateAdminRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<Bike>.Return(false, validation.StatusCode, validation.Message!);

            var entity = _unitOfWork!.bikeRepository.GetById(request.Id).Result;

            if (entity is null)
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id informado inválido!");

            var search = _unitOfWork!.bikeRepository.GetAll().Result;

            if (search!.Any(x => x.Plate == request.Plate && x.Id != request.Id))
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe uma moto com essa placa!");

            entity.Plate = request.Plate;

            var result = _unitOfWork.bikeRepository.Update(entity).Result;

            _unitOfWork.CommitAsync().Wait();

            if (result)
                return ServiceResponseFactory<Bike>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Moto atualizada com sucesso.", entity);
            else
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não foi possível atualizar a moto!", entity);
        }

        public override async Task<ServiceResponseFactory<Bike>> Delete(BikeRequest request)
        {
            if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            //Valida o requester
            var validation = await ValidateAdminRequester((Guid)request.RequestUserId!);

            if (!validation.IsValid)
                return ServiceResponseFactory<Bike>.Return(false, validation.StatusCode, validation.Message!);

            var entity = _unitOfWork!.bikeRepository.GetById(request.Id).Result;

            if (entity is null)
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id informado inválido!");

            //Verifico se a moto se encontra locada
            if (entity.IsLeased)
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Moto não pode ser removida, pois se encontra alocada!");

            //Verifico se existe alguma locação já realizada com essa moto
            var search = _unitOfWork.rentalRepository.GetList(x => x.Bike.Id == request.Id).Result;

            if (search!.Any())
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Moto não pode ser removida, pois existem locações associadas!");
            
            var result = _unitOfWork.bikeRepository.Delete(request.Id).Result;

            _unitOfWork.CommitAsync().Wait();

            if (result)
                return ServiceResponseFactory<Bike>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Moto removida com sucesso.", entity);
            else
                return ServiceResponseFactory<Bike>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não foi possível remover a moto!", entity);
        }
    }
}
