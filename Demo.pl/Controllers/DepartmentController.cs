using Demo.BLL.Interfaces;
using Demo.BLL.Repo;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Demo.pl.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IunitOfWork _untofwork;

        //public IDepartmentRepo DepartmentRepo { get; }
        //private readonly IDepartmentRepo _departmentRepo;

        //private IDepartmentRepo _DepartmentRepo;

        public DepartmentController(IunitOfWork untofwork)
        {
            _untofwork = untofwork;
            //DepartmentRepo = departmentRepo;
            //_departmentRepo = departmentRepo;
            //_DepartmentRepo = departmentRepo;
        }

        public IActionResult Index()
        {
            var department= _untofwork.DepartmentRepository.GetAll();
            return View(department);
        }
        [HttpGet]// default
        public IActionResult Creat()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Creat(Department department)
        {
            if (ModelState.IsValid) 
            {
                 _untofwork.DepartmentRepository.Add(department);
                 _untofwork.Complet();
               
                    return RedirectToAction(nameof(Index));
                
            }

            return View(department);
        }

        public IActionResult Details(int? id, string viewName= "Details")
        {
            if(!id.HasValue)
            {
                return BadRequest();//400
            }

            var department= _untofwork.DepartmentRepository.Get(id.Value);
           
            if(department==null)
            {
                return NotFound();
            }

            return View(viewName,department);
        }

        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest();//400
            //}
            //var department = _departmentRepo.Get(id.Value);
            //if (department == null)
            //{
            //    return NotFound();
            //}
            return Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,Department department)
        {

            if(id != department.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _untofwork.DepartmentRepository.Update(department);

                    _untofwork.Complet();
                        return RedirectToAction(nameof(Index));
                    
                }
                catch (Exception ex)
                {
                    //log exception //devolpers
                    //frindly message // users
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
                
            return View(department);
        }

        public IActionResult Delete(int id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete([FromRoute]int id,Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            try
            {
                _untofwork.DepartmentRepository.Delete(department);
                _untofwork.Complet();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message); 
            }
            return View(department);
        }
    }
}
