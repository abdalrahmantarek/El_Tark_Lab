using LAB.IService;
using Microsoft.AspNetCore.Mvc;
using LAB.Service;
using LAB.Models;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using LAB.ViewModel;

namespace LAB.Controllers
{
	public class ServicesPricelistController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ServicesPricelistController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			var model = _unitOfWork.servicesPricelistService.GetAll();

			return View(model);
		}
		[HttpGet]
		public IActionResult Add(int id)
		{
			ServicesPricelistViewModel model = new();

			model.ContractId = id;
			model.LastUpdateDate = DateTime.Now;
			model.LastUpdateFrom = "hr";
			model.LastUpdateBy = "hr";
			model.standardServices = _unitOfWork.StandardServices.GetAll().ToList();
			model.categories  = _unitOfWork._Context.ServicesCategories.ToList();
			return View("_AddServicePartialView",model);
		}
		[HttpPost]
		public IActionResult Add(int id, ServicesPricelistViewModel ServicesPricelist)
		{
			ServicesPricelist newPriceList = new ServicesPricelist();
			if (ModelState.IsValid)
			{
				newPriceList.ServiceName = ServicesPricelist.ServiceName;
				newPriceList.ServiceNameAr = ServicesPricelist.ServiceNameAr;
				newPriceList.ServicePrice = ServicesPricelist.ServicePrice;
				newPriceList.ServiceCat = ServicesPricelist.ServiceCat;
				newPriceList.ContractId = ServicesPricelist.ContractId;
				newPriceList.StandRef = ServicesPricelist.StandRef;
				newPriceList.LastUpdateBy = ServicesPricelist.LastUpdateBy;
				newPriceList.LastUpdateFrom = ServicesPricelist.LastUpdateFrom;
				newPriceList.LastUpdateDate = ServicesPricelist.LastUpdateDate;
				_unitOfWork.servicesPricelistService.Add(newPriceList);
				_unitOfWork.Save();
				return RedirectToAction("index","Vendor");
			}
			return View(newPriceList);
		}

		public IActionResult Details(int id)
		{
			var Details = _unitOfWork.servicesPricelistService.GetById(id);
			return View(Details);
		}

		public IActionResult ImportSPLfile()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Upload(IFormFile file)
		{
			if (file != null && file.Length > 0)
			{

				using (var stream = file.OpenReadStream())
				{
					using (var package = new ExcelPackage(stream))
					{
						ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
						var worksheet = package.Workbook.Worksheets[0];
						var rowCount = worksheet.Dimension.Rows;


						List<ServicesPricelist> serviceserror = new List<ServicesPricelist>();


						for (int row = 2; row <= rowCount; row++) // Changed loop condition
						{
							var regexname = "[A-Z]";


							var ServiceNamee = worksheet.Cells[row, 1].Value?.ToString(); // Use ?. to handle null values
							var ServiceNameArr = worksheet.Cells[row, 2].Value?.ToString();
							var ServicePricevalue = worksheet.Cells[row, 3].Value?.ToString();
							var ServiceCata = worksheet.Cells[row, 4].Value?.ToString();
							var ContractID = worksheet.Cells[row, 5].Value?.ToString();
							var StanderdRef = worksheet.Cells[row, 6].Value?.ToString();

							if (string.IsNullOrWhiteSpace(ServiceNamee) || Regex.IsMatch(ServiceNamee, regexname) ||
								string.IsNullOrWhiteSpace(ServiceNameArr) || Regex.IsMatch(ServiceNameArr, regexname) ||
								string.IsNullOrWhiteSpace(ServicePricevalue) ||
								string.IsNullOrWhiteSpace(ServiceCata) ||
								string.IsNullOrWhiteSpace(ContractID) || string.IsNullOrWhiteSpace(StanderdRef)
								)
							{
								// Handle missing or invalid data as needed
								serviceserror.Add(new ServicesPricelist()); // Skip this row and proceed to the next
							}
							if (serviceserror.Count > 0)
							{
								return View("ErrorInSPLFile", serviceserror);
							}

							// Now, parse the values
							if (
								!double.TryParse(ServicePricevalue, out double ServicePrice) ||
								!int.TryParse(ServiceCata, out int ServiceCat) ||
								!int.TryParse(ContractID, out int ContractId) ||
								!int.TryParse(StanderdRef, out int StanderdREF)

								)
							{
								// Handle parsing errors as needed
								continue; // Skip this row and proceed to the next
							}
							else
							{
								_unitOfWork.servicesPricelistService.Add(new ServicesPricelist
								{
									ServiceName = ServiceNamee,
									ServiceNameAr = ServiceNameArr,
									ServicePrice = ServicePrice,
									ServiceCat = ServiceCat,
									ContractId = ContractId,
									StandRef = StanderdREF,
									LastUpdateBy = "abdo tarek",
									 LastUpdateDate = DateTime.Now,
									 LastUpdateFrom = ":::1",
									 
								});
							}
						}

						_unitOfWork.Save();
					}

				}
			}
			return RedirectToAction("Index",controllerName:"Vendor");
		}


		[HttpGet]
		public IActionResult ShowService(int id) 
		{
			var model = _unitOfWork.servicesPricelistService.GetAllByContractId(id);
			ViewBag.contractID = id;
			return PartialView("_ShowAllServiceInContructPartialView", model);
		}

		[HttpGet]
		public IActionResult ShowServiceInVisit(int vendorId)
		{
			var contract = _unitOfWork.ContractService.GetLastValidContractByVendorId(vendorId);
			if (contract == null )
			{
				contract = new VendorContract();
			}
			var model = new SPLSelectListViewModel();
			var serviceIncontract = _unitOfWork.servicesPricelistService.GetAllByContractId(contract.ContractId).ToList();
			if (serviceIncontract != null)
				model.ServicesPricelists = serviceIncontract;
			else
				model.ServicesPricelists=new List<ServicesPricelist>();
			model.L2LVendor = _unitOfWork.VendorService.GetLab2LabVendor();
			return PartialView("_ShowSPLInVisit", model);
		}

	}
}
