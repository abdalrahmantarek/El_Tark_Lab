using LAB.IService;
using LAB.Models;
using LAB.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB.Controllers
{
	public class PatientInvoiceController : Controller
	{
		private readonly IUnitOfWork _unitofwork;

		public PatientInvoiceController(IUnitOfWork unitOfWork) 
		{
			_unitofwork=unitOfWork;
		}

		public IActionResult Index()
		{

			var date =DateOnly.FromDateTime(DateTime.Now);
			var modal = _unitofwork.PatientInvoiceServices.GetAll();
			//var modal = _unitofwork.PatientInvoiceServices.GetAll().Where(d=>DateOnly.FromDateTime( d.CreationDate) == date );
			foreach (var item in modal)
			{
				if (item.InvoiceStatus.ToLower() == "p")
				{ item.InvoiceStatus = "pending"; }
				else if (item.InvoiceStatus.ToLower() == "a")
				{ item.InvoiceStatus = "Active"; }
				else if (item.InvoiceStatus.ToLower() == "c")
				{ item.InvoiceStatus = "Cancel";
				    
				
				}

			}

			return View(modal);
		}


		public IActionResult GetInvoice() 
		{
			var model = _unitofwork.PatientInvoiceServices.GetAll();

			foreach (var item in model)
			{
				if (item.InvoiceStatus.ToLower() == "p")
				{ item.InvoiceStatus = "pending"; }
				else if (item.InvoiceStatus.ToLower() == "a")
				{ item.InvoiceStatus = "Active"; }
				else if (item.InvoiceStatus.ToLower() == "c")
				{ item.InvoiceStatus = "Cancel"; }

			}

			return View("Index", model);
		}

		[HttpGet]
		public IActionResult PatientInvoicePay(int id )
		{

			var modal = _unitofwork.PatientInvoiceServices.GetByVisitId(id);
			
			modal.LastUpdateDate= DateTime.Now;
			modal.LastUpdateFrom = "hr";
			modal.LastUpdateBy = "hr";
			int vendorId;


             ViewData["servicelist"] = _unitofwork.VisitServiceServices.GetServicesByVisitId(id);
			if (modal.Visit.VisitInsurred)
			{
				 vendorId = (int)modal.Visit.VisitInsurredCompanyId;
				ViewData["VendorName"] = _unitofwork.VendorService.GetById(vendorId).VendorName;

			}
			else
			{
				ViewData["VendorName"] = "LaB";
			}
			

                  //no max
			if (modal.Visit.VisitInsurred == true)
			{

				
				var visitcopayment = modal.Visit.Copayment;

				

				if (modal.Visit.ApprovalMaxLimit == 0)
				{
					var patientcost_Max0 =modal.InvoiceCost * (visitcopayment / 100);

					ViewData["PatientCost"] = patientcost_Max0;
					ViewData["VendorCost"] = modal.InvoiceCost - patientcost_Max0;



				}
				else
				{
					//if (modal.Visit.PatientApprovalNum !=null || modal.Visit.PatientApprovalNum =="")
					//{
                        if(modal.Visit.ApprovalMaxLimit > modal.InvoiceCost)
					     {


						  var	PatientCost_maxLimit =modal.InvoiceCost * (visitcopayment / 100);
							ViewData["PatientCost"] = PatientCost_maxLimit;
							ViewData["VendorCost"] = modal.InvoiceCost - PatientCost_maxLimit;

						}
					     else
					      {
							if (modal.Visit.PatientApprovalNum != null || modal.Visit.PatientApprovalNum == "")
							{
								var PatientCost_maxLimit = modal.InvoiceCost * (visitcopayment / 100);
								ViewData["PatientCost"] = PatientCost_maxLimit;
								ViewData["VendorCost"] = modal.InvoiceCost - PatientCost_maxLimit;
							}
							else
							{
								var ValueLeft = modal.InvoiceCost - modal.Visit.ApprovalMaxLimit;

								var CopaymentValue = modal.Visit.ApprovalMaxLimit * (visitcopayment / 100);

								var PatientCost_MinLimit = CopaymentValue + ValueLeft;

								ViewData["PatientCost"] = PatientCost_MinLimit;


								ViewData["VendorCost"] = modal.InvoiceCost - PatientCost_MinLimit;
							}
							


						}
					//}
					//else
					//{
					//	var PatientCostNotAprronum = modal.InvoiceCost * (visitcopayment / 100);

					//	ViewData["PatientCost"] =  PatientCostNotAprronum;
					//	ViewData["VendorCost"] = modal.InvoiceCost - PatientCostNotAprronum;
					//}
					
				}



				return View(modal);
			}

			else
			{
				if(modal.Visit.DiscountCard != null && modal.Visit.DiscountCard != 0)
				{
					

					var discount = modal.Visit.DiscountCard;

					var PatientCostDiscount = modal.InvoiceCost * (discount / 100);

					ViewData["PatientCost"] = PatientCostDiscount;
					

				}

				else
				{
					ViewData["PatientCost"] = modal.InvoiceCost - (modal.PaidValue??0);

				}
			}

			return View("DiscountView",modal);
		}
		[HttpPost]
		public IActionResult Pay( double paidValue , int InvoiceId , double PatientCost, double InvoiceCost)
		{
			InvoicesPaying p = new InvoicesPaying()
			{
				InvoiceId = InvoiceId,
				PayingValue= paidValue,
				LastUpdateFrom=":::1",
				LastUpdateBy="abdo",
				PayingDate= DateTime.Now,
			};
			var invoice = _unitofwork._Context.PatientInvoices.Include(x => x.Visit).FirstOrDefault(x => x.InvoiceId == InvoiceId);
			

			if (invoice.PaidValue != null) 
			invoice.PaidValue += paidValue;
			else
			invoice.PaidValue = paidValue;

			var visit = _unitofwork._Context.Visits.FirstOrDefault(x => x.VisitId == invoice.VisitId);

			visit.VisitStatus = "A";
			invoice.InvoiceStatus = "A";

			_unitofwork._Context.Update(visit);
			_unitofwork._Context.Update(invoice);
			_unitofwork._Context.InvoicesPayings.Add(p);
			_unitofwork.Save();
			double? totalPaiedValue = invoice.PaidValue;

			var result = new { status = "ok", remainingvalue = PatientCost - totalPaiedValue, paidValue = paidValue };
			return Json(result);
		}


		[HttpPost]
		public IActionResult Filter(DateOnly Date)
		{
			//, DateOnly EndDate

			//var model = _unitofwork.PatientInvoiceServices.GetAll().Where(i => DateOnly.FromDateTime(i.CreationDate) >= StartDate && DateOnly.FromDateTime(i.CreationDate) <= EndDate).ToList();
			
			var model = _unitofwork.PatientInvoiceServices.GetAll().Where(i => DateOnly.FromDateTime(i.CreationDate) >= Date).ToList();


			foreach (var item in model)
			{
				if (item.InvoiceStatus.ToLower() == "p")
				{ item.InvoiceStatus = "pending"; }
				else if (item.InvoiceStatus.ToLower() == "a")
				{ item.InvoiceStatus = "Active"; }
				else if (item.InvoiceStatus.ToLower() == "c")
				{ item.InvoiceStatus = "Cancel"; }

			}


			return View(model);
		}


		[HttpGet]
		public IActionResult Cancle(int id)
		{

			var modal = _unitofwork.PatientInvoiceServices.GetByVisitId(id);

			modal.LastUpdateDate = DateTime.Now;
			modal.LastUpdateFrom = "hr";
			modal.LastUpdateBy = "hr";
			int vendorId;

			if (modal.Visit.VisitInsurred)
			{
				vendorId = (int)modal.Visit.VisitInsurredCompanyId;
				ViewData["VendorName"] = _unitofwork.VendorService.GetById(vendorId).VendorName;

			}
			else
			{
				ViewData["VendorName"] = "LaB";
			}
            ViewData["servicelist"] = _unitofwork.VisitServiceServices.GetServicesByVisitId(id);

            return View(modal);
		}

		//filter by date
	   [HttpPost]
		public IActionResult FilterByDate(DateOnly Date)
		{

			var model = _unitofwork.PatientInvoiceServices.GetAll().Where(i => DateOnly.FromDateTime(i.CreationDate) == Date).ToList();


			return View("Filter", model);
		}

	}
}
