namespace Mottu.Infrastructure.Repositories
{
    public class CnhTypeRepository : RepositoryBase<CnhType>, ICnhTypeRepository
    {
        public CnhTypeRepository([NotNull] AppDbContext context) : base(context)
        {
        }
    }
}
