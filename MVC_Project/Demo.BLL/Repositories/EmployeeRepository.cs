using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        private readonly MVCAppDbContext context;

        public EmployeeRepository(MVCAppDbContext context) : base(context) 
        {
            this.context = context;
        }

        public IEnumerable<Employee> GetEmployeeByDepartment(int? deptId)
        => context.Employees.Where(x => x.DepartmentId == deptId);



        public IEnumerable<Employee> Search(string? name)
        => context.Employees.Where(e => e.Name.Trim().ToLower().Contains(name.Trim().ToLower()));

    

        //public int Add(Employee employee)
        //{
        //    context.Employees.Add(employee);
        //    return context.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    context.Employees.Remove(employee);
        //    return context.SaveChanges();
        //}

        //public IEnumerable<Employee> GetAllDepartments()
        //=> context.Employees.ToList();

        //public Employee GetDepartmentById(int? id)
        //=> context.Employees.FirstOrDefault(x => x.Id == id);


        //public int Update(Employee employee)
        //{
        //    context.Employees.Update(employee);
        //    return context.SaveChanges();
        //}
    }
}
