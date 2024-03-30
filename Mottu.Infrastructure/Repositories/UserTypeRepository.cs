namespace Mottu.Infrastructure.Repositories
{
    public class UserTypeRepository : RepositoryBase<UserType>, IUserTypeRepository
    {
        public UserTypeRepository([NotNull] AppDbContext context) : base(context)
        {
        }
    }
}
