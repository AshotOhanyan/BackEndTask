using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>>  GetAllDbObjectsAsync();
        public Task<T>  GetDbObjectByIdAsync(Guid id);
        public IQueryable<T>  GetDbObjectsByFilter(T filter);
        public Task<T>  AddDbObjectAsync(T newObject);
        public Task<T>  UpdateDbObjectAsync(T updatedObject);
        public Task  RemoveDbObjectAsync(Guid id);
    }
}
