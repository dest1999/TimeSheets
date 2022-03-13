using AutoMapper;
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
        private Mapper mapper;
        public EmployeeDBRepository(SQLiteDBContext context)
        {
            db = context;
            MapperConfiguration mapperConfiguration = new (cfg => cfg.CreateMap<EmployeeDTO, Employee>());
            mapper = new Mapper(mapperConfiguration);
        }
        public async Task Create(EmployeeDTO employeeDTO)
        {
            Employee employee = mapper.Map<Employee>(employeeDTO);
            employee.Id = db.Employees.Count() + 1;
            db.Add(employee);
            await db.SaveChangesAsync();
        }

        public async Task<Employee> Get(int id)
        {
            return await db.FindAsync<Employee>(id);
        }
        public Employee? GetByLogin(string login)
        {
            return db.Employees.FirstOrDefault(e => e.Login == login);
        }
        public Employee GetByToken(string token)
        {
            return db.Employees.FirstOrDefault(e =>e.Token == token);
        }
        public async Task Update(Employee employee)
        {
            var old = Get(employee.Id).Result;
            old.FirstName = employee.FirstName;
            old.LastName = employee.LastName;
            old.Email = employee.Email;
            old.Company = employee.Company;
            old.Age = employee.Age;
            //old.IsDeleted = employee.IsDeleted; для удаления отдельный метод
            old.Login = employee.Login;
            old.Password = employee.Password; 
            old.Token = employee.Token;
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
