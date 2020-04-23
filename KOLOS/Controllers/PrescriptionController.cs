using KOLOS.Services;
using Microsoft.AspNetCore.Mvc;

namespace KOLOS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase

    {
        private IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public IActionResult GetAll(
            [FromQuery(Name = "sortby")] string sortBy = "date",
            [FromQuery(Name = "sortDir")] string sortDir = "DESC"
        )
        {
            var prescription = _prescriptionService.GetAll(sortBy, sortDir);
            return Ok(prescription);
        }

        [HttpPost]
        public IActionResult Add(Request request)
        {
            if (_prescriptionService.AddPrescription(request))
            {
                return Ok("Added!");
            }

            return BadRequest();
        }
    }
}