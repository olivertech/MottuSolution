using Mottu.Application.InterfacesMDB.Base;

namespace Mottu.Application.InterfacesMDB
{
    public interface IPlanServiceMDB : IServiceBaseMDB<Plan, PlanRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<Plan>>> GetListByName(string name);
    }
}
