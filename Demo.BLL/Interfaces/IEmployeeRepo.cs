﻿using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepo : IGenericReposatory<Employees>
    {    
        IQueryable<Employees> GetEmployeeByAddress(string address);

        IQueryable<Employees> GetEmployeeByName(string Name);
    }
}

