using AutoMapper;
using Demo.DAL.Models;
using Demo.pl.Helpers;
using Demo.pl.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.pl.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplactionUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplactionUser> userManager,RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
           _userManager = userManager;
           _roleManager = roleManager;
           _mapper = mapper;
        }
        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var users = await _userManager.Users.Select(u => new UserViewModel
                {
                    Id = u.Id,
                    FName = u.FName,
                    LName = u.LName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = _userManager.GetRolesAsync(u).Result

                }).ToListAsync();
                return View(users);
            }
            else
            {
                var user= await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var mappedUser = new UserViewModel
                    {
                        Id = user.Id,
                        FName = user.FName,
                        LName = user.LName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Role = _userManager.GetRolesAsync(user).Result
                    };
                    return View(new List<UserViewModel> { mappedUser});
                }
            }
           return View(Enumerable.Empty<UserViewModel>());
        }

        public  async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();//400
            }

            var user = await _userManager.FindByIdAsync(id);
            //var MappedEmp = _mapper.Map<Employees, EmployeeViewModel>(Employees);

            if (user is null)
            {
                return NotFound();
            }
            var mapped=  _mapper.Map<ApplactionUser,UserViewModel>(user);

            return View(viewName, mapped);
        }

        public async Task<IActionResult> Edit(string id)
        {
      
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel UserVm)
        {

            if (id != UserVm.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var user = await _userManager.FindByIdAsync(id);
                    if (user != null) 
                    {
                        user.FName = UserVm.FName;
                        user.LName = UserVm.LName;
                        user.PhoneNumber = UserVm.PhoneNumber;
                        await _userManager.UpdateAsync(user);

                        return RedirectToAction(nameof(Index));
                    }
                    

                }
                catch (Exception ex)
                {
                   
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(UserVm);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await  Details(id,"Delete");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete( string id)
        {
       
            try
            {
                var user= await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
          
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View("Error","Home");
        }


    }
}
