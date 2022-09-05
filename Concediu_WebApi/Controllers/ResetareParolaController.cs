using Concediu_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Concediu_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetareParolaController : ControllerBase
    {
        private readonly BreakingBreadContext _context;
        public ResetareParolaController(BreakingBreadContext context)
        {
            _context = context;
        }

       
        [HttpPost]
        public ActionResult UpdateAngajat(string email, string parola)
        {
            Angajat angajat = _context.Angajats.Where(x => x.Email == email).FirstOrDefault();
            
            if(!String.IsNullOrEmpty(parola) && !String.IsNullOrWhiteSpace(parola) && angajat != null)
            {
                
                angajat.Parola = parola;
                _context.SaveChanges();
                return Ok("Parola resetata");
            }
            else
            {
                return BadRequest();
            }
            
        }
        
    }
}
