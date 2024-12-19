using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.pl.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Max Length For Name Is 50")]
        [MinLength(5, ErrorMessage = "Min Length For Name Is 5")]
        public string name { get; set; }

        [Range(21, 60)]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
                          ErrorMessage = "Address Must be Like 123-Ismaeel-Asyot-Egypt")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        public IFormFile Image { get; set; }
        public  string ImaegeName { get; set; }
        //[ForeignKey("DepartmentId")]
        public int? DepartmetnId { get; set; }
        //[InverseProperty(nameof(Models.Department.Employees))]
        public Department Department { get; set; } //navigation proporty => [one]

    }
}
