using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Enteties;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUniteOfWork uniteOfWork;
		private readonly IMapper mapper;

		public EmployeeController(IUniteOfWork uniteOfWork , IMapper mapper) {
            this.uniteOfWork = uniteOfWork;
			this.mapper = mapper;
		}
        public IActionResult Index(string searchValue = "" , int deptId = 0 )
        {
            IEnumerable<Employee> employees;
            IEnumerable<EmployeeViewModel> mappedEmloyees;

           

            if (searchValue == null && deptId <= 0 )
            {
                 employees = uniteOfWork.EmployeeRepository.GetAll();
                mappedEmloyees = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
            }
            else if (searchValue != null && deptId <= 0)
			{
                 employees = uniteOfWork.EmployeeRepository.Search(searchValue);
				mappedEmloyees = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

			}else 
            {
				employees = uniteOfWork.EmployeeRepository.GetEmployeeByDepartment(deptId);
				mappedEmloyees = mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
			}
			return View(mappedEmloyees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments  = uniteOfWork.DepartmentReopsitory.GetAll();
            return View(new EmployeeViewModel());
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            //employee.Department = uniteOfWork.DepartmentReopsitory.GetById(employee.DepartmentId);
            //ModelState["Department"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                try
                {
                    //Employee employee = new Employee 
                    //{
                    //    Name = employeeVM.Name,
                    //    Address = employeeVM.Address,
                    //    DepartmentId = employeeVM.DepartmentId,
                    //    Salary = employeeVM.Salary,
                    //    Email = employeeVM.Email,
                    //    HireDate = employeeVM.HireDate,
                    //    IsActive = employeeVM.IsActive,
                    //};
                    var employee = mapper.Map<Employee>(employeeVM);
                    employee.ImageUrl = DocumentSettings.UploadFile(employeeVM.Image, "Images");

				    uniteOfWork.EmployeeRepository.Add(employee);
				    return RedirectToAction(nameof(Index));
			    }catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }


            ViewBag.Departments = uniteOfWork.DepartmentReopsitory.GetAll();

			return View(employeeVM);
        }

        public IActionResult Details(int id)
        {
            EmployeeViewModel mappedEmployee;
            try
            {
                if (id == null)
                    return NotFound();

                var employee = uniteOfWork.EmployeeRepository.GetById(id);
                if (employee == null)
                    return NotFound();
                else
					 mappedEmployee = mapper.Map<EmployeeViewModel>(employee);


				return View(mappedEmployee);
            }catch (Exception ex)
            {
				return RedirectToAction("Error", "Home");
			}
        }

        public IActionResult Update(int id)
        {
			EmployeeViewModel mappedEmployee;
			try
			{
				if (id == null)
					return NotFound();

                ViewBag.Departments = uniteOfWork.DepartmentReopsitory.GetAll();
				var employee = uniteOfWork.EmployeeRepository.GetById(id);
				if (employee == null)
					return NotFound();
				else
					mappedEmployee = mapper.Map<EmployeeViewModel>(employee);


				return View(mappedEmployee);
			}
			catch (Exception ex)
			{
				return RedirectToAction("Error", "Home");
			}
		}
        [HttpPost]
        public IActionResult Update(int? id , EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return NotFound();
            try
            {
                if (ModelState.IsValid)
                {
					employeeVM.ImageUrl = DocumentSettings.UploadFile(employeeVM.Image, "Images");
					var employee = mapper.Map<Employee>(employeeVM);
                    uniteOfWork.EmployeeRepository.Update(employee);
                    return RedirectToAction("Index");
                }
            }catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(employeeVM);
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
                return NotFound();

            var employee = uniteOfWork.EmployeeRepository.GetById(id);
            if (employee == null) 
                return NotFound();
			
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files" , "Images");

			var ImagePath = Path.Combine(folderPath, employee.ImageUrl);


            System.IO.File.Delete(ImagePath);
            uniteOfWork.EmployeeRepository.Delete(employee);

			return RedirectToAction("Index");

		}
	}
}
