using AutoMapper;
using Mottu.Api.Classes;
using Mottu.Application.Messaging;
using Mottu.Application.Requests;
using Mottu.Application.Responses;
using Mottu.Domain.Entities;

namespace Mottu.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /**
             * Mapping Requests ( Request -> Model )
             */
            CreateMap<UserTypeRequest, UserType>();
            CreateMap<StatusOrderRequest, StatusOrder>();
            CreateMap<BikeRequest, Bike>();
            CreateMap<CnhTypeRequest, CnhType>();
            CreateMap<PlanRequest, Plan>();
            CreateMap<RentalRequest, Rental>();
            CreateMap<AppUserRequest, AppUser>();
            CreateMap<OrderRequest, Order>();

            /**
             * Mapping Responses ( Response <- Model )
             */
            CreateMap<UserType, UserTypeResponse>();
            CreateMap<StatusOrder, StatusOrderResponse>();
            CreateMap<Bike, BikeResponse>();
            CreateMap<CnhType, CnhTypeResponse>();
            CreateMap<Plan, PlanResponse>();
            CreateMap<Rental, RentalResponse>();
            CreateMap<AppUser, AppUserResponse>();
            CreateMap<Order, OrderResponse>();
            CreateMap<AcceptedOrder, AcceptedOrderResponse>();
            CreateMap<DeliveredOrder, DeliveredOrderResponse>();
            CreateMap<NotificationMessage, OrderResponse>();
        }
    }
}
