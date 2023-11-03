using LAB.IService;
using LAB.Models;
using LAB.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB.Controllers
{
    public class ReportController : Controller
    {
        IUnitOfWork unitOfWork;

        public ReportController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }


        // report genrated based on barcode id 
        [HttpGet]
        public IActionResult Generate(int id = 4032 )
        {
            ReportViewModel model = new ();
            var iswritten = unitOfWork._Context.RoportResults.Where(x=>x.BarcodeId == id).Count() > 0;
            List<RoportResult> reports;
                ViewBag.iswritten = iswritten;

            //check if report alredy exisit 
            if (iswritten)
            {
                reports = unitOfWork._Context.RoportResults.Where(x=>x.BarcodeId == id).ToList(); 
                model.RoportResults = reports;
            }

            var servicebarcode = unitOfWork._Context.ServiceBarcodes
                .Include(x => x.SampleLogs)
                .ThenInclude(x => x.Status)
                .Include(x => x.Services)
                .ThenInclude(x => x.StandRefNavigation)
                .Include(x => x.Visit)
                .ThenInclude(x => x.Patient)
                .FirstOrDefault(x => x.BarcodeId == id);  // write service barcode


            var age = DateOnly.FromDateTime(DateTime.Now).Year - DateOnly.FromDateTime(servicebarcode.Visit.Patient.PatientBirthdate.Value).Year;
            var standref = servicebarcode.Services.StandRef;
            var gender = servicebarcode.Visit.Patient.PatientGender;
           

            //case packege 
            if (servicebarcode.Services.StandRefNavigation.IsPackage)
            {
                servicebarcode = unitOfWork._Context.ServiceBarcodes
                .Include(x => x.SampleLogs)
                .ThenInclude(x => x.Status)
                .Include(x => x.Services)
                .ThenInclude(x => x.Services) // pakage services 
                .Include(x => x.Visit)
                .ThenInclude(x => x.Patient)
                .FirstOrDefault(x => x.BarcodeId == id); // write service barcode


                //add normal range foreach service
                foreach (var item in servicebarcode.Services.Services)
                {
                    var NR = GetNormalRange(age, item.ServiceId, gender);
                    model.RoportResults.Add(new RoportResult()
                    {
                        BarcodeId = servicebarcode.BarcodeId,
                        NormalRangeId = NR.Id,
                        LastUpdateBy = "abdo",
                        LastUpdateDate = DateTime.Now,
                        LastUpdateFrom = ":::1",

                    });

                    model.NormalRanges.Add(NR);

                }

                model.ServiceBarcode = servicebarcode;

                return View("pakageServicesResult", model);

            }
            //case service only
            else 
            {
                var NR = GetNormalRange(age, servicebarcode.Services.StandRefNavigation.ServiceId, gender);
                model.ServiceBarcode = servicebarcode;
                model.RoportResults.Add(new RoportResult()
                {
                    BarcodeId = servicebarcode.BarcodeId,
                    NormalRangeId = NR.Id,
                    LastUpdateBy = "abdo",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateFrom = ":::1",

                });

                model.NormalRanges.Add(NR);
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Generate(List<RoportResult> RoportResults) 
        {

            unitOfWork._Context.AddRange(RoportResults);
            unitOfWork.Save();

            return RedirectToAction("Details", new {id = RoportResults.FirstOrDefault()?.BarcodeId });
        }

        //get report Details By Barcode ID
        [HttpGet]
        public IActionResult Details(int id) 
        {
            var model  = unitOfWork._Context.RoportResults
                .Include(x=>x.NormalRange)
                .ThenInclude(x=>x.StandrefNavigation)
                .Include(x=>x.Barcode)
                .ThenInclude(x=>x.Visit)
                .ThenInclude(x=>x.Patient)
                .Where(x=>x.BarcodeId == id)
                .ToList();
            return View(model);
        }

        public NormalRange GetNormalRange(int age, int? standrefID, string? Gender = "b")
        {

            var ranges = unitOfWork._Context.NormalRanges.Where(x => x.Standref == standrefID);

            foreach (var item in ranges)
            {
                int startAge = int.Parse(item.AgeRange.Split('-')[0]);
                int endAge = int.Parse(item.AgeRange.Split('-')[1]);

                if (startAge <= age && endAge > age && (item.Gender.ToLower().Trim() == "b" || item.Gender.ToLower().Trim() == Gender.ToLower().Trim()))
                {

                    return item;
                }
            }
            return new NormalRange();
        }
    }
}
