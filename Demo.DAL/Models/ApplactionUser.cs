﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
	public class ApplactionUser : IdentityUser
	{
        public bool IsAgree { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

    }
}
