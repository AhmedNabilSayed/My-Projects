using Demo.DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {

        IEnumerable<Employee> Search(string? name);
		IEnumerable<Employee> GetEmployeeByDepartment(int? deptId);

		//Employee GetEmployeeById(int? id);
		//IEnumerable<Employee> GetAllEmployees();
		//int Add(Employee employee);
		//int Update(Employee employee);
		//int Delete(Employee employee);
	}
}
