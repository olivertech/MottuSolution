using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mottu.Infrastructure.Repositories.Base
{
    public abstract class RepositoryBaseWithRedis<T> : IRepositoryBase<T> 
        where T : BaseEntity
    {
        protected readonly AppDbContext? _context;
        protected readonly IDistributedCache? _cache;
        protected readonly DbSet<T>? _entities;
        protected readonly string? _redisKey;
        protected readonly DistributedCacheEntryOptions? _cacheOptions;

        public RepositoryBaseWithRedis([NotNull] AppDbContext context, IDistributedCache cache, string redisKey)
        {
            _context = context;
            _cache = cache;
            _redisKey = redisKey;
            _entities = _context.Set<T>();

            // =================================================================
            // - Tempo absoluto de 1 hora para remoção de fato do cache
            // - Tempo relativo de 20 minutos, para em caso de nenhum acesso
            //   dentro desse tempo, o cache é removido, ou a cada nova
            //   requisição, o tempo de 20 minutos é reiniciado
            // =================================================================
            _cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600), //Tempo total de remoção da memória de 1 hora
                SlidingExpiration = TimeSpan.FromSeconds(1200) //Tempo relativo de remoção da memória de 20 minutos
            };
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
                var redisObj = await _cache!.GetStringAsync(_redisKey!);

                //Se houverem dados armazenados em cache, retornar
                if(!string.IsNullOrWhiteSpace(redisObj))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<T>>(redisObj);
                }
                else
                {
                    var list = await _entities!.ToListAsync();
                    await _cache!.SetStringAsync(_redisKey!, JsonConvert.SerializeObject(list), _cacheOptions!);

                    return list ?? Enumerable.Empty<T>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RepositoryError - Não foi possível recuperar a lista.", ex);
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
            catch (Exception ex)
            {
                throw new Exception("RepositoryError - Não foi possível recuperar a id informado.", ex);
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
            catch (Exception ex)
            {
                throw new Exception("RepositoryError - Não foi possível recuperar a lista.", ex);
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
            catch (Exception ex)
            {
                throw new Exception("RepositoryError - Não foi possível recuperar o total.", ex);
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

                var redisObj = await _cache!.GetStringAsync(_redisKey!);

                //Se houverem dados armazenados em cache, remove o cache
                if (!string.IsNullOrWhiteSpace(redisObj))
                {
                    _cache!.Remove(_redisKey!);
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("RepositoryError - Não foi possível inserir a entidade.", ex);
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

                var redisObj = await _cache!.GetStringAsync(_redisKey!);

                //Se houverem dados armazenados em cache, remove o cache
                if (!string.IsNullOrWhiteSpace(redisObj))
                {
                    _cache!.Remove(_redisKey!);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("RepositoryError - Não foi possível atualizar a entidade.", ex);
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

                var redisObj = await _cache!.GetStringAsync(_redisKey!);

                //Se houverem dados armazenados em cache, remove o cache
                if (!string.IsNullOrWhiteSpace(redisObj))
                {
                    _cache!.Remove(_redisKey!);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("RepositoryError - Não foi possível remover a entidade.", ex);
            }
        }
    }
}
