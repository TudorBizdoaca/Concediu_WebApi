using Concediu_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concediu_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IstoricConcediiController : ControllerBase
    {
        private readonly ILogger<IstoricConcediiController> _logger;
        private readonly BreakingBreadContext _context;
       
        public IstoricConcediiController(ILogger<IstoricConcediiController> logger, BreakingBreadContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet("GetConcediiAngajat")]
        public List<Concediu> GetConcediiAngajat(int Id)
        {
            return _context.Concedius.
                Include(x => x.StareConcediu).
                Include(x => x.TipConcediu).
                Include(x => x.Inlocuitor).Where(x => x.AngajatId == Id).
                Select(x => new Concediu
                { DataInceput = x.DataInceput,
                    DataSfarsit = x.DataSfarsit,
                    Comentarii = x.Comentarii,
                    Inlocuitor = new Angajat()
                    {
                        Nume = x.Inlocuitor.Nume,
                        Prenume = x.Inlocuitor.Prenume,
                    },
                    TipConcediu = new TipConcediu()
                    {
                        Nume = x.TipConcediu.Nume
                    },
                    StareConcediu = new StareConcediu()
                    {
                        Nume = x.StareConcediu.Nume
                    }

                }).OrderByDescending(x=>x.DataInceput.Date).ToList();
            
        }
    }
}
