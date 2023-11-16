using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.DAL.Enteties;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
	public class DepartmentController : Controller
	{
		//private readonly IDepartmentReopsitory departmentReopsitory;
        private readonly ILogger<DepartmentController> logger;
        private readonly IUniteOfWork uniteOfWork;
		private readonly IMapper mapper;

		public DepartmentController(
			//IDepartmentReopsitory departmentReopsitory ,
			ILogger<DepartmentController> logger,
			IUniteOfWork uniteOfWork,
			IMapper mapper
			)
		{
			//this.departmentReopsitory = departmentReopsitory;
            this.logger = logger;
            this.uniteOfWork = uniteOfWork;
			this.mapper = mapper;
		}
		public IActionResult Index()
		{
			var department = uniteOfWork.DepartmentReopsitory.GetAll();
			//         ViewData["Message"] = "Hello From View Data";
			//ViewBag.MessageBag = "Hello From View Bag";
			IEnumerable<DepartmentViewModel> departmentVM;

			departmentVM = mapper.Map<IEnumerable<DepartmentViewModel>>(department);
			TempData.Keep("Message");
            return View(departmentVM);
		}
		[HttpGet]
        public IActionResult Create()
        {
           
            return View(new DepartmentViewModel());
        }
		[HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
			if(ModelState.IsValid)
			{
				Department department = mapper.Map<Department>(departmentVM);
                uniteOfWork.DepartmentReopsitory.Add(department);
				TempData["Message"] = "Department Created Successfully!!";
                return RedirectToAction("Index");

			}
			else
			{
				return RedirectToAction("Index");
			}
        }

		public IActionResult Details(int? id)
		{
			try
			{
                if (id is null)
                    return NotFound();

                var department = uniteOfWork.DepartmentReopsitory.GetById(id);
                if (department == null)
                    return NotFound();
				DepartmentViewModel departmentVM = mapper.Map<DepartmentViewModel>(department);

                return View(departmentVM);
            }
			catch(Exception ex)
			{
				logger.LogError(ex.Message);
				return RedirectToAction("Error" , "Home");
			}
			
        }

		public IActionResult Update(int? id)
		{
			if (id is null)
				return NotFound();

			var department = uniteOfWork.DepartmentReopsitory.GetById(id);
			if (department == null)
				return NotFound();

			DepartmentViewModel departmentVM = mapper.Map<DepartmentViewModel>(department);

			return View(departmentVM);
		}
		[HttpPost]
		public IActionResult Update(int? id ,DepartmentViewModel departmentVM)
		{
			if (id != departmentVM.Id)
				return NotFound();

			try
			{
				if (ModelState.IsValid)
				{
					Department department = mapper.Map<Department>(departmentVM);
                    uniteOfWork.DepartmentReopsitory.Update(department);
					return RedirectToAction("Index");
				}
			}
			catch(Exception ex) 
			{
				throw new Exception(ex.Message);
			}
			return View(departmentVM);
		}

		public IActionResult Delete(int? id)
		{
			if(id is null)
				return NotFound();
			var department = uniteOfWork.DepartmentReopsitory.GetById(id);

			if( department == null)
				return NotFound();
            uniteOfWork.DepartmentReopsitory.Delete(department);

			return RedirectToAction("Index");
		}
	}
}
