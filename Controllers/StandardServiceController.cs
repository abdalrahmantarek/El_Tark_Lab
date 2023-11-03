
using LAB.IService;
using LAB.Models;
using LAB.Service;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace LAB.Controllers
{
    public class StandardServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StandardServiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {

            var model = _unitOfWork.StandardServices.GetAll();

            return View("Index", model);
        }
        [HttpPost]
        public IActionResult AddStandardService(StandardService sd)
        {

            sd.ServiceAlphaCode = 1;
            _unitOfWork.StandardServices.Add(sd);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

      


        public IActionResult importfile()
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


                        List<StandardService> serviceserror = new List<StandardService>();


						for (int row = 2; row <= rowCount; row++) // Changed loop condition
						{
                            var regexname = "[A-Z]";

                            var regexavailability ="[Yy]";

                            var regexispackage ="[^(?i)(true|false)$\r\n]";




							var serviceName = worksheet.Cells[row, 1].Value?.ToString(); // Use ?. to handle null values
							var serviceAvailability = worksheet.Cells[row, 2].Value?.ToString();
							var serviceAlphaCode = worksheet.Cells[row, 3].Value?.ToString();
							var serviceUnit = worksheet.Cells[row, 4].Value?.ToString();
							var isPackageStr = worksheet.Cells[row, 5].Value?.ToString();

                            if (string.IsNullOrWhiteSpace(serviceName) || Regex.IsMatch(serviceName,regexname) ||
								string.IsNullOrWhiteSpace(serviceAvailability) || Regex.IsMatch(serviceAvailability, regexavailability)||
								string.IsNullOrWhiteSpace(serviceAlphaCode) || serviceAlphaCode =="1"||
								string.IsNullOrWhiteSpace(serviceUnit) || Regex.IsMatch(serviceUnit, regexname) ||
								string.IsNullOrWhiteSpace(isPackageStr) || Regex.IsMatch(isPackageStr, regexispackage)
								)
							{
								// Handle missing or invalid data as needed
								serviceserror.Add(new StandardService() ); // Skip this row and proceed to the next
							}
                            if (serviceserror.Count > 0) 
                            {
                                return View("error", serviceserror);
                            }

							// Now, parse the values
							if (int.TryParse(serviceAlphaCode, out int serviceAlphaCodeValue) &&
								bool.TryParse(isPackageStr, out bool isPackage))
							{
								_unitOfWork.StandardServices.Add(new StandardService
								{
									ServiceName = serviceName,
									ServiceAvailability = serviceAvailability,
									ServiceAlphaCode = serviceAlphaCodeValue,
									ServiceUnit = serviceUnit,
									IsPackage = isPackage
								});
							}
							else
							{
								// Handle parsing errors as needed
								continue; // Skip this row and proceed to the next
							}
						}

						_unitOfWork.Save();


					}

                }





            }



            return RedirectToAction("Index");
        }





    }
}








