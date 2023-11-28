using Common.Interfaces;
using Common.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Presentation.Controllers
{
    public class EmployeesController : Controller
    {
        /*
         * i.SearchEmployee: should return only the ID, Name & Department of 
         * employees matching part of the name; [1] 
         * 
         * ii.RegisterEmployee: 
         * after you validate that no employee carrying the same PassportNo 
         * has been already registered.If it happens throw an Exception [1]
         * */

        private IEmployeesRepository _employeeDbRepository;
        public EmployeesController(  IEmployeesRepository employeeDbRepository) {
        _employeeDbRepository= employeeDbRepository;
        
        }

        public IActionResult SearchEmployee(string partOfTheName)
        {
            var list = _employeeDbRepository.GetEmployees().Where(x=>
            x.Name.Contains(partOfTheName));

            var result = from employee in list
                         select new EmployeeViewModel()
                         {
                             ID = employee.ID,
                             Name = employee.Name,
                             Department = employee.Department.Name
                         };

            return View(result);
        }

        public IActionResult RegisterEmployee()
        { return View();
        }


        [HttpPost]
        public IActionResult RegisterEmployee(RegisterEmployeeViewModel input )
        {
            var foundEmployee = _employeeDbRepository.GetEmployees().SingleOrDefault(x =>
            x.PassportNo== input.PassportNo);

            if (foundEmployee == null)
            {
                _employeeDbRepository.AddEmployee(new Employee()
                {
                    Name = input.Name,
                    PassportNo = input.PassportNo,
                    Password = input.Password,
                    Username = input.Username,
                    DepartmentFk = input.DepartmentFk
                });
            }
            else
            {
                throw new Exception("Employee already exists");
            }
            return View(foundEmployee);

        }


    }
}
