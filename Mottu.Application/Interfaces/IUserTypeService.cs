namespace Mottu.Application.Interfaces
{
    public interface IUserTypeService : IServiceBase<UserType, UserTypeRequest>
    {
        Task<ServiceResponseFactory<IEnumerable<UserType>>> GetListByName(string name);
    }
}
