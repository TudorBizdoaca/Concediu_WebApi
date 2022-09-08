using Concediu_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concediu_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaginaMeaController : ControllerBase
    {
        private readonly ILogger<PaginaMeaController> _logger;
        private readonly BreakingBreadContext _context;

        public PaginaMeaController(ILogger<PaginaMeaController> logger, BreakingBreadContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public Angajat Get(string email, string parola)
        {
            return _context.Angajats.Select(x => x).Where(x => x.Email == email && x.Parola == parola).FirstOrDefault();
        }

        [HttpPost("UpdateAngajat")]
        public ActionResult PostUpdateAngajat(Angajat angajat)
        {
            var result = _context.Angajats.SingleOrDefault(a => a.Id == angajat.Id);

            if (result != null)
            {
                result.Nume = angajat.Nume;
                result.Prenume = angajat.Prenume;
                result.Cnp = angajat.Cnp;
                result.Serie = angajat.Serie;
                result.No = angajat.No;
                result.Email = angajat.Email;
                result.NrTelefon = angajat.NrTelefon;
                result.DataNasterii = angajat.DataNasterii;
                result.DataAngajare = angajat.DataAngajare;
                result.Poza = angajat.Poza;
                _context.SaveChanges();

                return Ok("Datele angajatului au fost editate cu succes!");
            }
            else
            {
                return BadRequest("Ceva nu a functionat!");
            }
        }
    }
}
