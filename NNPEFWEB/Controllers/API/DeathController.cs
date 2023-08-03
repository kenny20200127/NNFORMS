using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NNPEFWEB.Service;

namespace NNPEFWEB.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeathController : ControllerBase
    {
        private readonly IDeathService deathService;
        public DeathController(IDeathService deathService)
        {
            this.deathService = deathService;
        }

        //api/Death/DeathDetails
        [Route("DeathDetails")]
        [HttpGet]
        public IActionResult GetDeath([FromQuery] string svcNo)
        {
            var result = deathService.GetPersonnelBySvcNo(svcNo);

            return Ok(result);
        }

        //api/Death/GetDeathByPersonID
        [HttpGet]
        [Route("GetDeathByPersonID")]
        public IActionResult GetDeathBySvc([FromQuery] int PersonID)
        {

            var deathByInitation = deathService.GetDeathByPersonID(PersonID);

            return Ok(deathByInitation);
        }

    }
}
