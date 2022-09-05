using Concediu_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concediu_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly BreakingBreadContext _context;
       public WeatherForecastController(ILogger<WeatherForecastController> logger, BreakingBreadContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public List <Concediu> Get()
        {
            return _context.Concedius.Include(x=>x.TipConcediu).Select(x => new Concediu() { Id = x.Id, DataInceput = x.DataInceput }).Where(x => x.Id == 1).ToList();

        }
    }
}