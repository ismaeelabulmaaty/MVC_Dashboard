using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repo;
using Demo.DAL.Models;
using Demo.pl.Helpers;
using Demo.pl.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Demo.pl.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IunitOfWork _unitOfWork;
        //private readonly IEmployeeRepo _EmployeesRepo;
        //private readonly IDepartmentRepo _departmentRepo;

        public EmployeeController(IMapper Mapper,IunitOfWork unitOfWork)
        {
            _mapper = Mapper;
            _unitOfWork = unitOfWork;
            //_EmployeesRepo = EmployeesRepo;
        }

        public IActionResult Index( string searchInp)
        {
            if (string.IsNullOrEmpty(searchInp))
            {
                var Employees = _unitOfWork.EmployeeRepository.GetAll();

                var MappedEmp = _mapper.Map<IEnumerable<Employees>, IEnumerable<EmployeeViewModel>>(Employees);

                return View(MappedEmp);
            }
            else
            {
                var Employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(searchInp.ToLower());

                var MappedEmp = _mapper.Map<IEnumerable<Employees>, IEnumerable<EmployeeViewModel>>(Employees);

                return View(MappedEmp);
            }

            
        }
        [HttpGet]// default
        public IActionResult Creat()
        {
            //ViewData["Department"]=_departmentRepo.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Creat(EmployeeViewModel EmployeesVm)
        {
            if (ModelState.IsValid)
            {
                //if (count > 0)
                //{
                //    TempData["Message"] = "Employee Created Successfully";

                //}else
                //{
                //    TempData["Message"] = "Employee NotCreated Successfully";
                //}

                EmployeesVm.ImaegeName = DocumentSettings.UploadFile(EmployeesVm.Image, "Images");

                var mappedEmp = _mapper.Map < EmployeeViewModel, Employees>(EmployeesVm);

                _unitOfWork.EmployeeRepository.Add(mappedEmp);
                _unitOfWork.Complet();

              
                return RedirectToAction(nameof(Index));
            }

            return View(EmployeesVm);
        }

        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();//400
            }

            var Employees = _unitOfWork.EmployeeRepository.Get(id.Value);
            var MappedEmp = _mapper.Map<Employees, EmployeeViewModel>(Employees);

            if (Employees == null)
            {
                return NotFound();
            }

            return View(viewName, MappedEmp);
        }

        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest();//400
            //}
            //var Employees = _EmployeesRepo.Get(id.Value);
            //if (Employees == null)
            //{
            //    return NotFound();
            //}
            //ViewData["Department"] = _departmentRepo.GetAll();
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel EmployeesVm)
        {

            if (id != EmployeesVm.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employees>(EmployeesVm);

                     _unitOfWork.EmployeeRepository.Update(mappedEmp);

                    _unitOfWork.Complet();
                        return RedirectToAction(nameof(Index));
                    
                }
                catch (Exception ex)
                {
                    //log exception //devolpers
                    //frindly message // users
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(EmployeesVm);
        }

        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete([FromRoute] int id, EmployeeViewModel EmployeesVm)
        {
            if (id != EmployeesVm.Id)
            {
                return BadRequest();
            }
            try
            {

                var mappedEmp = _mapper.Map<EmployeeViewModel, Employees>(EmployeesVm);

                DocumentSettings.DeleteFile(EmployeesVm.ImaegeName,"Images");

                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                _unitOfWork.Complet();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(EmployeesVm);
        }
    }
}
