using Concediu_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Runtime.InteropServices;

namespace Concediu_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotivRespingereController : ControllerBase
    {

        private readonly ILogger<MotivRespingereController> _logger;
        private readonly BreakingBreadContext _context;
        public MotivRespingereController(ILogger<MotivRespingereController> logger, BreakingBreadContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("updateMotivRespingereConcediu")]
        public ActionResult updateMotivRespingereConcediu (Concediu c)
        {
            var result = _context.Concedius.SingleOrDefault(x => x.Id == c.Id);

            if (result != null)
            {
                result.MotivRespingere = c.MotivRespingere;
                result.StareConcediuId = c.StareConcediuId;
                _context.SaveChanges();
                return Ok();
            }
            else
                return BadRequest();
        }
    }
}
