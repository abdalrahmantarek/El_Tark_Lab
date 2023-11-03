using LAB.IService;
using LAB.Models;
using LAB.Service;
using LAB.ViewModel;
using Microsoft.AspNetCore.Mvc;

/*
 Summary
- make change in add Action by (Abdelfatah )
- Add New Action Edit by (Abdelfatah) 

 
 */




namespace LAB.Controllers
{
    public class VendorContactController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public VendorContactController(IUnitOfWork unitOfWork) 
        {
            _unitofwork = unitOfWork;
        }
        
        public IActionResult Index()
        {

            var model = _unitofwork.VendorContactService.GetAll();

            return View();
        }

        public IActionResult Allcontactbyid(int id)
        {
            var model = _unitofwork.VendorContactService.GetAllbyVendorid(id);

            return View();
        }

        // 
        [HttpPost]
        public IActionResult Add(VendorContactViewModel vcontact)
        {
			
			//ViewData["VendorList"] = _unitofwork.VendorService.GetAll(); 
			VendorContact NewContact = new VendorContact();
          
				NewContact.ContactId=vcontact.ContactId; 
                NewContact.ContactName=vcontact.ContactName; 
                NewContact.ContactEmail = vcontact.ContactEmail;
                NewContact.ContactTel=vcontact.ContactTel;
                NewContact.ContactTel2=vcontact.ContactTel2;


                NewContact.ContactVendorId = 1;
                NewContact.Occuption = 1;
                NewContact.LastUpdateDate= DateTime.Now;
                NewContact.LastUpdateFrom = "hr";
                NewContact.LastUpdateBy = "hr";
				_unitofwork.VendorContactService.Add(NewContact);
				_unitofwork.Save();
				return RedirectToAction(controllerName: "Vendor", actionName: "Index");
		}
		[HttpGet]
		public ActionResult Edit(int id)
		{
			ViewData["VendorList"] = _unitofwork.VendorService.GetAll();
			VendorContact contract = _unitofwork.VendorContactService.GetById(id);

			return View("Edit", contract);
		}

		[HttpPost]
		public ActionResult Edit(VendorContact contact)
		{
			contact.LastUpdateFrom = "hr";
			contact.LastUpdateDate = DateTime.Now;
			contact.LastUpdateBy = "hr";
			contact.Occuption = 1;
			_unitofwork.VendorContactService.Update(contact);
			_unitofwork.Save();
			return RedirectToAction(controllerName: "Vendor", actionName: "Index");
		}



		public IActionResult ContactPartial(int id)
        {
			var model = _unitofwork.VendorContactService.GetAllbyVendorid(id);

			return PartialView("_ContactPartial",model);
        }

    }
}
