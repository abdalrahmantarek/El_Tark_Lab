using LAB.IService;
using LAB.Models;
using LAB.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB.Controllers
{
	public class ContractController : Controller
	{
		IUnitOfWork unitOfWork;
		public ContractController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}


		public ActionResult Add_Contract(int vendor_id)
		{
			ViewBag.vendor_id = vendor_id;
			var model = unitOfWork._Context.VendorContractTypes.ToList();
			return View(model);
		}


		[HttpPost]
		public ActionResult Add(VendorContract contract)
		{
			var listContract=unitOfWork._Context.VendorContracts.Where(x => x.VendorId == contract.VendorId).ToList();
			foreach (var item in listContract)
			{
				if (item.EndDate > contract.StartDate)
				{
					item.EndDate = contract.StartDate.AddDays(-1);
				}
			}
			unitOfWork.ContractService.Add(contract);
			unitOfWork.Save();
			return RedirectToAction(controllerName:"Vendor" , actionName:"Index");
		}



		[HttpGet]
		public ActionResult Edit(int id)
		{
			VendorContract contract = unitOfWork.ContractService.GetById(id);
			contract.LastUpdateBy = "eman";
			contract.LastUpdateFrom = "1";
			contract.LastUpdateDate = DateTime.Now;
			return View(contract);
		}


		[HttpPost]
		public ActionResult Edit(VendorContract contract)
		{
			unitOfWork.ContractService.Update(contract);
			unitOfWork.Save();
			return RedirectToAction(controllerName: "Vendor", actionName: "Index");
		}





		public IActionResult Index()
		{
			return View();
		}
	}
}
