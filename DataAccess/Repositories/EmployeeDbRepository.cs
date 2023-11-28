using Common.Interfaces;
using Common.Models;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EmployeeDbRepository : IEmployeesRepository
    {
        EmployeeDbContext _context;
        public EmployeeDbRepository(EmployeeDbContext context) { 
        _context= context;
        }

        public IQueryable<Employee> GetEmployees()
        {
            return _context.Employees;
        }

        public void AddEmployee(Employee employee) {
         _context.Employees.Add(employee);
            _context.SaveChanges();
        }
    }
}
