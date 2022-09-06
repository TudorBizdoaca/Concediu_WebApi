using Concediu_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concediu_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabelConcediiController : ControllerBase
    {

        private readonly BreakingBreadContext _context;
        public TabelConcediiController(BreakingBreadContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Concediu> GetConcedii()
        {
            return _context.Concedius.Include(x => x.Angajat)
                .Include(x => x.StareConcediu)
                .Include(x => x.TipConcediu)
                .Select(x => new Concediu 
                {  Angajat = new Angajat { Nume = x.Angajat.Nume, Prenume = x.Angajat.Prenume, Manager = new Angajat{ Nume = x.Angajat.Manager.Nume, Prenume = x.Angajat.Manager.Prenume} }, 
                    TipConcediu = new TipConcediu { Nume = x.TipConcediu.Nume},
                    Inlocuitor = new Angajat { Nume = x.Inlocuitor.Nume, Prenume = x.Inlocuitor.Prenume},
                    DataInceput = x.DataInceput, DataSfarsit = x.DataSfarsit, 
                    StareConcediu = new StareConcediu { Nume = x.StareConcediu.Nume},
                    Id = x.Id
                   
                })
                .ToList();
        }
    }
}
