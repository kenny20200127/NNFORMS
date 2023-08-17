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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace NNPEFWEB.Controllers
{
    public class DeathFormsController : Controller
    {
        private readonly IPensionService service;
        private readonly IDeathService deathService;
        private readonly ApplicationDbContext _context;
        private readonly IGeneratePdf generatePdf;
        private readonly ILogger<DeathFormsController> _logger;
        public DeathFormsController(
            IPensionService service, 
            IDeathService deathService,
            ApplicationDbContext _context,
             IGeneratePdf generatePdf,
            ILogger<DeathFormsController> _logger)
        {
            this.deathService = deathService;
            this.service = service;
            this._context = _context;
            this.generatePdf = generatePdf;
            this._logger = _logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Initiation(int? pageNumber, string searchString)
        {
            DeathModel model = new();
            DeathViewModel model2 = new();
            string role = TempData["userRole2"] as string;
            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            var initList = filterDeathByRole(deathService.GetDeathInit(), role);
            model2.shipDetails = GetShip();
            if (!String.IsNullOrEmpty(searchString))
            {
                initList = initList.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }

            model.death = model2;

            int pageSize = 5;
            TempData["role"] = role;

            model.deaths = await PaginatedList<DeathViewModel>.CreateAsync(initList.AsQueryable(), pageNumber ?? 1, pageSize);
            model.death.rankDetails = GetRank();
            return View(model);

        }
        public IActionResult CreateDeathBen()
        {
            DeathModel model = new();
            DeathViewModel model2 = new();
            string role = TempData["userRole2"] as string;
            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            model2.shipDetails = GetShip();
            model2.rankDetails = GetRank();
            model.death = model2;
            TempData["role"] = role;
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> CreateInitiation(DeathModel model)
        {
            DeathViewModel model2 = new DeathViewModel();
            model2 = model.death;
            string createdBy = TempData["username"] as string;
            model2.createdby = createdBy;
            model2.status = ApplicationConstant.Initiation;
            model2.shipDetails = GetShip();
            var getdeath = deathService.GetPersonnelBySvcNo(model2.SVC_NO);
            if (getdeath == null)
            {
                model.death.status = null;
                await deathService.CreateDeath(model2);

            }
            else
            {
                await deathService.UpdateDeath(model2);
            }
            //var result = await deathService.UpdateDeath(model2);

            ModelState.Clear();
            return RedirectToAction("Initiation", "DeathForms");
        }

        public async Task<IActionResult> DeathReviews(int? pageNumber, string searchString)
        {
            DeathModel model = new();
            DeathViewModel model2 = new DeathViewModel();
            string role = TempData["userRole2"] as string;
            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            var deathForReview = filterDeathByRole(deathService.GetDeathByStatus(ApplicationConstant.Initiation), role);
            var pp = deathForReview.ToList();
            model2.shipDetails = GetShip();
            model.death = model2;

            if (!String.IsNullOrEmpty(searchString))
            {
                deathForReview = deathForReview.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }

            TempData["role"] = role;
            int pageSize = 5;
            model.deaths = await PaginatedList<DeathViewModel>.CreateAsync(pp.AsQueryable(), pageNumber ?? 1, pageSize);
            return View(model);
        }

        public async Task<IActionResult> DeathUpdateReview(int? pageNumber, string searchString)
        {
            DeathModel model = new();
            DeathViewModel model2 = new();
            model2.shipDetails = GetShip();
            var deathUpdateForReview = deathService.GetDeathByStatus(ApplicationConstant.Update);
            var pp = deathUpdateForReview.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                deathUpdateForReview = deathUpdateForReview.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }

            model.death = model2;
            int pageSize = 5;
            model.deaths = await PaginatedList<DeathViewModel>.CreateAsync(pp.AsQueryable(), pageNumber ?? 1, pageSize); ;
            return View(model);

        }
        public async Task<IActionResult> DeathUpdate(int? pageNumber, string searchString)
        {
            DeathModel model = new DeathModel();
            DeathViewModel viewModel = new DeathViewModel();
            viewModel.shipDetails = GetShip();
            var deathByInitation = deathService.GetDeathByStatus(ApplicationConstant.Initiation);

            if (!String.IsNullOrEmpty(searchString))
            {
                deathByInitation = deathByInitation.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }
            var pp = deathByInitation.ToList();
            model.death = viewModel;
            int pageSize = 5;
            model.deaths = await PaginatedList<DeathViewModel>.CreateAsync(pp.AsQueryable(), pageNumber ?? 1, pageSize);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeathUpdate(DeathModel model)
        {

            string createdBy = TempData["username"] as string;
            model.death.createdby = createdBy;
            model.death.status = ApplicationConstant.Update;

            var result = await deathService.UpdateDeathForCPO(model.death);
            return Ok(result);
        }


        public async Task<IActionResult> ApproveDeath(int? pageNumber, string searchString)
        {
            string role = TempData["userRole2"] as string;
            if (string.IsNullOrEmpty(role))
            {
                role = TempData["role"] as string;
            }
            var deathGratuityForApproval = filterDeathByRole(deathService.GetDeathByStatus(ApplicationConstant.Update), role);
            int pageSize = 5;
            if (!String.IsNullOrEmpty(searchString))
            {
                deathGratuityForApproval = deathGratuityForApproval.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }
            TempData["role"] = role;

            return View(await PaginatedList<DeathViewModel>.CreateAsync(deathGratuityForApproval.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> DeathPayment(int? pageNumber, string searchString)
        {
            var pensionForApproval = deathService.GetDeathByStatus(ApplicationConstant.Approved);
            if (!String.IsNullOrEmpty(searchString))
            {
                pensionForApproval = pensionForApproval.Where(s => s.SVC_NO.ToLower() == searchString.ToLower());
            }
            int pageSize = 5;
            return View(await PaginatedList<DeathViewModel>.CreateAsync(pensionForApproval.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        [HttpPost]
        [Route("DeathForms/CreateDeathPayment")]
        public async Task<IActionResult> CreateDeathPayment([FromQuery] int PersonID)
        {
            string createdBy = TempData["username"] as string;
            ApproveDeathViewModel model = new();
            model.personID = PersonID;
            model.status = ApplicationConstant.Payment;
            model.createdBy = createdBy;
            var pp = await deathService.ApproveDeathPayment(model);

            return Ok(pp);
        }



        //PersonnelLogin/ApprovePension
        [HttpPost]
        [Route("DeathForms/DeathApproved")]
        public async Task<IActionResult> DeathApproved([FromQuery] int PersonID)
        {
            string createdBy = TempData["username"] as string;
            ApproveDeathViewModel model = new();
            model.personID = PersonID;
            model.status = ApplicationConstant.Approved;
            model.createdBy = createdBy;
            var pp = await deathService.ApproveDeath(model);

            return Ok(pp);
        }

        public IActionResult DeathEnquery(string svcNo)
        {
            if (!string.IsNullOrWhiteSpace(svcNo))
            {
                DeathViewModel searChDeathBySvc = deathService.GetDeathBySvcNo(svcNo);
                return View(searChDeathBySvc);

            }
            else
            {
                return View();
            }

        }

        public IActionResult UploadDeathRecord()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadDeathRecord(IFormFile formfile, CancellationToken cancellationToken)
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

                                var pp = new DeathViewModel()
                                {
                                    Title = TITLE,
                                    SVC_NO = SVCNO,
                                    FirstName = FIRSTNAME,
                                    LastName = LASTNAME,
                                    MiddleName = MIDDLENAME,
                                    createdby = createdBy
                                };

                                var response = await deathService.AddDeath(pp);
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
                    string excelName = $"FormC-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";


                    return File(stream2, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
                }
                if (z == rowCount - 1 && string.IsNullOrWhiteSpace(errorOutput.ToString()))
                {
                    errorOutput.Append("All record successfully inserted");
                }

                TempData["messageDExcel"] = errorOutput.ToString();
                return View();


            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception ==>{ex.Message}");
                return null;
            }
        }

        public async Task<IActionResult> DeathReview(int? pageNumber)
        {
            DeathModel model = new();
            DeathViewModel model2 = new DeathViewModel();
            var deathForReview = deathService.GetDeathByStatus(ApplicationConstant.Update);
            var pp = deathForReview.ToList();
            model2.shipDetails = GetShip();

            int pageSize = 5;
            model.deaths = await PaginatedList<DeathViewModel>.CreateAsync(pp.AsQueryable(), pageNumber ?? 1, pageSize); ;

            return View(model);
        }

        //PersonnelLogin/ApprovePension
        [HttpPost]
        [Route("DeathForms/RemoveDeathInitiation")]
        public async Task<IActionResult> RemoveDeathInitiation([FromQuery] int personID)
        {
            var pp = await deathService.RemoveDeath(personID);
            return Ok(pp);
        }

        public IActionResult DeathGratuity()
        {

            return View();

        }
        public IEnumerable<DeathViewModel> filterDeathByRole(IEnumerable<DeathViewModel> deathList, string role)
        {
            var filterByRole = new List<DeathViewModel>();
            if (role.ToLower() == "navsec"|| role == "NAVSECADMIN")
            {
                var unFilteredRecord = deathList.Where(x => x.SVC_NO.Substring(0, 2).ToLower() == "nn");
                filterByRole = unFilteredRecord.ToList();
            }
            if (role== "SYSTEMADMIN")
            {
                var unFilteredRecord = deathList;
                filterByRole = unFilteredRecord.ToList();
            }

            if (role.ToLower() == "cnd"||role=="CNDADMIN")
            {
                var unFilteredRecord2 = deathList.Where(x => x.SVC_NO.Substring(0, 2).ToLower() != "nn");
                filterByRole = unFilteredRecord2.ToList();
            }

            return filterByRole;
        }

        public IActionResult DeathStatusReport()
        {
            var roles = new List<SelectListItem>()
            {
                new SelectListItem{ Text="All",Value="All"},
                new SelectListItem{ Text="NAVSEC",Value="NAVSEC"},
                new SelectListItem{ Text="CPO",Value="CPO"},
                new SelectListItem{ Text="CND",Value="CND"},
                new SelectListItem{ Text="APPROVED",Value="APPROVED"}
            };
            var model = new DeathReportRequestModel();
            model.roles = roles;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeathStatusReport(DeathReportRequestModel payload)
        {
            var roles = new List<SelectListItem>()
            {
                new SelectListItem{ Text="All",Value="All"},
                new SelectListItem{ Text="NAVSEC",Value="NAVSEC"},
                new SelectListItem{ Text="CPO",Value="CPO"},
                new SelectListItem{ Text="CND",Value="CND"},
                new SelectListItem{ Text="APPROVED",Value="APPROVED"}
            };
            var model = new DeathReportRequestModel();
            var pp = new List<DeathStatusExcelReport>();
            model.roles = roles;

            //get the value
            var reportList = await deathService.getDeathStatusReport(payload.filteredValue);
            //filtered record
            var filterStatusReportList= new List<DeathReportModel>();
            reportList.ToList().ForEach(x =>
            {
                filterStatusReportList.Add(new DeathReportModel()
                {
                    Amount = x.Amount,
                    status = x.status,
                    SVC_NO = x.SVC_NO,
                    Names = x.Names,
                    DateInitiated = x.DateInitiated,
                    LastUpdateDate = GetlastUpdatedDate(x.datecreated, x.datecreated2, x.datecreated3)
                });
            });
            if (filterStatusReportList.ToList().Count > 0)
            {
                if (payload.filterType == "excel")
                {
                    filterStatusReportList.ToList().ForEach(x =>
                    {
                        pp.Add(new DeathStatusExcelReport
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
                    var newReportList = new List<DeathReportDTO>();
                    filterStatusReportList.ForEach(x =>
                    {
                        newReportList.Add(new DeathReportDTO
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
                    return await generatePdf.GetPdf("Views/DeathForms/DeathStatusReportPage.cshtml", newReportList);
                }
                else
                {
                    TempData["messageReport"] = "Please check the right format";
                    return View(model);
                }

            }
            else
            {
                TempData["messageReport"] = "there is no record";
                return View(model);
            }

        }

        public IActionResult DeathMainReport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeathMainReport(DeathReportRequestModel payload)
        {
            var model = new DeathReportRequestModel();
            var pp = new List<DeathMainExcelReport>();

            if (payload.start <= payload.end)
            {
                //get the value
                var reportList = await deathService.getDeathpaidReport(payload.start.Date, payload.end.Date);
                if (reportList.ToList().Count > 0)
                {
                    if (payload.filterType == "excel")
                    {
                        reportList.ToList().ForEach(x =>
                        {
                            pp.Add(new DeathMainExcelReport
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
                    else if(payload.filterType =="pdf")
                    {
                        return await generatePdf.GetPdf("Views/DeathForms/DeathMainReportPage.cshtml", reportList);
                    }
                    else
                    {
                        TempData["messageReport"] = "Please select the right format";
                        return View(model);
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
                            }).OrderBy(x => x.Text).ToList();

            shipList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            return shipList;
        }
        public async Task<FileResult> DownloadCForm()
        {
            string fileName = "ny_nn9cRecord.xlsx";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(bytes, "application/octet", fileName);

        }

        public DateTime GetlastUpdatedDate(DateTime first,DateTime second,DateTime third)
        {
            DateTime output = DateTime.MinValue;
            if(first!= DateTime.MinValue && second==DateTime.MinValue && third == DateTime.MinValue)
            {
                output= first;
            }

            if(first != DateTime.MinValue && second != DateTime.MinValue && third == DateTime.MinValue)
            {
                output= second;
            }

            if (first != DateTime.MinValue && second != DateTime.MinValue && third != DateTime.MinValue)
            {
                output= third;
            }

            return output;
        }
        public async Task<IActionResult> DeathFormsReport(int id)
        {

            //get the value
            var reportList = await deathService.getDeathFormsReport(id);
            if (reportList != null)
            {
                return await generatePdf.GetPdf("Views/DeathForms/DeathFormReportPage.cshtml", reportList);
            }
            else
            {
                TempData["messageReport"] = "there is no record";
                return View();
            }

        }
    }
}
