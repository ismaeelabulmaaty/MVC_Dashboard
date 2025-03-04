﻿using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Configrations
{
    public class EmployyesConfigration : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.Property(E => E.Salary)
                   .HasColumnType("decimal(18,2)");


            builder.Property(E => E.name)
                   .IsRequired(true)
                   .HasMaxLength(50);
                   
        }
    }
}

