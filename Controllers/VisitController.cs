//using IronBarCode;
//using iTextSharp.text.pdf;
//using LAB.IService;
//using LAB.Models;
//using LAB.ViewModel;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Reflection.Metadata;
//using ZXing.OneD;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using Org.BouncyCastle.Asn1.Pkcs;
//using LAB.Service;
//using iText.Layout.Element;
using IronBarCode;
using LAB.IService;
using LAB.Models;
using LAB.Service;
using LAB.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LAB.Controllers
{
    public class VisitController : Controller
    {

        IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment hostingEnvironment;

        public VisitController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {

			var date = DateTime.Now;
			//var model = unitOfWork.VisitServices.GetAll().Where(d => DateOnly.FromDateTime(d.VisitStartDate.Value) == DateOnly.FromDateTime(date)); 
			var model = unitOfWork.VisitServices.GetAll();
			foreach (var item in model)
			{
				if (item.VisitStatus.ToLower() == "p")
				{ item.VisitStatus = "pending"; }
				else if (item.VisitStatus.ToLower() == "a")
				{ item.VisitStatus = "Active"; }
				else if (item.VisitStatus.ToLower() == "c")
				{ item.VisitStatus = "Cancel"; }

             
			}

			return View(model);
        }


		[HttpPost]
		public IActionResult Filter(DateOnly? Date)
		{
			//, DateOnly EndDate

			//var model = _unitofwork.PatientInvoiceServices.GetAll().Where(i => DateOnly.FromDateTime(i.CreationDate) >= StartDate && DateOnly.FromDateTime(i.CreationDate) <= EndDate).ToList();


			
			var model = unitOfWork.VisitServices.GetAll().Where(d => DateOnly.FromDateTime( d.VisitStartDate.Value) == Date) .ToList();


			foreach (var item in model)
			{
				if (item.VisitStatus.ToLower() == "p")
				{ item.VisitStatus = "pending"; }
				else if (item.VisitStatus.ToLower() == "a")
				{ item.VisitStatus = "Active"; }
				else if (item.VisitStatus.ToLower() == "c")
				{ item.VisitStatus = "Cancel"; }


			}


			return View("Index", model);
		}

		[HttpGet]
        public IActionResult Add()
        {
            VisitViewModel model = new VisitViewModel();

            model.Visit = new Visit()
            {
                VisitType = "LB",
                LastUpdateBy = "abdo",
                LastUpdateDate = DateTime.Now,
                LastUpdateFrom = "::1",
                VisitStartDate = DateTime.Now,
                VisitStatus = "P"
            };
            model.RefDoctor = unitOfWork._Context.RefferedDoctors.ToList();
            model.Patients = unitOfWork.PatientService.GetAll().ToList();
            model.ServicesPricelists = unitOfWork.servicesPricelistService.GetAll().Where(x=>x.ContractId == 8 ).ToList();
            model.Vendors = unitOfWork.VendorService.GetAll().Where(x => x.VendorCat == 6).ToList();
            model.L2LVendor = unitOfWork.VendorService.GetLab2LabVendor();




			return View(model);
        }

        [HttpPost]
        public IActionResult Add(VisitViewModel model, string? DoctorName, Patient patient, List<int> selecSelectedServices, List<int> SelectedServicesLab2Lab, List<int> SelectedServicesDiscount)
        {
            //update RefferedDoctor if exisit & add if not
            if (DoctorName != null)
            {

                var doctor = unitOfWork._Context.RefferedDoctors.FirstOrDefault(x => x.DoctorName.ToLower() == DoctorName.ToLower()) ?? new RefferedDoctor() {DoctorName= DoctorName, LastUpdateBy = "abdo", LastUpdateDate = DateTime.Now, LastUpdateFrom = ":::1" };

                unitOfWork._Context.Update(doctor);
                unitOfWork.Save();
                model.Visit.RefererDoctor = doctor.DrId;
            }

            for (int i = 0; i < selecSelectedServices.Count; i++)
            {
                model.VisitserviceViewModel.Add(new VisitserviceViewModel() { SelectedServices = selecSelectedServices[i], SelectedServicesDiscount = SelectedServicesDiscount[i], SelectedServicesLab2Lab = SelectedServicesLab2Lab[i] });
            }
            double totalcost = 0;

            if (ModelState.IsValid)
            {
                if (patient.PatientId == 0)
                {
                    patient.CreationDate = DateTime.Now;
                }
                else
                {
                    patient.LastUpdateDate = DateTime.Now;
                }

                unitOfWork.PatientService.Update(patient);
                unitOfWork.Save();
                model.Visit.PatientId = patient.PatientId;
                model.Visit.VisitStatus = "P";
                unitOfWork.VisitServices.Add(model.Visit);
                unitOfWork.Save();
                //var vid = model.Visit.VisitId;

                if (model.VisitserviceViewModel.Count > 0)
                {
                    foreach (var item in model.VisitserviceViewModel)
                    {
                        var numerForServiceInVisit = unitOfWork.VisitServiceServices.GetByServiceIdAndVisitId(item.SelectedServices, model.Visit.VisitId).Count;

                        if (item.SelectedServicesLab2Lab == 0)
                        {
                            item.SelectedServicesLab2Lab = null;
                        }

                        unitOfWork.VisitServiceServices.Add(new VisitService()
                        {
                            VisitId = model.Visit.VisitId,
                            ServiceId = item.SelectedServices,
                            ServiceSerial = numerForServiceInVisit + 1,
                            ServiceDate = DateTime.Now,
                            Lab2lab = item.SelectedServicesLab2Lab,
                        });

                        totalcost += unitOfWork.servicesPricelistService.GetById(item.SelectedServices).ServicePrice;
                        unitOfWork.Save();

                        var serviceStendRefid = unitOfWork._Context.ServicesPricelists.FirstOrDefault(x => x.ServiceId == item.SelectedServices).StandRef;
                        var isPackage = unitOfWork._Context.StandardServices.FirstOrDefault(x => x.ServiceId == serviceStendRefid).IsPackage;

                        #region case of package service
                        //if (serviceStendRefid != null && isPackage)
                        //{
                        //	var package = unitOfWork._Context.StandardServices.Include(x => x.Packages).FirstOrDefault(x => x.ServiceId == serviceStendRefid);

                        //	foreach (var service in package.Packages)
                        //	{
                        //		CreateBarcode(model.Visit.VisitId, service.ServiceId, serviceStendRefid);

                        //	}
                        //}
                        //else
                        //{
                        //	// Generate the barcode and save it
                        //	CreateBarcode(model.Visit.VisitId, item.SelectedServices);
                        //}


                        ////case of package service
                        //if (serviceStendRefid != null && isPackage)
                        //{
                        //    var package = unitOfWork._Context.ServicesPricelists.Include(x => x.Services).FirstOrDefault(x => x.ServiceId == item.SelectedServices);

                        //    foreach (var service in package.Services)
                        //    {
                        //        CreateBarcode(model.Visit.VisitId, service.ServiceId, package.ServiceId);

                        //    }
                        //}
                        //else
                        //{
                        //    // Generate the barcode and save it
                        //    CreateBarcode(model.Visit.VisitId, item.SelectedServices);
                        //}
                        #endregion

                        CreateBarcode(model.Visit.VisitId, item.SelectedServices);


                    }
                }

                // Add in invoice
                PatientInvoice patientInvoice = new PatientInvoice();
                patientInvoice.CreationDate = DateTime.Now;
                patientInvoice.InvoiceCost = totalcost;
                patientInvoice.Currency = "EGP";
                patientInvoice.PaymentType = "Cash";
                patientInvoice.VisitId = model.Visit.VisitId;
                patientInvoice.InvoiceStatus = "P";
                patientInvoice.LastUpdateBy = "ay7aga";
                patientInvoice.LastUpdateFrom = "2";
                patientInvoice.LastUpdateDate = DateTime.Now;
                patientInvoice.IsSupply = false;
                unitOfWork._Context.Add(patientInvoice);
                unitOfWork.Save();

                // Add in invoice details
                foreach (var item in model.VisitserviceViewModel)
                {
                    PatientInvoiceDetail invoiceDetail = new PatientInvoiceDetail();
                    invoiceDetail.InvId = patientInvoice.InvoiceId;
                    invoiceDetail.VisitId = patientInvoice.VisitId;
                    invoiceDetail.ServiceId = item.SelectedServices;
                    invoiceDetail.ServiceCost = unitOfWork.servicesPricelistService.GetById(item.SelectedServices).ServicePrice;
                    invoiceDetail.Serial = unitOfWork.VisitServiceServices.GetByServiceIdAndVisitId(item.SelectedServices, model.Visit.VisitId).Count + 1;
                    invoiceDetail.LastUpdateBy = "abdo";
                    invoiceDetail.LastUpdateFrom = ":::1";
                    invoiceDetail.LastUpdateDate = DateTime.Now;
                    unitOfWork._Context.Add(invoiceDetail);
                    unitOfWork.Save();
                }

                //string folderPath = Path.Combine("E:\\", "VisitFolder");

                //if (!Directory.Exists(folderPath))
                //{
                //    Directory.CreateDirectory(folderPath);
                //}

                return RedirectToAction("PatientInvoicePay", "PatientInvoice", new { id = model.Visit.VisitId });
            }
            else
            {
                return View(model);
            }
        }

        // Function to generate and save the barcode image


        //[HttpPost]
        //public IActionResult CreateBarcode(int visitId, int serviceId)
        //{
        //	try
        //	{
        //		// Generate the barcode for the specific service
        //		string barcodeText = $"{visitId}_{serviceId}"; // You can customize the text as needed

        //		GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(barcodeText, BarcodeWriterEncoding.Code128);
        //		barcode.ResizeTo(400, 120);
        //		barcode.AddBarcodeValueTextBelowBarcode();
        //		barcode.ChangeBarCodeColor(System.Drawing.Color.BlueViolet);
        //		barcode.SetMargins(10);

        //		string serviceFolderPath = Path.Combine(hostingEnvironment.WebRootPath, "GeneratedBarcode", $"Service_{serviceId}");

        //		if (!Directory.Exists(serviceFolderPath))
        //		{
        //			Directory.CreateDirectory(serviceFolderPath);
        //		}

        //		string fileName = $"{visitId}_{serviceId}.png";
        //		string filePath = Path.Combine(serviceFolderPath, fileName);
        //		barcode.SaveAsPng(filePath);

        //		string imageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + $"/GeneratedBarcode/Service_{serviceId}/{fileName}";
        //		ViewBag.BarcodeUri = imageUrl;
        //		// Save the path in the database
        //		var serviceBarcode = new ServiceBarcode();
        //		serviceBarcode.ServicesId = serviceId;
        //		serviceBarcode.VisitId = visitId;
        //		serviceBarcode.Barcode = filePath; // Save the path to the image
        //		unitOfWork._Context.Add(serviceBarcode);
        //		unitOfWork.Save();
        //	}
        //	catch (Exception ex)
        //	{
        //		ViewBag.ErrorMessage = "An error occurred while generating the barcode: " + ex.Message;
        //	}

        //	return RedirectToAction("DetailServiceQrCode");
        //}




        public IActionResult PrintBarcodes(int visitId)
        {
            var model = unitOfWork._Context.Visits
                .Include(x => x.VisitServices)
                .ThenInclude(x => x.Service)
                .ThenInclude(x => x.StandRefNavigation)
                .Where(x => x.VisitId == visitId)
                .ToList();

            // Create a new PDF document
            var document = new iTextSharp.text.Document();
            var memoryStream = new MemoryStream();
            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            foreach (var item in model.SelectMany(x => x.VisitServices))
            {
                // Load the barcode image
                string visitIdStr = item.VisitId.ToString();
                string serviceIdStr = item.ServiceId.ToString();
                string imageUrl = Path.Combine(hostingEnvironment.WebRootPath, "GeneratedBarcode", $"Service_{serviceIdStr}", $"{visitIdStr}_{serviceIdStr}.png");

                if (System.IO.File.Exists(imageUrl)) // Use System.IO to specify the File class
                {
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageUrl);
                    image.ScaleToFit(200f, 100f); // Set image size as per your requirement
                    document.Add(image);
                }
            }

            document.Close();

            // Send the PDF for viewing or download
            byte[] pdfBytes = memoryStream.ToArray();
            Response.Headers.Add("Content-Disposition", "inline; filename=barcodes.pdf");
            return File(pdfBytes, "application/pdf");
        }


        //public IActionResult PrintBarcodes(int visitId)
        //{
        //	var model = unitOfWork._Context.Visits
        //		.Include(x => x.VisitServices)
        //		.ThenInclude(x => x.Service)
        //		.ThenInclude(x => x.StandRefNavigation)
        //		.Where(x => x.VisitId == visitId)
        //		.ToList();

        //	using (MemoryStream memoryStream = new MemoryStream())
        //	{
        //		var writer = new iText.Kernel.Pdf.PdfWriter(memoryStream);
        //		var pdf = new iText.Kernel.Pdf.PdfDocument(writer);
        //		var document = new iText.Layout.Document(pdf);

        //		// Create a table with one row and five columns (five images per row)
        //		iText.Layout.Element.Table table = new iText.Layout.Element.Table(new float[] { 100, 100, 100, 100, 100 });
        //		int imageCount = 0;
        //		foreach (var item in model.SelectMany(x => x.VisitServices))
        //		{

        //			// Load the barcode image
        //			string visitIdStr = item.VisitId.ToString();
        //			string serviceIdStr = item.ServiceId.ToString();
        //			string imageUrl = Path.Combine(hostingEnvironment.WebRootPath, "GeneratedBarcode", $"Service_{serviceIdStr}", $"{visitIdStr}_{serviceIdStr}.png");



        //			var image = new iText.Layout.Element.Image(iText.IO.Image.ImageDataFactory.Create(imageUrl));
        //			image.ScaleToFit(100, 100); // Adjust the image size as needed

        //			// Create a cell for the image and add it to the table
        //			Cell cell = new Cell();
        //			cell.Add(image);
        //			table.AddCell(cell);

        //			imageCount++;

        //			// If five images have been added to the current row, start a new row
        //			if (imageCount % 5 == 0)
        //			{
        //				table.StartNewRow();
        //			}
        //		}






        //		document.Add(table); // Add the table to the document

        //		document.Close(); // Close the document

        //		return File(memoryStream.ToArray(), "application/pdf", "barcode.pdf");
        //	}
        //}




        [HttpPost]
        public void CreateBarcode(int visitId, int serviceId, int? pakageId = null)
        {
            try
            {

                // Get patient and service information
                var visit = unitOfWork._Context.Visits.Include(x => x.Patient).FirstOrDefault(x => x.VisitId == visitId); // Replace with your visit repository
                var service = unitOfWork._Context.ServicesPricelists.Include(x => x.StandRefNavigation).FirstOrDefault(x => x.ServiceId == serviceId); // Replace with your service repository
                if (visit != null && service != null)
                {
                    string barcodeText;
                    if (pakageId != null)
                    {
                        barcodeText = $"{visit.Patient.PatientName}_{pakageId}_{service.ServiceName}_{visit.VisitStartDate}";

                    }
                    else
                    {
                        barcodeText = $"{visit.Patient.PatientName}_{service.ServiceName}_{visit.VisitStartDate}";
                    }
                    IronBarCode.GeneratedBarcode barcode = BarcodeWriter.CreateBarcode(barcodeText, BarcodeWriterEncoding.Code128);
                    barcode.ResizeTo(400, 120);
                    barcode.AddBarcodeValueTextBelowBarcode();
                    barcode.ChangeBarCodeColor(System.Drawing.Color.BlueViolet);
                    barcode.SetMargins(10);

                    string serviceFolderPath = Path.Combine(hostingEnvironment.WebRootPath, "GeneratedBarcode", $"Service_{serviceId}");

                    if (!Directory.Exists(serviceFolderPath))
                    {
                        Directory.CreateDirectory(serviceFolderPath);
                    }

                    string fileName = $"{visitId}_{serviceId}.png";
                    string filePath = Path.Combine(serviceFolderPath, fileName);
                    barcode.SaveAsPng(filePath);

                    string imageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + $"/GeneratedBarcode/Service_{serviceId}/{fileName}";
                    ViewBag.BarcodeUri = imageUrl;

                    // Save the path in the database
                    var serviceBarcode = new ServiceBarcode();

                    if (pakageId != null)
                        serviceBarcode.PackageId = pakageId;

                    serviceBarcode.ServicesId = serviceId;
                    serviceBarcode.VisitId = visitId;
                    serviceBarcode.Barcode = filePath; // Save the path to the image
                    unitOfWork._Context.Add(serviceBarcode); // Replace with your repository
                    unitOfWork.Save(); // Save changes to the database
                }
                else
                {
                    ViewBag.ErrorMessage = "Patient or service not found.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while generating the barcode: " + ex.Message;
            }


        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            VisitDetailsViewModel model = new();
            var visit = unitOfWork._Context.Visits
                    .Include(x => x.Patient)
				.Include(x => x.VisitServices)
				.ThenInclude(x => x.Service)
				.ThenInclude(x => x.StandRefNavigation)
				.Include(x => x.ServiceBarcodes)
					.ThenInclude(x => x.SampleStatusNavigation)
					.ThenInclude(x => x.SampleLogs)
				// Eagerly load navigation property
				.FirstOrDefault(x => x.VisitId == id);

			ViewBag.sampleStatusList = unitOfWork._Context.SampleStatuses.Include(x => x.ServiceBarcodes).ToList();
            if (visit == null)
            {
                return NotFound();
            }
            model.Visit = visit;


            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            VisitViewModel model = new VisitViewModel();
            model.Visit = unitOfWork.VisitServices.GetById(id);
            model.Visit.VisitType = "LB";
            model.Visit.LastUpdateBy = "abdo";
            model.Visit.LastUpdateDate = DateTime.Now;
            model.Visit.LastUpdateFrom = "::1";
            model.RefDoctor = unitOfWork._Context.RefferedDoctors.ToList();
            model.Patients = unitOfWork.PatientService.GetAll().ToList();
            model.ServicesPricelists = unitOfWork.servicesPricelistService.GetAll().ToList();
            model.Vendors = unitOfWork.VendorService.GetAll().ToList();
            model.L2LVendor = unitOfWork.VendorService.GetLab2LabVendor();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(VisitViewModel model)
        {
            model.Visit.VisitType = "LB";
            model.Visit.LastUpdateBy = "abdo";
            model.Visit.LastUpdateDate = DateTime.Now;
            model.Visit.LastUpdateFrom = "::1";

            if (ModelState.IsValid)
            {
                unitOfWork.VisitServices.Update(model.Visit);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {

                return View(model);
            }
        }

        public IActionResult DetailServiceQrCode()
        {
            return View();
        }

        public IActionResult ServiceVisit(int id)
        {
            var model = unitOfWork._Context.Visits.Include(x => x.VisitServices).ThenInclude(x => x.Service).ThenInclude(x => x.StandRefNavigation).ThenInclude(x => x.ServiceBarcodes).Where(x => x.VisitId == id).FirstOrDefault();
            return View(model);
        }

        public IActionResult LogStatus(List<int> isCollected, List<int> isAnalisis, List<int> isCompleted, int visitId)
        {
            foreach (var barcodeId in isCollected)
            {
                var sample = new SampleLog()
                {
                    StatusId = 1,
                    BarcodeId = barcodeId,
                    LastUpdateBy = "abdo",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateFrom = ":::1",
                };
                unitOfWork._Context.Add(sample);
                var serviceBarcode = unitOfWork._Context.ServiceBarcodes.Find(barcodeId);
                serviceBarcode.SampleStatus = 1;
                unitOfWork._Context.Update(serviceBarcode);
            }
            foreach (var barcodeId in isAnalisis)
            {
                var sample = new SampleLog()
                {
                    StatusId = 2,
                    BarcodeId = barcodeId,
                    LastUpdateBy = "abdo",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateFrom = ":::1",
                };
                unitOfWork._Context.Add(sample);
                var serviceBarcode = unitOfWork._Context.ServiceBarcodes.Find(barcodeId);
                serviceBarcode.SampleStatus = 2;
                unitOfWork._Context.Update(serviceBarcode);
            }
            foreach (var barcodeId in isCompleted)
            {
                var sample = new SampleLog()
                {
                    StatusId = 3,
                    BarcodeId = barcodeId,
                    LastUpdateBy = "abdo",
                    LastUpdateDate = DateTime.Now,
                    LastUpdateFrom = ":::1",
                };
                unitOfWork._Context.Add(sample);
                var serviceBarcode = unitOfWork._Context.ServiceBarcodes.Find(barcodeId);
                serviceBarcode.SampleStatus = 3;
                unitOfWork._Context.Update(serviceBarcode);
            }
            unitOfWork.Save();
            return RedirectToAction("Details", new { id = visitId });
        }
    }

}
