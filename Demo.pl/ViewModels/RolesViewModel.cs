using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.pl.ViewModels
{
    public class RolesViewModel
    {
        public string Id { get; set; }
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }

        public RolesViewModel()
        {
            Id=Guid.NewGuid().ToString();
        }

    }
}
