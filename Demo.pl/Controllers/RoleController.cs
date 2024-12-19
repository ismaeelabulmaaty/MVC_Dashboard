using Demo.DAL.Models;
using Demo.pl.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Demo.pl.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController( RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var roles = await _roleManager.Roles.Select(R => new RolesViewModel
                {
                    Id = R.Id,
                    RoleName = R.Name,
                    

                }).ToListAsync();
                return View(roles);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(name);
                if (role != null)
                {
                    var mappedRole = new RolesViewModel
                    {
                        Id = role.Id,
                         RoleName= role.Name,
                    };
                    return View(new List<RolesViewModel> { mappedRole });
                }
            }
            return View(Enumerable.Empty<RolesViewModel>());
        }

        public IActionResult Creat()
        {
            return  View();
        }

        [HttpPost]
        public async Task<IActionResult> Creat(RolesViewModel RoleVm)
        {

            if (ModelState.IsValid)
            {
                var mappedRole=_mapper.Map<RolesViewModel,IdentityRole>(RoleVm);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction(nameof(Index));

            }
            return View(RoleVm);

        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();//400
            }

            var Role = await _roleManager.FindByIdAsync(id);
            //var MappedEmp = _mapper.Map<Employees, EmployeeViewModel>(Employees);

            if (Role is null)
            {
                return NotFound();
            }
            var mappedRole = _mapper.Map<IdentityRole, RolesViewModel>(Role);

            return View(viewName, mappedRole);
        }

        public async Task<IActionResult> Edit(string id)
        {

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RolesViewModel RoleVm)
        {

            if (id != RoleVm.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var role = await _roleManager.FindByIdAsync(id);
                    if (role != null)
                    {

                        role.Name = RoleVm.RoleName;
                        await _roleManager.UpdateAsync(role);

                        return RedirectToAction(nameof(Index));
                    }


                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(RoleVm);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {

            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(role);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View("Error", "Home");
        }
    }
}
