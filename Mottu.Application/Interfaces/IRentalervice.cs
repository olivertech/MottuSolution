using Mottu.Application.Interfaces.Base;
using Mottu.Application.Responses;
using Mottu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Application.Interfaces
{
    public interface IRentalService : IServiceBase<Rental, RentalResponse>
    {
    }
}
