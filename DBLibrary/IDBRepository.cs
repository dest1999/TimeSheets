using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public interface IDBRepository<T> where T : Person
    {
        void Create(T entity);
        T Read(int id);
        IEnumerable<T> Read(string name);
        IEnumerable<T> Read(int skip, int take);
        void Update(T entity);
        void Delete(int id);
        IList<T> GetAll();
    }
}
