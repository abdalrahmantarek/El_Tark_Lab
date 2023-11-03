using AutoMapper;
using LAB.IService;
using LAB.Models;
using LAB.Service;
using LAB.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace LAB.Controllers
{
    public class VendorController : Controller
    {
        readonly IMapper mapper;
        IUnitOfWork unitOfWork;
        public VendorController(IUnitOfWork _unitOfWork, IMapper mapper)
        {
            unitOfWork = _unitOfWork;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var model = unitOfWork.VendorService.GetAll();

			return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {          
            
            VendorViewModel v = new VendorViewModel();
            v.VendorCategories = unitOfWork.VendorCat.GetAll().ToList();

            return View(v);
        }

        [HttpPost]
        public IActionResult Add(VendorViewModel v)
        {

            var vendor = mapper.Map<Vendor>(v);

            if (ModelState.IsValid)
            {
                vendor.CreationDate = DateTime.Now;

                unitOfWork.VendorService.Add(vendor);
                
                return RedirectToAction("Index");
            }
            else
            {
				v.VendorCategories = unitOfWork.VendorCat.GetAll().ToList();

				return View(v);
            }

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
           var vendor =  unitOfWork.VendorService.GetById(id);
           var model = mapper.Map<VendorViewModel>(vendor);
		    model.VendorCategories = unitOfWork.VendorCat.GetAll().ToList();
			return View(model);

        }
        [HttpPost]
        public IActionResult Edit(VendorViewModel v)
        {
            var vendor = mapper.Map<Vendor>(v);

            if (ModelState.IsValid) 
            {
                vendor.LastUpdateDate = DateTime.Now;

                unitOfWork.VendorService.Update(vendor);
                return RedirectToAction("Index");
			}
			else
			{
				v.VendorCategories = unitOfWork.VendorCat.GetAll().ToList();

				return View(v);
			}
		}

        public IActionResult Details (int id) 
        {
            var model= unitOfWork.VendorService.GetById(id);

            if (model.VendorContracts.Count !=0)
            {
                
                var SPL = unitOfWork.servicesPricelistService.GetAllByContractId(model.VendorContracts.OrderBy(x=>x.EndDate).LastOrDefault().ContractId);
                var contractID = model.VendorContracts.OrderBy(x => x.EndDate).LastOrDefault().ContractId;

                ViewData["contractID"] = contractID;
				ViewData["SPL"] = SPL;
                ViewBag.SBL = SPL.Count();
            }
            

            return PartialView("_VendorDetailsPartial",model);

        }


	}
}
