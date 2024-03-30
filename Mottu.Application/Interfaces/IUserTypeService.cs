using Mottu.Application.Interfaces.Base;
using Mottu.Application.Requests;
using Mottu.Application.Responses;
using Mottu.Application.Services;
using Mottu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Application.Interfaces
{
    public interface IUserTypeService : IServiceBase<UserType, UserTypeRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<UserType>>> GetListByName(string name);
    }
}
