using Concediu_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Runtime.InteropServices;

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

        [HttpGet("GetNrAngajati")]
        public int GetNrAngajati(int id, bool esteAdmin)
        {
            if (esteAdmin == false)
            {
                return _context.Angajats.Select(x => x.ManagerId == id).Count();
            }
            else
            {
                return _context.Angajats.Count();
            }
        }

        [HttpGet("GetAngajati")]
        public List<Angajat> GetAngajati(int position, int id, bool esteAdmin)
        {
            if (esteAdmin == false)
            {
                var nextPage = _context.Angajats.Select(x => x).Where(x => x.ManagerId == id).OrderBy(x => x.Id).Skip(position).Take(10).ToList();
                return nextPage;
            }
            else
            {
                var nextPage = _context.Angajats.Select(x => x).OrderBy(x => x.Id).Skip(position).Take(10).ToList();

                return nextPage;
            }
            
        }

        [HttpGet("GetAngajat")]
        public Angajat GetAngajat(int IdAngajat)
        {
            Angajat t = new Angajat();
            t = _context.Angajats.Select(x => x).Where(x => x.Id == IdAngajat).FirstOrDefault();
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
                Where(x => x.Id == IdAngajat).
                FirstOrDefault();
            return t;
        }
        [HttpGet("GetAngajatiFull")]
        public List<Angajat> GetAngajatiFull()
        {
            


            return _context.Angajats.
                   Include(x => x.ConcediuAngajats).ThenInclude(s => s.TipConcediu).
                   Include(x => x.ConcediuAngajats).ThenInclude(s => s.StareConcediu).
                   Include(x => x.ConcediuAngajats).ThenInclude(s => s.Inlocuitor).
                   Include(x => x.ConcediuInlocuitors).ThenInclude(s => s.TipConcediu).
                   Include(x => x.ConcediuInlocuitors).ThenInclude(s => s.StareConcediu).
                   Include(x => x.ConcediuInlocuitors).ThenInclude(s => s.Angajat).
                   Select(x => x).
                   ToList();


        }
        [HttpPut("UpdateAngajat")]
        public void UpdateAngajat([FromBody]Angajat angajat)
        {
            Angajat t = new Angajat();
            t = GetAngajat(angajat.Id);
            t.ManagerId = angajat.ManagerId;
          
            t.Manager = GetAngajat((int)t.ManagerId);
            /*Add methods for Lists
               public virtual ICollection<Concediu> ConcediuAngajats { get; set; }
        public virtual ICollection<Concediu> ConcediuInlocuitors { get; set; }
        public virtual ICollection<Angajat> InverseManager { get; set; } */
            _context.SaveChanges();
        }

    
    }   
    
}
