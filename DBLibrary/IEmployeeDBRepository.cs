﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public interface IEmployeeDBRepository
    {
        Task Create(Employee user);
        Task<Employee> Get(int id);
        Task Update(Employee user);
        Task Delete(int id);
        Employee GetByLoginAsync(string login);
    }
}
