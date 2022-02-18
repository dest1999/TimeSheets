using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class EmployeeDBRepository : IDBRepository<Employee>
    {
        public void Create(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee Read(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> Read(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> Read(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
