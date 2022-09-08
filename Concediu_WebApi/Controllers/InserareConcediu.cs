using Concediu_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Concediu_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InserareConcediu : ControllerBase
    {
        private readonly ILogger<PaginaInregistrare> _logger;
        private readonly BreakingBreadContext _context;
        public InserareConcediu(ILogger<PaginaInregistrare> logger, BreakingBreadContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("GetTipConcediu")]
        public List<TipConcediu> GetTipConcediu()
        {
            return _context.TipConcedius.Select(x => new TipConcediu {Id = x.Id, Nume = x.Nume }).ToList();
        }


        [HttpGet("GetIdNumeAng")]
        
        public List<Angajat> getIdNume()
        {
            return _context.Angajats.Select(x => new Angajat { Id = x.Id, Nume = x.Nume , Prenume = x.Prenume}).ToList();
        }

        [HttpGet("GetAngajatId")]

        public int getAngId(DateTime StartDate, DateTime EndDate)
        {
            
            int result = _context.Concedius.Where(x => x.DataInceput <= StartDate && x.DataSfarsit >= EndDate).Select(x => x.Id).FirstOrDefault();
            return result;

        }

        [HttpPost("InsertConcediu")]
        public ActionResult InsertConcediu(Concediu con)
        {

            _context.Concedius.Add(con);

            try
            { _context.SaveChanges(); }
            catch (Exception e)
            {

                return BadRequest(e.Message);

            }
            return Ok();


        }




    }

}
