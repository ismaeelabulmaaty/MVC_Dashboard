using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repo
{
    public class unitOfWork : IunitOfWork
    {
        private readonly AppDbContext _dbContext;

        public IEmployeeRepo EmployeeRepository { get ; set ; }
        public IDepartmentRepo DepartmentRepository { get ; set ; }

        public unitOfWork(AppDbContext dbContext)
        {
            EmployeeRepository = new EmployyeRepo(dbContext);

            DepartmentRepository = new DepartmentRepo(dbContext);

            _dbContext = dbContext;
        }

        public int Complet()
        {
           return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
