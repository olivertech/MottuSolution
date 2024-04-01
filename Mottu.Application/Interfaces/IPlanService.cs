namespace Mottu.Application.Interfaces
{
    public interface IPlanService : IServiceBase<Plan, PlanRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<Plan>>> GetListByName(string name);
    }
}
