﻿using Mottu.Application.Interfaces.Base;
using Mottu.Application.Requests;
using Mottu.Domain.Entities;

namespace Mottu.Application.Interfaces
{
    public interface IStatusOrderService : IServiceBase<StatusOrder, StatusOrderRequest>
    {
    }
}
