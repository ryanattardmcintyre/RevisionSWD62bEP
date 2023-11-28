using Common.Interfaces;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EmployeeFileRepository: IEmployeesRepository
    {
        string _path;
        public EmployeeFileRepository(string filePath) {
            _path = filePath;

            if (File.Exists(_path)== false) {
            
             using (var myFile = File.CreateText(_path))
                {
                    myFile.Close();
                }
            
            }
        
        }

        //to consider the txt file as a json file
        //assuming that on a new fresh line there will be a new employee stored
        public IQueryable<Employee> GetEmployees()
        {
            string[] employees = File.ReadAllLines(_path);

            List<Employee> result = new List<Employee>();
            foreach (string employee in employees)
            {
                //1,ryan,ict,1234567,ryanattard,123
                string[] employeeDetails = employee.Split(',');
                Employee myEmp = new Employee()
                {
                    ID = Convert.ToInt32(employeeDetails[0]),
                    Name = employeeDetails[1],
                    DepartmentFk = Convert.ToInt32(employeeDetails[2]),
                    PassportNo = employeeDetails[3],
                    Username = employeeDetails[4],
                    Password = employeeDetails[5]
                };

                result.Add(myEmp);
            }

            return result.AsQueryable();
        }

            public void AddEmployee(Employee employee)
            {

            int count = GetEmployees().Count()+1;

            string employeeAsAString = $"{count},{employee.Name},{employee.DepartmentFk},{employee.PassportNo},{employee.Username},{employee.Password}";
            File.AppendAllLines(_path, new List<string>() { employeeAsAString });
            }
    }


 
}
