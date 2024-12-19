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
    public class GenericRepo<T> : IGenericReposatory<T> where T : ModelBase
    {

        private protected readonly AppDbContext _dbContext;// null
        public GenericRepo(AppDbContext dbcontext)
        {
            //_dbContext = new AppDbContext();
            _dbContext = dbcontext;
        }
        public int Add(T entity)
        {

            _dbContext.Set<T>().Add(entity);//stat Added
            //_dbContext.Add(entity); طريقة تاني
            return _dbContext.SaveChanges();
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            //var Department= _dbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();

            //if (Department == null)
            //{
            //    _dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();
            //}
            //return Department;

            return _dbContext.Set<T>().Find(id);//find =>pk
            //return _dbContext.Find<Department>(id); //feuature 3.1 core
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employees)){
                return (IEnumerable<T>)_dbContext.Employee.Include(E=>E.Department).AsNoTracking().ToList();
            }
            else
            {
                return _dbContext.Set<T>().AsNoTracking().ToList();
            }
            
        }


    }
}
