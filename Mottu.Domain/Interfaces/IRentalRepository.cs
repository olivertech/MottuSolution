﻿using Mottu.Domain.Entities;
using Mottu.Domain.Interfaces.Base;

namespace Mottu.Domain.Interfaces
{
    public interface IRentalRepository : IRepositoryBase<Rental>
    {
        Task<IEnumerable<Rental>?> GetFullAll();
        Task<Rental?> GetFullById(Guid? id);
    }
}