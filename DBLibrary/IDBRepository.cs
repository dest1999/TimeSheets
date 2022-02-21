using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public interface IDBRepository<T> where T : Person
    {
        void Create(Person entity);
        Person Read(int id);
        IEnumerable<Person> Find(string name);
        IEnumerable<Person> Read(int skip, int take);
        void Update(Person entity);
        void Delete(int id);
        IList<T> GetAll();
    }
}
