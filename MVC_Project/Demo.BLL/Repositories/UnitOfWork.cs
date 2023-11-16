using Demo.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUniteOfWork
    {
        public IDepartmentReopsitory DepartmentReopsitory { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public UnitOfWork(IDepartmentReopsitory departmentReopsitory , IEmployeeRepository employeeRepository) 
        {
            DepartmentReopsitory = departmentReopsitory;
            EmployeeRepository = employeeRepository;
        }
    }
}
