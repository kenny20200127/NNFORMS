using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NNPEFWEB.Data;
using NNPEFWEB.Enum;
using NNPEFWEB.Models;
using NNPEFWEB.Service;
using NNPEFWEB.ViewModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NNPEFWEB.Controllers
{
    public class PensionFormsController : Controller
    {
        private readonly IPensionService service;
        private readonly ApplicationDbContext _context;
        private readonly IDeathService deathService;
        private readonly ILogger<PensionFormsController> _logger;
        private readonly IGeneratePdf generatePdf;

        public PensionFormsController(
            IPensionService service, 
            ApplicationDbContext _context,
            IDeathService deathService, 
            ILogger<PensionFormsController> _logger,
            IGeneratePdf generatePdf)
        {
            this.service = service;
            this._context = _context;
            this.deathService = deathService;
            this._logger = _logger;
            this.generatePdf = generatePdf;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PensionGratuity()
        {

            Pension pension = new();
            pension.pensionDto = service.GetPensionCount();
            pension.deathDto = deathService.GetDeathCount();

            return View(pension);
        }

        public async Task<IActionResult> PensionInitiation(int? pageNumber, string searchString)
        {
            PensionModel model = new();
            PensionViewModel model2 = new PensionViewModel();
            string role = HttpContext.Session.GetString("userRole").ToString();

            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }

            var InitPensionList = filterPensionByRole(service.GetPensionInit(), role);
            if (!String.IsNullOrEmpty(searchString))
            {
                InitPensionList = InitPensionList.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }
            int pageSize = 5;
            model.Pensions = await PaginatedList<PensionViewModel>.CreateAsync(InitPensionList.AsQueryable(), pageNumber ?? 1, pageSize);
            model2.shipDetails = GetShip();
            model2.rankDetails = GetRank();
            model.pension = model2;

            TempData["role"] = role;
            return View(model);
        }

        public async Task<IActionResult> PensionReviews(int? pageNumber, string searchString)
        {
            PensionModel model = new PensionModel();
            string role = TempData["userRole2"] as string;
            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            PensionViewModel model2 = new PensionViewModel();
            var pensionForReview = filterPensionByRole(service.GetPensionByStatus(ApplicationConstant.Initiation), role);
            if (!String.IsNullOrEmpty(searchString))
            {
                pensionForReview = pensionForReview.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }
            var pp = pensionForReview.ToList();
            model2.shipDetails = GetShip();
            model.pension = model2;
            int pageSize = 5;
            model.Pensions = await PaginatedList<PensionViewModel>.CreateAsync(pp.AsQueryable(), pageNumber ?? 1, pageSize);
            TempData["role"] = role;
            return View(model);
        }

        public IActionResult CreatePension()
        {
            string role = TempData["userRole2"] as string;
            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            PensionModel model = new();
            PensionViewModel model2 = new PensionViewModel();
            model2.shipDetails = GetShip();
            model2.rankDetails = GetRank();
            model.pension = model2;
            TempData["role"] = role;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePensionInitiation(PensionModel model)
        {

            string createdBy = TempData["username"] as string;
            PensionViewModel viewModel = new PensionViewModel();
            viewModel = model.pension;
            viewModel.createdBy = createdBy;
            viewModel.status = ApplicationConstant.Initiation;
            var getpension = service.GetPersonnelBySvcNo(viewModel.SVC_NO);
            var result =new BaseResponse();
            if (getpension == null)
            {
                viewModel.status = null;
                 result = await service.CreatePension(viewModel);

            }
            else
            {
                 result = await service.UpdatePension(viewModel);
            }
            
            TempData["messageInit"] = result.responseMessage;
            TempData["user"] = createdBy;
            ModelState.Clear();
            return RedirectToAction("PensionInitiation", "PensionForms");
        }


        [HttpGet]
        public async Task<IActionResult> PensionUpdate(int? pageNumber, string searchString)
        {
            PensionModel model = new PensionModel();
            PensionViewModel model2 = new PensionViewModel();

            var pensionByInitation = service.GetPensionByStatus(ApplicationConstant.Initiation);
            //model.Pensions = pp;
            if (!String.IsNullOrEmpty(searchString))
            {
                pensionByInitation = pensionByInitation.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }

            model2.shipDetails = GetShip();
            model.pension = model2;
            int pageSize = 5;
            model.Pensions = await PaginatedList<PensionViewModel>.CreateAsync(pensionByInitation.AsQueryable(), pageNumber ?? 1, pageSize);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PensionUpdateReview(int? pageNumber, string searchString)
        {
            PensionModel model = new PensionModel();
            PensionViewModel mm = new PensionViewModel();
            mm.shipDetails = GetShip();
            var pensionUpdateForReview = service.GetPensionByStatus(ApplicationConstant.Update);


            if (!String.IsNullOrEmpty(searchString))
            {
                pensionUpdateForReview = pensionUpdateForReview.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }

            var pp = pensionUpdateForReview.ToList();
            //model.Pensions = pp;
            int pageSize = 5;
            model.Pensions = await PaginatedList<PensionViewModel>.CreateAsync(pp.AsQueryable(), pageNumber ?? 1, pageSize);
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> CreatePensionUpdate(PensionModel model)
        {
            PensionViewModel viewModel = new PensionViewModel();
            viewModel = model.pension;
            string createdBy = TempData["username"] as string;
            viewModel.createdBy = createdBy;
            viewModel.status = ApplicationConstant.Update;


            var result = await service.UpdatePensionForCPO(viewModel);
            return Ok(result);
        }
        public async Task<IActionResult> ApprovePension(int? pageNumber, string searchString)
        {
            string role = HttpContext.Session.GetString("userRole").ToString();
            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            var pensionForApproval = filterPensionByRole(service.GetPensionByStatus(ApplicationConstant.Update), role);
            if (!String.IsNullOrEmpty(searchString))
            {
                pensionForApproval = pensionForApproval.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }

            int pageSize = 5;
            TempData["role"] = role;
            return View(await PaginatedList<PensionViewModel>.CreateAsync(pensionForApproval.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> PensionPayment(int? pageNumber)
        {
            var pensionForApproval = service.GetPensionByStatus(ApplicationConstant.Approved);
            int pageSize = 5;
            return View(await PaginatedList<PensionViewModel>.CreateAsync(pensionForApproval.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        [Route("PensionForms/CreatePensionPayment")]
        public async Task<IActionResult> CreatePensionPayment([FromQuery] int PersonID)
        {
            string createdBy = TempData["username"] as string;
            ApprovePensionViewModel model = new();
            model.personID = PersonID;
            model.status = ApplicationConstant.Payment;
            model.createdBy = createdBy;
            var pp = await service.UpdatePensionPayment(model);

            return Ok(pp);
        }
        [HttpPost]
        [Route("PensionForms/PensionApproved")]
        public async Task<IActionResult> PensionApproved([FromQuery] int PersonID)
        {
            string createdBy = TempData["username"] as string;
            ApprovePensionViewModel model = new();
            model.personID = PersonID;
            model.status = ApplicationConstant.Approved;
            model.createdBy = createdBy;
            var pp = await service.ApprovePension(model);

            return Ok(pp);
        }

        public IActionResult PensionEnquery(string svcNo)
        {
            PensionViewModel searChPensionBySvc = new PensionViewModel();
            string role = HttpContext.Session.GetString("userRole").ToString();
            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            if (!string.IsNullOrWhiteSpace(svcNo))
            {
                if (role.ToLower() == "navsec" || role == "NAVSECADMIN" && svcNo.Substring(0, 2).ToLower() == "nn")
                {
                    searChPensionBySvc = service.GetPensionBySvcNo(svcNo);
                }
                if (role == "SYSTEMADMIN")
                {
                    searChPensionBySvc = service.GetPensionBySvcNo(svcNo);
                }

                if (role.ToLower() == "cnd" || role == "CNDADMIN" && svcNo.Substring(0, 2).ToLower() != "nn")
                {
                     searChPensionBySvc = service.GetPensionBySvcNo(svcNo);
                }
                return View(searChPensionBySvc);
            }
            else
            {
                return View();
            }

        }

        public IActionResult UploadPensionRecord()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadPensionRecord(IFormFile formfile, CancellationToken cancellationToken)
        {
            string createdBy = TempData["username"] as string;
            StringBuilder errorOutput = new StringBuilder();
            var unSuccessfulRecord = new List<PensionViewDto>();
            int rowCount = 1;
            int z = 0;
            try
            {
                if (formfile == null || formfile.Length <= 0)
                {
                    errorOutput.Append("No file for Upload");
                }
                else if (!Path.GetExtension(formfile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    errorOutput.Append("File not an Excel Format");
                }
                else
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    var stream = new MemoryStream();
                    await formfile.CopyToAsync(stream);

                    var reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    DataSet ds = new DataSet();
                    ds = reader.AsDataSet();
                    reader.Close();

                    rowCount = ds.Tables[0].Rows.Count;
                    DataTable serviceDetails = ds.Tables[0];

                    int k = 0;
                    if (ds != null && ds.Tables.Count > 0)
                    {

                        string svcNo = serviceDetails.Rows[0][0].ToString();
                        string firstname = serviceDetails.Rows[0][1].ToString();
                        string lastname = serviceDetails.Rows[0][2].ToString();
                        string middlename = serviceDetails.Rows[0][3].ToString();
                        string title = serviceDetails.Rows[0][4].ToString();



                        if (svcNo != "ServiceNo" || firstname != "FirstName" || lastname != "LastName" || middlename != "MiddleName" || title != "Title")
                        {
                            errorOutput.Append("File not in the Right format");
                        }
                        else
                        {
                            for (int row = 1; row < serviceDetails.Rows.Count; row++)
                            {
                                string SVCNO = string.IsNullOrEmpty(serviceDetails.Rows[row][0].ToString()) ? "" : serviceDetails.Rows[row][0].ToString().Trim();
                                string FIRSTNAME = string.IsNullOrEmpty(serviceDetails.Rows[row][1].ToString()) ? "" : serviceDetails.Rows[row][1].ToString().Trim();
                                string LASTNAME = string.IsNullOrEmpty(serviceDetails.Rows[row][2].ToString()) ? "" : serviceDetails.Rows[row][2].ToString().Trim();
                                string MIDDLENAME = string.IsNullOrEmpty(serviceDetails.Rows[row][3].ToString()) ? "" : serviceDetails.Rows[row][3].ToString().Trim();
                                string TITLE = string.IsNullOrEmpty(serviceDetails.Rows[row][4].ToString()) ? "" : serviceDetails.Rows[row][4].ToString().Trim();

                                var pp = new PensionViewModel()
                                {
                                    Title = TITLE,
                                    SVC_NO = SVCNO,
                                    FirstName = FIRSTNAME,
                                    LastName = LASTNAME,
                                    MiddleName = MIDDLENAME,
                                    createdBy = createdBy
                                };

                                var response = await service.AddPension(pp);
                                if (response.responseCode != "00")
                                {
                                    unSuccessfulRecord.Add(new PensionViewDto()
                                    {
                                        Title = pp.Title,
                                        FirstName = pp.FirstName,
                                        LastName = pp.LastName,
                                        MiddleName = pp.MiddleName,
                                        ServiceNo = pp.SVC_NO,
                                        Message = response.responseMessage
                                    });
                                }
                                else
                                {
                                    z++;
                                }


                            }

                        }

                    }
                }

                if (unSuccessfulRecord.Count > 0)
                {

                    var stream2 = new MemoryStream();

                    using (var package2 = new ExcelPackage(stream2))
                    {
                        var workSheet = package2.Workbook.Worksheets.Add("Sheet1");
                        workSheet.Cells.LoadFromCollection(unSuccessfulRecord, true);
                        package2.Save();
                    }
                    stream2.Position = 0;
                    string excelName = $"FormB-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";


                    return File(stream2, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }
                if (z == rowCount - 1 && string.IsNullOrWhiteSpace(errorOutput.ToString()))
                {
                    errorOutput.Append("All record successfully inserted");
                }

                TempData["messageExcel"] = errorOutput.ToString();
                return View();


            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception ==>{ex.Message}");
                return null;
            }
        }

        public async Task<IActionResult> PensionReview(int? pageNumber)
        {
            PensionModel model = new PensionModel();
            var pensionForReview = service.GetPensionByStatus(ApplicationConstant.Update);
            //var pp = pensionForReview.ToList();
            int pageSize = 5;
            model.Pensions = await PaginatedList<PensionViewModel>.CreateAsync(pensionForReview.AsQueryable(), pageNumber ?? 1, pageSize);

            return View(model);
        }

        [HttpPost]
        [Route("PensionForms/RemovePensionInitiation")]
        public async Task<IActionResult> RemovePensionInitiation([FromQuery] int PersonID)
        {
            var pp = await service.RemovePension(PersonID);
            return Ok(pp);
        }
        public IEnumerable<PensionViewModel> filterPensionByRole(IEnumerable<PensionViewModel> pensionList, string role)
        {
            var filterByRole = new List<PensionViewModel>();
            if (role.ToLower() == "navsec" || role == "NAVSECADMIN")
            {
                var unFilteredRecord = pensionList.Where(x => x.SVC_NO.Substring(0, 2).ToLower() == "nn");
                filterByRole = unFilteredRecord.ToList();
            }
            if (role == "SYSTEMADMIN")
            {
                var unFilteredRecord = pensionList;
                filterByRole = unFilteredRecord.ToList();
            }

            if (role.ToLower() == "cnd"|| role == "CNDADMIN")
            {
                var unFilteredRecord2 = pensionList.Where(x => x.SVC_NO.Substring(0, 2).ToLower() != "nn");
                filterByRole = unFilteredRecord2.ToList();
            }

            return filterByRole;
        }

        public List<SelectListItem> GetRank()
        {
            var ranksList = (from rk in _context.ef_ranks
                             select new SelectListItem()
                             {
                                 Text = rk.rankName,
                                 Value = rk.rankName.ToString(),
                             }).ToList();

            ranksList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return ranksList;
        }
        public List<SelectListItem> GetShip()
        {
            var shipList = (from rk in _context.ef_ships
                            select new SelectListItem()
                            {
                                Text = rk.shipName,
                                Value = rk.shipName
                            }).OrderBy(x=>x.Text).ToList();

            shipList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return shipList;
        }
        public async Task<FileResult> DownloadBForm()
        {
            string fileName = "ny_nn9bRecord.xlsx";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(bytes, "application/octet", fileName);

        }
        public IActionResult PensionStatusReport()
        {
            var roles = new List<SelectListItem>()
            {
                new SelectListItem{ Text="All",Value="All"},
                new SelectListItem{ Text="NAVSEC",Value="NAVSEC"},
                new SelectListItem{ Text="CPO",Value="CPO"},
                new SelectListItem{ Text="CND",Value="CND"},
                new SelectListItem{ Text="APPROVED",Value="APPROVED"}
            };
            var model = new PensionReportRequestModel();
            model.roles = roles;
            return View(model);
        }

        
        [HttpPost]
        public async Task<IActionResult> PensionStatusReport(PensionReportRequestModel payload)
        {
            var roles = new List<SelectListItem>()
            {
                new SelectListItem{ Text="All",Value="All"},
                new SelectListItem{ Text="NAVSEC",Value="NAVSEC"},
                new SelectListItem{ Text="CPO",Value="CPO"},
                new SelectListItem{ Text="CND",Value="CND"},
                new SelectListItem{ Text="APPROVED",Value="APPROVED"}
            };
            var model = new PensionReportRequestModel();
            var pp = new List<PensionStatusExcelReport>();
            model.roles = roles;

            //get the value
            var reportList = await service.getPensionStatusReport(payload.filteredValue);
            //filtered result
            var filteredReportList = new List<PensionReportModel>();
            reportList.ToList().ForEach(x =>
            {
                filteredReportList.Add(new PensionReportModel()
                {
                    Names = x.Names,
                    SVC_NO = x.SVC_NO,
                    status = x.status,
                    Amount = x.Amount,
                    DateInitiated = x.DateInitiated,
                    LastUpdateDate = GetlastUpdatedDate(x.datecreated, x.datecreated2, x.datecreated3)
                });
            });

            if(filteredReportList.ToList().Count > 0)
            {
                if (payload.filterType == "excel")
                {
                    filteredReportList.ToList().ForEach(x =>
                    {
                        pp.Add(new PensionStatusExcelReport
                        {
                            Status = x.status,
                            SVCNO = x.SVC_NO,
                            Amount = x.Amount == null ? 0M : x.Amount,
                            Names = x.Names,
                            DateInitiated = x.DateInitiated.ToString("dd MMMM yyyy"),
                            LastUpdatedDate = x.LastUpdateDate == DateTime.MinValue ? "" : x.LastUpdateDate.ToString("dd MMMM yyyy")
                        });
                    });

                    var stream2 = new MemoryStream();

                    using (var package2 = new ExcelPackage(stream2))
                    {
                        var workSheet = package2.Workbook.Worksheets.Add("Sheet1");
                        workSheet.Cells.LoadFromCollection(pp, true);
                        package2.Save();
                    }
                    stream2.Position = 0;
                    string excelName = $"StatusReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                    return File(stream2, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

                }
                else if(payload.filterType=="pdf")
                {
                    var newReportList = new List<PensionReportDTO>();
                    filteredReportList.ToList().ForEach(x =>
                    {
                        newReportList.Add(new PensionReportDTO
                        {
                            SVC_NO = x.SVC_NO,
                            DateInitiated = x.DateInitiated.ToString("dd MMMM yyyy"),
                            DatePaid = x.DatePaid == DateTime.MinValue ? "" : x.DatePaid.ToString("dd MMMM yyyy"),
                            LastUpdateDate = x.LastUpdateDate == DateTime.MinValue ? "" : x.LastUpdateDate.ToString("dd MMMM yyyy"),
                            Amount = x.Amount == null ? 0M : x.Amount,
                            Names = x.Names,
                            status = x.status
                        });
                    });
                    return await generatePdf.GetPdf("Views/PensionForms/PensionStatusReportPage.cshtml", newReportList);
                }
                else
                {
                    TempData["messageReport"] = "Please select the format for your report";
                    return View(model);
                }

            }
            else
            {
                TempData["messageReport"] = "there is no record";
                return View(model);
            }

        }

        public IActionResult PensionMainReport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PensionMainReport(PensionReportRequestModel payload)
        {
            var model = new PensionReportRequestModel();
            var pp = new List<PensionMainExcelReport>();

            if (payload.start <= payload.end)
            {
                //get the value
                var reportList = await service.getPensionpaidReport(payload.start.Date, payload.end.Date);
                if(reportList.ToList().Count>0) {
                    if (payload.filterType == "excel")
                    {
                        reportList.ToList().ForEach(x =>
                        {
                            pp.Add(new PensionMainExcelReport
                            {
                                SVCNO = x.SVC_NO,
                                Amount = x.Amount == null ? 0M : x.Amount,
                                Names = x.Names,
                                DateInitiated = x.DateInitiated.ToString("dd MMMM yyyy"),
                                DatePaid = x.DatePaid.ToString("dd MMMM yyyy")
                            });
                        });

                        var stream2 = new MemoryStream();

                        using (var package2 = new ExcelPackage(stream2))
                        {
                            var workSheet = package2.Workbook.Worksheets.Add("Sheet1");
                            workSheet.Cells.LoadFromCollection(pp, true);
                            package2.Save();
                        }
                        stream2.Position = 0;
                        string excelName = $"MainReport-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
                        return File(stream2, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

                    }
                    else if(payload.filterType=="pdf")
                    {
                        return await generatePdf.GetPdf("Views/PensionForms/PensionMainReportPage.cshtml", reportList);
                    }
                    else
                    {
                        TempData["messageReport"] = "Please select the right format";
                        return View();
                    }
                }
                else
                {
                    TempData["messageReport"] = "there is no record";
                    return View();
                }
                
            }
            else
            {
                TempData["messageReport"] = "start date must be less than end date";
                return View();
            }

        }

        public DateTime GetlastUpdatedDate(DateTime first, DateTime second, DateTime third)
        {
            DateTime output = DateTime.MinValue;
            if (first != DateTime.MinValue && second == DateTime.MinValue && third == DateTime.MinValue)
            {
                output = first;
            }

            if (first != DateTime.MinValue && second != DateTime.MinValue && third == DateTime.MinValue)
            {
                output = second;
            }

            if (first != DateTime.MinValue && second != DateTime.MinValue && third != DateTime.MinValue)
            {
                output = third;
            }

            return output;
        }

        public async Task<IActionResult> PensionFormsReport(int id)
        {
            
                //get the value
                var reportList = await service.getPensionFormsReport(id);
                if (reportList!=null)
                {
                        return await generatePdf.GetPdf("Views/PensionForms/PensionFormReportPage.cshtml", reportList);
                }
                else
                {
                    TempData["messageReport"] = "there is no record";
                    return View();
                }

        }
        
    }
}
