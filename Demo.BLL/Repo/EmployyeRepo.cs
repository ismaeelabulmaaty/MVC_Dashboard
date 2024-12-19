using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repo
{
    public class EmployyeRepo : GenericRepo<Employees>, IEmployeeRepo

    {
        public EmployyeRepo(AppDbContext dbcontext) : base(dbcontext)
        {

        }

        public IQueryable<Employees> GetEmployeeByAddress(string address)
        {
            return _dbContext.Employee.Where(E => E.Address.ToLower().Contains(address.ToLower()));
        }

        public IQueryable<Employees> GetEmployeeByName(string Name)
        {
           return _dbContext.Employee.Where(E=>E.name.ToLower().Contains(Name.ToLower()));
        }

      
    }
}
