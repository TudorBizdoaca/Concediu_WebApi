using Concediu_WebApi.Models;
using Concediu_WebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Concediu_WebApi.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class InserareConcediuController : ControllerBase
    {
        private readonly ILogger<PaginaInregistrare> _logger;
        private readonly BreakingBreadContext _context;
        List<ZileLibereAnCurent> ZileLibere = new List<ZileLibereAnCurent>();
        public InserareConcediuController(ILogger<PaginaInregistrare> logger, BreakingBreadContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("getTipuriConcediu")]
        public List<TipConcediu> GetTipConcedius()
        {
            return _context.TipConcedius.Select(x => x).ToList();
        }
        [HttpGet("getZileConcediu")]
        public async Task<Dictionary<int, int>> getZileConcediuAsync(int idAngajat)
        {
            ZileLibere = await GetLibereAsync(2022);
            List<Concediu> concedii = _context.Concedius.Where(x => x.AngajatId == idAngajat && x.DataInceput.Year == DateTime.Now.Year).Include(x => x.StareConcediu).Select(x => x).ToList();
            List<TipConcediu> tipuriConcediu = _context.TipConcedius.Select(x => x).ToList();
            Dictionary<int, int> zileConcediuPerTip = new Dictionary<int, int>();
            foreach (TipConcediu tc in tipuriConcediu)
            {
                if (tc.Nume == "medical")
                    zileConcediuPerTip.Add(tc.Id, 90);
                else
                    zileConcediuPerTip.Add(tc.Id, 21);
            }
            foreach (Concediu concediu in concedii)
                if (concediu.StareConcediu.Nume != "respins")
                        zileConcediuPerTip[(int)concediu.TipConcediuId] -= DateCalculator.businessDaysBetween(concediu.DataInceput, concediu.DataSfarsit, ZileLibere);

            return zileConcediuPerTip;
        }
        [HttpPut("setZileConcediu")]
        public void setZileConcediu(int idAngajat, int Zile)
        {
            Angajat ang = _context.Angajats.Where(x => x.Id == idAngajat).Select(x => x).FirstOrDefault();
            ang.ZileConcediu = Zile;
            _context.Angajats.Update(ang);
            _context.SaveChanges();

        }
        [HttpPost("insertConcediu")]

        public void insertConcediu(Concediu concediu)
        {
            _context.Concedius.Add(concediu);
            _context.SaveChanges();
        }
        [HttpGet("esteAngajatInConcediu")]
        public bool esteAngajatInConcediu(int id, DateTime dataInceput, DateTime dataFinal)
        {
            foreach (Concediu c in _context.Concedius)
            {
                if ((c.DataInceput <= dataFinal) && (c.DataSfarsit >= dataInceput) && c.AngajatId == id)
                    return true;
            }
            return false;
        }
        [HttpGet("getAngajati")]
        public List<Angajat> getAngajati(int Id)
        {
            return _context.Angajats.Where(x => x.Id != Id).Select(x => new Angajat { Id = x.Id, Nume = x.Nume + " " + x.Prenume }).ToList();
        }


        [HttpGet("zileLibereLegale")]
        public async Task<List<ZileLibereAnCurent>> GetLibereAsync(int An)
        {
            List<ZileLibereAnCurent> ZileLibereLista;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://zilelibere.webventure.ro/api/" + An);
            string responseBody = await response.Content.ReadAsStringAsync();
           
            ZileLibereLista = JsonConvert.DeserializeObject<List<ZileLibereAnCurent>>(responseBody);
            return ZileLibereLista;
        }
    }

}
