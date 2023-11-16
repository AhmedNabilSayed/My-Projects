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
	public class DepartmentRepository : GenericRepository<Department> , IDepartmentReopsitory
	{
		private readonly MVCAppDbContext context;

		public DepartmentRepository(MVCAppDbContext context) : base(context)
		{
			this.context = context;
		}

		//public int Add(Employee department)
		//{
		//	context.Departments.Add(department);
		//	return context.SaveChanges();
		//}

		//public int Delete(Employee department)
		//{
		//	context.Departments.Remove(department);
		//	return context.SaveChanges();
		//}

		//public IEnumerable<Employee> GetAllDepartments()
		//=> context.Departments.ToList();

		//public Employee GetDepartmentById(int? id)
		//=> context.Departments.SingleOrDefault(x => x.Id == id);

		//public int Update(Employee department)
		//{
		//	context.Departments.Update(department);
		//	return context.SaveChanges();
		//}
	}
}
