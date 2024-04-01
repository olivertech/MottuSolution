namespace Mottu.Application.Helpers
{
    public class ConvertModelToResponse<M,R> 
        where M : class
        where R : new()
    {
        protected IMapper? _mapper;

        public ConvertModelToResponse(IMapper? mapper)
        {
            _mapper = mapper;
        }

        public List<R> GetResponsList(IEnumerable<M> list)
        {
            List<R> result = new List<R>();
            
            foreach (var item in list)
            {
                R obj = new R();
                _mapper!.Map(item, obj);
                result.Add(obj);
            }

            return result;
        }
    }
}
