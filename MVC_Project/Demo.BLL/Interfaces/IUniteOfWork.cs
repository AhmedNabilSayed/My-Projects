using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUniteOfWork
    {
        public IDepartmentReopsitory DepartmentReopsitory { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
    }
}
