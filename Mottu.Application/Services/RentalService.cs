

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;

namespace Mottu.Application.Services
{
    public class RentalService : ServiceBase<Rental, RentalRequest>, IRentalService
    {
        protected readonly IMapper? _mapper;
        protected IConfiguration? _configuration;

        public RentalService(IUnitOfWork unitOfWork, IMapper? mapper, IConfiguration configuration)
            : base(unitOfWork)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        public override async Task<ServiceResponseFactory<IEnumerable<Rental>>> GetAll()
        {
            var list = await _unitOfWork!.rentalRepository!.GetFullAll();
            return ServiceResponseFactory<IEnumerable<Rental>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Lista de Locações recuperada com sucesso.", list!);
        }

        public override async Task<ServiceResponseFactory<Rental>> GetById(Guid id)
        {
            if (!Guid.TryParse(id.ToString(), out _))
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            var entity = await _unitOfWork!.rentalRepository.GetById(id);

            if (entity == null)
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            return ServiceResponseFactory<Rental>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Locação recuperada com sucesso.", entity!);
        }

        public async Task<ServiceResponseFactory<IEnumerable<Rental>>> GetListByBike(Guid bikeId)
        {
            if (!Guid.TryParse(bikeId.ToString(), out _))
                return ServiceResponseFactory<IEnumerable<Rental>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            var entities = await _unitOfWork!.rentalRepository.GetList(x => x.Id == bikeId);

            if (entities != null)
                return ServiceResponseFactory<IEnumerable<Rental>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Locação recuperada por Id com sucesso.", entities);
            else
                return ServiceResponseFactory<IEnumerable<Rental>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não existe Locação para o Id informado!");
        }

        public ServiceResponseFactory<FinishRentalResponse> FinishRental(FinishRentalRequest request)
        {
            if (request is null)
                return ServiceResponseFactory<FinishRentalResponse>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            var rental = _unitOfWork!.rentalRepository.GetFullById(request.RentalId).Result;

            if (rental is null)
                return ServiceResponseFactory<FinishRentalResponse>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Locação não encontrada!");

            if (!DateOnly.TryParse(request.FinishRentalDate.ToString(), out _))
                return ServiceResponseFactory<FinishRentalResponse>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Data inválida para fechar locação!");

            ///===========================
            /// Calcular valor da locação
            ///===========================
            var rentalValue = CalculateTotalRental.GetRentalValue(rental);

            ///================
            /// Calcular multa
            ///================
            var fine = CalculateTotalRental.GetFineValue(ref rental, request, _configuration!);

            rental.TotalValue = rentalValue + fine;

            //================================================================================================
            //TODO: LINHAS ABAIXO FICARAM COMENTADAS, POR NÃO TER FICADO CLARO SE NA REGRA DE DEVOLUÇÃO DA
            //MOTO, SE JÁ SERÁ ENCERRADA A LOCAÇÃO, OU SE É APENAS UMA CONSULTA DO VALOR TOTAL DA LOCAÇÃO
            //================================================================================================
            //rental.IsActive = false;
            //_unitOfWork.rentalRepository.Update(rental);
            //_unitOfWork.CommitAsync().Wait();

            var response = new FinishRentalResponse(rental, rentalValue + fine);

            return ServiceResponseFactory<FinishRentalResponse>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Total a ser pago pela Locação.", response);
        }

        public override async Task<ServiceResponseFactory<Rental>> Insert(RentalRequest request)
        {
            if (request is null)
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            //Verifico se existem motos livres
            var bikesFree = await _unitOfWork!.bikeRepository.GetList(x => x.IsLeased == false);

            if (!bikesFree!.Any())
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não existem motos disponíveis no momento. Tente mais tarde.");

            //Verifico se a moto em questão enviada se encontra livre
            var search = await _unitOfWork!.rentalRepository.GetList(x => x.Bike!.Id == request.BikeId && x.Bike.IsLeased);

            if (search!.Any())
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe uma Locação em andamento (ativa) com essa moto. Informe outra moto.");

            //Verifico se o usuário existe
            var entity = _mapper!.Map<Rental>(request);

            var user = await _unitOfWork.userRepository.GetFullById(request.UserId);

            if (user == null)
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Usuário informado para Locação não existe. Verifique o id do usuário.");

            //Verifico se o usuário possui cnh tipo A
            if (user!.CnhType!.Name!.ToLower() != GetDescriptionFromEnum.GetFromStatusCnhType(EnumCnhTypes.A).ToLower())
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Usuário não possui carteira de motorista do tipo A.");

            //Recupera a moto a ser usada na locação
            var bike = await _unitOfWork.bikeRepository.GetById(request.BikeId);

            if (bike == null)
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Moto informada para Locação não existe. Verifique o id da moto.");

            //Verifico se o plano solicitado existe
            var plan = await _unitOfWork.planRepository.GetById(request.PlanId);

            if (plan == null)
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Plano informado para Locação não existe. Verifique o id da plano.");

            bike!.IsLeased = true;

            entity.Id = Guid.NewGuid();
            entity.Bike = bike;
            entity.User = user;
            entity.Plan = plan;

            entity.CreationDate = DateOnly.FromDateTime(DateTime.Now);
            entity.InitialDate = entity.CreationDate.AddDays(1);
            entity.PredictionDate = DateOnly.FromDateTime(DateTime.Now.AddDays(plan.NumDays));
            entity.EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(plan.NumDays));

            var result = await _unitOfWork.rentalRepository.Insert(entity);

            _unitOfWork.CommitAsync().Wait();

            if (result != null)
                return ServiceResponseFactory<Rental>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Inclusão de Locação Realizada Com Sucesso.", result);
            else
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não foi possível incluir a Locação! Verifique os dados enviados.", result!);
        }

        public override async Task<ServiceResponseFactory<Rental>> Update(RentalRequest request)
        {
            if (request is null || !Guid.TryParse(request.Id.ToString(), out _))
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            var entity = _unitOfWork!.rentalRepository.GetById(request.Id).Result;

            if (entity is null)
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id informado inválido.", entity!);

            var bike = _unitOfWork!.bikeRepository.GetById(request.BikeId).Result;

            if (bike is null)
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id da moto inválido!", entity!);

            bike.IsLeased = false;

            _mapper!.Map(request, entity);

            var result = await _unitOfWork.rentalRepository.Update(entity);

            _unitOfWork.CommitAsync().Wait();

            if (result)
                return ServiceResponseFactory<Rental>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Atualização de Locação realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status304NotModified, "Não foi possível atualizar a Locação.", entity!);
        }

        public override async Task<ServiceResponseFactory<Rental>> Delete(RentalRequest request)
        {
            if (!Guid.TryParse(request.Id.ToString(), out _))
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            var entity = await _unitOfWork!.rentalRepository.GetById(request.Id);

            if (entity is null)
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status404NotFound, "Id informado inválido!");

            entity.EndDate = DateOnly.FromDateTime(DateTime.Now);
            entity.IsActive = false;

            var result = await _unitOfWork.rentalRepository.Update(entity);

            //Recupero a bike usada na locação e atualizo o seu status
            var bike = _unitOfWork!.bikeRepository.GetById(entity.Bike.Id).Result;
            bike!.IsLeased = false;

            await _unitOfWork.bikeRepository.Update(bike);

            _unitOfWork.CommitAsync().Wait();

            if (result)
                return ServiceResponseFactory<Rental>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Remoção de Locação realizada com sucesso.", entity);
            else
                return ServiceResponseFactory<Rental>.Return(false, Application.Helpers.EnumStatusCode.Status304NotModified, "Não foi possível remover a Locação.", entity!);
        }
    }
}
