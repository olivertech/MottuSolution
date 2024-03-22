using AutoMapper;
using Mottu.Application.Services.Base;
using Mottu.Application.Responses;
using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mottu.Application.Interfaces.Base;
using Mottu.Application.Interfaces;

namespace Mottu.Application.Services
{
    public class UserTypeService : ServiceBase<UserType, UserTypeResponse>, IUserTypeService
    {
        protected readonly IMapper? _mapper;

        public UserTypeService(IUnitOfWork unitOfWork, IMapper? mapper) 
            : base(unitOfWork)
        {
            _mapper = mapper;
        }

        public override Task<IEnumerable<UserType>?> GetAll()
        {
            return _unitOfWork!.userTypeRepository!.GetAll();
        }
    }
}
