using Concediu_WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Concediu_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutentificareController : ControllerBase
    {
        private readonly ILogger<AutentificareController> _logger;
        private readonly BreakingBreadContext _context;

        public AutentificareController(ILogger<AutentificareController> logger, BreakingBreadContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("LogInUtilizator")]
        public Angajat GetLogInUtilizator(string email, string parola)
        {
            if (email != null && parola != null)
            {
                return _context.Angajats.Select(x => x).Where(x => x.Email == email && x.Parola == parola).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
