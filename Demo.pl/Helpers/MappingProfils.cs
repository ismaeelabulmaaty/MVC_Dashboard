using AutoMapper;
using Demo.DAL.Models;
using Demo.pl.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.pl.Helpers
{
    public class MappingProfils : Profile
    {

        public MappingProfils()
        {

            CreateMap<EmployeeViewModel, Employees>().ReverseMap();
            CreateMap<SignUpViewModel,ApplactionUser>()
                    .ForMember(d=>d.UserName,o=>o.MapFrom(s=>s.FName)).ReverseMap();
            CreateMap<ApplactionUser,UserViewModel>().ReverseMap();
            CreateMap<IdentityRole,RolesViewModel>()
                     .ForMember(d=>d.RoleName,o=>o.MapFrom(s=>s.Name)).ReverseMap();

        }

    }
}
