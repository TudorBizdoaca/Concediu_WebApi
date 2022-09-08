using Concediu_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Concediu_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaginaInregistrare : ControllerBase
    { 
        private readonly ILogger<PaginaInregistrare> _logger;
        private readonly BreakingBreadContext _context;
        public PaginaInregistrare(ILogger<PaginaInregistrare> logger, BreakingBreadContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("InsertAngajat")]
        public ActionResult InsertAngajat(Angajat ang)
        {

            _context.Angajats.Add(ang);
            try
            { _context.SaveChanges(); }
            catch (Exception e)
            {

                return BadRequest(e.Message);

            }
            return Ok();
        }


        [HttpGet("GetAngajatByEmail")]
        public Angajat getAngajatByEmail(string email)
        {
            return _context.Angajats.Select(x => x).Where(x => x.Email == email).FirstOrDefault();
        }


      

    }
}