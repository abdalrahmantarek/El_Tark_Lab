using AutoMapper;
using LAB.IService;
using LAB.Models;
using LAB.Service;
using LAB.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LAB.Controllers
{
    public class PatientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var Model = _unitOfWork.PatientService.GetAll();
            foreach (var item in Model)
            {
                if (item.PatientGender.ToLower() == "m")
                {
                    item.PatientGender = "Male";
                }
                if (item.PatientGender.ToLower() == "f")
                {
                    item.PatientGender = "Female";
                }


			}
            return View(Model);
        }
        public IActionResult Details(int id)
        
        {
            var Details = _unitOfWork.PatientService.GetById(id);
			
			
				if (Details.PatientGender.ToLower() == "m")
				{
					Details.PatientGender = "Male";
				}
				else if (Details.PatientGender.ToLower() == "f")
				{
					Details.PatientGender = "Female";
				} 
				ViewData["VisitList"]= _unitOfWork.VisitServices.GetAll().Where(i => i.PatientId == id).ToList();
				List<Visit> visitList = (List<Visit>)ViewData["VisitList"];
				if (visitList != null)
				{
					foreach (var item in visitList)
					{
						if (item.VisitStatus.ToLower() == "p")
						{ item.VisitStatus = "pending";}
						else if (item.VisitStatus.ToLower() == "a")
						{ item.VisitStatus = "Active"; }
						else if (item.VisitStatus.ToLower() == "c")
						{ item.VisitStatus = "Cancel"; }
					}
				}
			return View(Details);
        }

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var model = _unitOfWork.PatientService.GetById(id);
			return View(model);
		}
		[HttpPost]
		public IActionResult Edit(Patient patient)
		{

			if (ModelState.IsValid)
			{
				patient.LastUpdateDate = DateTime.Now;
				
				_unitOfWork.PatientService.Update(patient);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}

				return View(patient);
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(PatientViewModel vpatient, int years=0, int months=0, int days = 0)
		{
            DateTime currentDate = DateTime.Now;
            Patient newPatient = new Patient();
            newPatient.CreationDate = DateTime.Now;
            if (vpatient!=null)
            {
                newPatient.PatientName = vpatient.PatientName;
                newPatient.PatientAddress = vpatient.PatientAddress;
                newPatient.PatientEmail = vpatient.PatientEmail;
                newPatient.PatientGender = vpatient.PatientGender;
                newPatient.PatientNid = vpatient.PatientNid;
                newPatient.PatientCountry = vpatient.PatientCountry;
                newPatient.PatientNidType = vpatient.PatientNidType;
                newPatient.PatientTel1 = vpatient.PatientTel1;
                newPatient.PatientTel2 = vpatient.PatientTel2;
               
                if (vpatient.PatientBirthdate == null)
                {
                    vpatient.Age = currentDate
                        .AddYears(-years)
                        .AddMonths(-months).
                        AddDays(-days);
                    newPatient.PatientBirthdate = vpatient.Age;

                }
                else { 
                          newPatient.PatientBirthdate = vpatient.PatientBirthdate;
                     }


                _unitOfWork.PatientService.Add(newPatient);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(vpatient);

        }

	




		[HttpGet]
		public IActionResult AddPatientInVisitView(int id) 
		{
			VisitViewModel model = new VisitViewModel();
			Patient patient;
			if (id == 0) 
			{
				patient = new Patient();
               
			}else
				patient= _unitOfWork.PatientService.GetById(id);

			patient.LastUpdateBy = "abdo";
			patient.LastUpdateDate = DateTime.Now;
            patient.LastUpdateFrom = "::1";
			model.Patient = patient;

			return View("_PatientInVisitPartialView",model);
		}

        
		public IActionResult GetPatientVisits(int id)
        {
             var model = _unitOfWork.VisitServices.GetAll().Where(i=>i.PatientId==id).ToList();
			foreach (var item in model)
			{
				if (item.VisitStatus.ToLower() == "p")
				{ item.VisitStatus = "pending"; }
				else if (item.VisitStatus.ToLower() == "a")
				{ item.VisitStatus = "Active"; }
				else if (item.VisitStatus.ToLower() == "c")
				{
					item.VisitStatus = "Cancel";


				}

			}
			return View("PatientVisits", model);
        }








	}
}
