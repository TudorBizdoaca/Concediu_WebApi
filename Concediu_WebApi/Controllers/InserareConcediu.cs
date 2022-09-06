using Concediu_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;



namespace Concediu_WebApi.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class InserareConcediu
    {
        private readonly ILogger<PaginaInregistrare> _logger;
        private readonly BreakingBreadContext _context;
        public InserareConcediu(ILogger<PaginaInregistrare> logger, BreakingBreadContext context)
        {
            _logger = logger;
            _context = context;
        }

        //[HttpGet("[InserareConcediu]")]
        //public 

    }

}
