using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class EmployeeDBRepository : IEmployeeDBRepository
    {
        private SQLiteDBContext db;
        public EmployeeDBRepository(SQLiteDBContext context)
        {
            db = context;
        }
        public async Task Create(Employee employee)
        {
            db.Add(employee);
            await db.SaveChangesAsync();
        }

        public async Task<Employee> Get(int id)
        {
            return await db.FindAsync<Employee>(id);
        }

        public async Task Update(Employee employee)
        {
            var old = Get(employee.Id).Result;
            old.FirstName = employee.FirstName;
            old.LastName = employee.LastName;
            old.Email = employee.Email;
            old.Company = employee.Company;
            old.Age = employee.Age;
            old.IsDeleted = employee.IsDeleted;
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var employee = Get(id).Result;
            employee.IsDeleted = true;
            await db.SaveChangesAsync();
        }
    }
}
