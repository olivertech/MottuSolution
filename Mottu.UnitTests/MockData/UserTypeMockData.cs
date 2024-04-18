using Mottu.Application.Services;
using Mottu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.UnitTests.MockData
{
    public class UserTypeMockData
    {
        public static ServiceResponseFactory<IEnumerable<UserType>> GetUserTypes()
        {
            var list = GetUserTypeList();

            return ServiceResponseFactory<IEnumerable<UserType>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Lista de Tipos de Usuário recuperada com sucesso.", list!);
        }

        public static ServiceResponseFactory<UserType> GetUserType(Guid id)
        {
            var list = GetUserTypeList();

            var entity = list.Where(x => x.Id == id).FirstOrDefault();

            return ServiceResponseFactory<UserType>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Tipo de Usuário recuperado com sucesso.", entity!);
        }

        public static ServiceResponseFactory<IEnumerable<UserType>> GetUserTypeByName(string name)
        {
            var data = GetUserTypeList();

            var list = data.Where(x => x.Name == name).ToList();

            return ServiceResponseFactory<IEnumerable<UserType>>.Return(true, Application.Helpers.EnumStatusCode.Status200OK, "Lista de Tipos de Usuário por nome recuperado com sucesso.", list!);
        }

        private static List<UserType> GetUserTypeList()
        {
            return new List<UserType>{
                 new UserType{
                     Id = Guid.Parse("f6a2372a-b146-45f9-be70-a0be13736dd8"),
                     Name = "ADMINISTRADOR"
                 },
                 new UserType{
                     Id = Guid.Parse("bbd4ed2e-5f92-414c-9f7b-e7395c464898"),
                     Name = "ENTREGADOR"
                 },
                 new UserType{
                     Id = Guid.Parse("79fd21e3-3127-4dda-ba1b-17d20a9d1d3e"),
                     Name = "CONSUMIDOR"
                 }
             };
        }
    }
}
