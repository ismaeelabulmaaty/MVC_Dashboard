using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IunitOfWork: IDisposable
    {

        public IEmployeeRepo EmployeeRepository { get; set; }

        public IDepartmentRepo DepartmentRepository { get; set; }

        int Complet();
    
    }
}
