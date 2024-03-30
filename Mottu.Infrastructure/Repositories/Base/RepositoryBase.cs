namespace Mottu.Infrastructure.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> 
        where T : BaseEntity
    {
        protected readonly AppDbContext? _context;
        protected readonly DbSet<T>? _entities;

        public RepositoryBase([NotNull] AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        /// <summary>
        /// Método para recuperar todos os registros 
        /// da entidade. Esse método pode ser sobrescrito
        /// </summary>
        /// <returns></returns>
        public async virtual Task<IEnumerable<T>?> GetAll()
        {
            try
            {
                var list = await _entities!.ToListAsync();
                
                return list ?? Enumerable.Empty<T>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que recupera um registro da entidade
        /// a partir do seu id. Esse método pode ser sobrescrito
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async virtual Task<T?> GetById(Guid? id)
        {
            try
            {
                var entity = await _entities!.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (entity == null)
                    throw new InvalidOperationException("Registro não encontrado!");

                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que recupera lista de registros da entidade
        /// baseada em uma expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async virtual Task<IEnumerable<T>?> GetList(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var list = await _entities!.Where(predicate).ToListAsync();
                
                return list ?? Enumerable.Empty<T>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que recupera o total de registros da entidade.
        /// Esse método pode ser sobrescrito
        /// </summary>
        /// <returns></returns>
        public async virtual Task<int> Count()
        {
            try
            {
                var list = await _entities!.ToListAsync();
                
                return list.Count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que insere um novo registro.
        /// Esse método pode ser sobrescrito
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async virtual Task<T?> Insert(T entity)
        {
            try
            {
                if (entity is null)
                    throw new ArgumentNullException(nameof(entity));

                await _entities!.AddAsync(entity);
                
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que atualiza um registro.
        /// Esse método pode ser sobrescrito
        /// </summary>
        /// <param name="entity"></param>
        public async virtual Task<bool> Update(T entity)
        {
            try
            {
                if (entity is null)
                    throw new ArgumentNullException(nameof(entity));

                var item = await _entities!.FindAsync(entity.Id);
                
                if (item is not null)
                    _entities.Update(entity);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Método que remove um registro.
        /// Esse método pode ser sobrescrito
        /// </summary>
        /// <param name="id"></param>
        public async virtual Task<bool> Delete(Guid? id)
        {
            try
            {
                var entity = await GetById(id);

                if (entity is null)
                    throw new InvalidOperationException("Registro não encontrado!");

                _entities!.Remove(entity);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
