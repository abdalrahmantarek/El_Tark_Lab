using LAB.IService;
using LAB.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB.Controllers
{
	public class RefferedDoctorController : Controller
	{
		private readonly IUnitOfWork _unitofwork;

		public RefferedDoctorController(IUnitOfWork unitOfWork) 
		{
			_unitofwork = unitOfWork;
		}
		public IActionResult Index()
		{
			var model = _unitofwork.RefferedDoctorServices.GetAll(); 

			return View(model);
		}

		
		[HttpPost]
		public IActionResult Add(RefferedDoctor doctor)
		{


			doctor.LastUpdateBy = "hr";
			doctor.LastUpdateFrom = "hr";


             _unitofwork.RefferedDoctorServices.Add(doctor);
				_unitofwork.Save();

				return RedirectToAction("Index");
			
		}
		[HttpGet]
		public IActionResult AddInVisitView(int id)
		{
			RefferedDoctor model;
			if (id > 0) { 
			 model = _unitofwork.RefferedDoctorServices.GetById(id);
			}
			else 
			{
				model= new RefferedDoctor();
			}
			model.LastUpdateBy = "hr";
			model.LastUpdateFrom = "hr";
			model.LastUpdateDate = DateTime.Now;

			return View(model);

		}

		//[HttpPost]
		//public IActionResult AddInVisitView(RefferedDoctor doctor)
		//{
		//	doctor.LastUpdateBy = "hr";
		//	doctor.LastUpdateFrom = "hr";
		//	_unitofwork.RefferedDoctorServices.Add(doctor);
		//	_unitofwork.Save();

		//	return RedirectToAction("Add","Visit");

		//}


		[HttpGet]
		public IActionResult Edit(int id)
		{
			var model =_unitofwork.RefferedDoctorServices.GetById(id);
			return View(model);

		}


		[HttpPost]
		public IActionResult Edit(RefferedDoctor doctor)

		{      doctor.LastUpdateBy = "hr";
				doctor.LastUpdateFrom = "hr";

			if (ModelState.IsValid)
			{
				

				_unitofwork.RefferedDoctorServices.Update(doctor);
				_unitofwork.Save();

				return RedirectToAction("Index");

			}
			return View(doctor);
			
		}






	}
}
