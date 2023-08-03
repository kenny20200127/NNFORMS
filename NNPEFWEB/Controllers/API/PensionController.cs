using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using NNPEFWEB.Enum;
using NNPEFWEB.Models;
using NNPEFWEB.Service;
using System.IO;
using System.Threading.Tasks;

namespace NNPEFWEB.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionController : ControllerBase
    {
        private readonly IPensionService pensionService;
        public PensionController(IPensionService pensionService)
        {
            this.pensionService = pensionService;
        }

        //api/Pension/PensionDetails
        [Route("PensionDetails")]
        [HttpGet]
        public IActionResult GetPension([FromQuery] string svcNo)
        {
            var result= pensionService.GetPersonnelBySvcNo(svcNo);

            return Ok(result);
        }

        //api/Pension/GetPensionByPersonID
        [HttpGet]
        [Route("GetPensionByPersonID")]
        public IActionResult GetPensionBySvc([FromQuery] int PersonID)
        {

            var pensionByInitation = pensionService.GetPensionByPersonID(PersonID);

            return Ok(pensionByInitation);
        }

        //api/Pension/ApprovePension
       

        //api/pension/DownloadFile
        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile()
        {
            var fileName = "ny_nn9bRecord.xlsx";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);
            var provider = new FileExtensionContentTypeProvider();
            if(!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes= await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType,Path.GetFileName(filePath));
        }


    }
}
