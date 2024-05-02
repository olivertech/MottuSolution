using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Mottu.Application.Classes;
using Mottu.Application.InterfacesMDB;
using Mottu.Application.RequestsMDB;
using Mottu.Application.ServicesMDB.Base;
using Mottu.Domain.EntitiesMDB;

namespace Mottu.Application.ServicesMDB
{
    public class UserTypeServiceMDB : ServiceBaseMDB<UserTypeMDB, UserTypeRequestMDB>, IUserTypeServiceMDB
    {
        protected readonly IMapper? _mapper;
        private readonly IMongoCollection<UserTypeMDB>? _userTypeCollection;

        public UserTypeServiceMDB(IOptions<MongoDBSettings> mongDbSettings,
                                  IMongoClient? client,
                                  IMapper? mapper)
            : base(mongDbSettings, client)
        {
            _mapper = mapper;
        }

        public override async Task<ServiceResponseFactory<IEnumerable<UserTypeMDB>>> GetAll()
        {
            var list = await _collection.Find(_ => true).ToListAsync();
            return ServiceResponseFactory<IEnumerable<UserTypeMDB>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Lista de Tipos de Usuário recuperada com sucesso.", list!);
        }

        public override async Task<ServiceResponseFactory<UserTypeMDB>> GetById(Guid id)
        {
            if (!Guid.TryParse(id.ToString(), out _))
                return ServiceResponseFactory<UserTypeMDB>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            var entity = await _collection.Find(x => x.Id!.Equals(id)).FirstOrDefaultAsync();

            if (entity == null)
                return ServiceResponseFactory<UserTypeMDB>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            return ServiceResponseFactory<UserTypeMDB>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Tipo de Usuário recuperado com sucesso.", entity!);
        }

        public async Task<ServiceResponseFactory<IEnumerable<UserTypeMDB>>> GetListByName(string name)
        {
            if (name is null)
                return ServiceResponseFactory<IEnumerable<UserTypeMDB>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Nome inválido!");

            var entities = await _collection.Find(x => x.Name!.ToLower() == name.ToLower()).ToListAsync();

            if (entities != null)
                return ServiceResponseFactory<IEnumerable<UserTypeMDB>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Tipo de usuário recuperado por nome com sucesso.", entities);
            else
                return ServiceResponseFactory<IEnumerable<UserTypeMDB>>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Não existe Tipo de Usuário para o nome informado!");
        }

        public override async Task<ServiceResponseFactory<long>> GetCount()
        {
            var numEntities = await _collection.Find(_ => true).CountDocumentsAsync();
            return ServiceResponseFactory<long>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Total de Tipos de Usuário recuperado com sucesso.", numEntities);
        }

        public override async Task<ServiceResponseFactory<UserTypeMDB>> Insert(UserTypeRequestMDB request)
        {
            if (request is null)
                return ServiceResponseFactory<UserTypeMDB>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            var search = await _collection.Find(_ => true).ToListAsync();

            if (search!.Any(x => x.Name == request.Name && x.Id != request.Id))
            {
                var existEntity = search!.Find(x => x.Name == request.Name);
                return ServiceResponseFactory<UserTypeMDB>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe um Tipo de Usuário com esse nome.", existEntity!);
            }

            var entity = _mapper!.Map<UserTypeMDB>(request);

            entity.Id = ObjectId.GenerateNewId().ToString();
            entity.Name = request.Name!;

            await _collection.InsertOneAsync(entity);

            return ServiceResponseFactory<UserTypeMDB>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Inclusão de Tipo de Usuário realizada com sucesso.", entity);
        }

        public override async Task<ServiceResponseFactory<UserTypeMDB>> Update(UserTypeRequestMDB request)
        {
            if (request is null || !Guid.TryParse(request.Id!.ToString(), out _))
                return ServiceResponseFactory<UserTypeMDB>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Request inválido!");

            var entity = _collection.Find(x => x.Id!.Equals(request.Id)).FirstOrDefaultAsync();

            if (entity is null)
                return ServiceResponseFactory<UserTypeMDB>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id informado inválido.", entity!.Result!);

            var search = await _collection.Find(_ => true).ToListAsync();

            if (search!.Any(x => x.Name == request.Name && x.Id != request.Id))
            {
                var existEntity = search!.Where(x => x.Name == request.Name).FirstOrDefault();
                return ServiceResponseFactory<UserTypeMDB>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Já existe um Tipo de Usuário com esse nome.", existEntity!);
            }

            request.Name = request.Name!.ToUpper();

            await _mapper!.Map(request, entity);

            var result = await _collection.ReplaceOneAsync(s => s.Id == request.Id, entity.Result);

            return ServiceResponseFactory<UserTypeMDB>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Atualização de Tipo de Usuário realizada com sucesso.", entity.Result);
        }

        public override async Task<ServiceResponseFactory<UserTypeMDB>> Delete(UserTypeRequestMDB request)
        {
            if (!Guid.TryParse(request.Id!.ToString(), out _))
                return ServiceResponseFactory<UserTypeMDB>.Return(false, Application.Helpers.EnumStatusCode.Status400BadRequest, "Id inválido!");

            var entity = await _collection.Find(s => s.Id == request.Id).FirstOrDefaultAsync();

            if (entity is null)
                return ServiceResponseFactory<UserTypeMDB>.Return(false, Application.Helpers.EnumStatusCode.Status404NotFound, "Id informado inválido!");

            var result = await _collection.DeleteOneAsync(s => s.Id == request.Id);

            return ServiceResponseFactory<UserTypeMDB>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Remoção do Tipo de Usuário realizada com sucesso.", entity);
        }
    }
}
