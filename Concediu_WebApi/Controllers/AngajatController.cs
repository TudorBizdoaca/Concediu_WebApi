using Concediu_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concediu_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AngajatController : ControllerBase
    {

        private readonly ILogger<AngajatController> _logger;
        private readonly BreakingBreadContext _context;
        public AngajatController(ILogger<AngajatController> logger, BreakingBreadContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet("GetAngajati")]
        public List<Angajat> GetAngajati()
        {

            return _context.Angajats.Select(x=>x).ToList();
        }
        [HttpGet("GetAngajat")]
        public Angajat GetAngajat(int IdAngajat)
        {
            Angajat t = new Angajat();
            t =_context.Angajats.Select(x => x).Where(x => x.Id == IdAngajat).FirstOrDefault();
            return t;
        }
        [HttpGet("GetAngajatFull")]
        public Angajat GetAngajatFull(int IdAngajat)
        {
            Angajat t = new Angajat();
            t = _context.Angajats.
                Include(x => x.ConcediuAngajats).ThenInclude(s => s.TipConcediu).
                Include(x => x.ConcediuAngajats).ThenInclude(s => s.StareConcediu).
                Include(x => x.ConcediuAngajats).ThenInclude(s => s.Inlocuitor).
                Include(x => x.ConcediuInlocuitors).ThenInclude(s => s.TipConcediu).
                Include(x => x.ConcediuInlocuitors).ThenInclude(s => s.StareConcediu).
                Include(x => x.ConcediuInlocuitors).ThenInclude(s => s.Angajat).
                Select(x => x).
                Where(x  =>x.Id == IdAngajat).
                FirstOrDefault();
            return t;
        }
    }
}
