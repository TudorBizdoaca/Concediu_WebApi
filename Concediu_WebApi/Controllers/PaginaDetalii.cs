using Concediu_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concediu_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaginaDetalii : ControllerBase
    {
        private readonly ILogger<PaginaDetalii> _logger;
        private readonly BreakingBreadContext _context;

        public PaginaDetalii(ILogger<PaginaDetalii> logger, BreakingBreadContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet("GetConcediubyId")]
        public Concediu GetConcediubyId( int idConcediu)
        {
            return _context.Concedius.Include(x => x.Angajat)
                .Include(x => x.StareConcediu)
                .Include(x => x.TipConcediu)
                .Where(x => x.Id == idConcediu)
                .Select(x => new Concediu
                {
                    Comentarii = x.Comentarii,
                    Angajat = new Angajat { Nume = x.Angajat.Nume + " " + x.Angajat.Prenume, Manager = new Angajat { Nume = x.Angajat.Manager.Nume + " " +  x.Angajat.Manager.Prenume } },
                    TipConcediu = new TipConcediu { Nume = x.TipConcediu.Nume },
                    Inlocuitor = new Angajat { Nume = x.Inlocuitor.Nume + " " +  x.Inlocuitor.Prenume },
                    DataInceput = x.DataInceput,
                    DataSfarsit = x.DataSfarsit,
                    StareConcediu = new StareConcediu { Nume = x.StareConcediu.Nume },
                    Id = x.Id,
                    MotivRespingere = x.MotivRespingere

                }).ToList().FirstOrDefault();
        }

        [HttpPost("UpdateStareConcediu")]

        public ActionResult UpdateStareConcediu(Concediu c)
        {
            var result = _context.Concedius.SingleOrDefault(x => x.Id == c.Id);

            if (result != null)
            {
                result.StareConcediuId = c.StareConcediuId;
                _context.SaveChanges();
                return Ok();
            }
            else
                return BadRequest();
        }


    }
}
