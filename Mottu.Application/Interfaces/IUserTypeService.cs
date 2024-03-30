using Mottu.Application.Interfaces.Base;
using Mottu.Application.Requests;
using Mottu.Application.Services;
using Mottu.Domain.Entities;

namespace Mottu.Application.Interfaces
{
    public interface IUserTypeService : IServiceBase<UserType, UserTypeRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<UserType>>> GetListByName(string name);
    }
}
