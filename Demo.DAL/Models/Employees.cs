using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Demo.DAL.Models
{
    public class Employees:ModelBase
    {


        [Required]
        [MaxLength(50)]
        public string name { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public  string ImaegeName { get; set; }
        //[ForeignKey("DepartmentId")]
        public int? DepartmetnId { get; set; }
        //[InverseProperty(nameof(Models.Department.Employees))]
        public Department Department { get; set; } //navigation proporty => [one]
    }
}
