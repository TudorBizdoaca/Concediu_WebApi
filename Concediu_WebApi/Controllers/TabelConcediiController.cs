﻿using Concediu_WebApi.Models;
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

        [HttpGet("GetConcedii")]
        public List<Concediu> GetConcedii()
        {
            return _context.Concedius.Include(x => x.Angajat)
                .Include(x => x.StareConcediu)
                .Include(x => x.TipConcediu)
                .Select(x => new Concediu
                { Angajat = new Angajat { Nume = x.Angajat.Nume, Prenume = x.Angajat.Prenume, Manager = new Angajat { Nume = x.Angajat.Manager.Nume, Prenume = x.Angajat.Manager.Prenume } },
                    TipConcediu = new TipConcediu { Nume = x.TipConcediu.Nume },
                    Inlocuitor = new Angajat { Nume = x.Inlocuitor.Nume, Prenume = x.Inlocuitor.Prenume },
                    DataInceput = x.DataInceput, DataSfarsit = x.DataSfarsit,
                    StareConcediu = new StareConcediu { Nume = x.StareConcediu.Nume },
                    Id = x.Id

                })
                .ToList();
        }


        [HttpGet("GetConcediiByStareId")]
        public List<Concediu> GetConcediiByStareId(int stareId)
        {
            return _context.Concedius.Include(x => x.Angajat)
              .Include(x => x.StareConcediu)
              .Include(x => x.TipConcediu).Where(x => x.StareConcediuId == stareId)
              .Select(x => new Concediu
              {
                  Angajat = new Angajat { Nume = x.Angajat.Nume, Prenume = x.Angajat.Prenume, Manager = new Angajat { Nume = x.Angajat.Manager.Nume, Prenume = x.Angajat.Manager.Prenume } },
                  TipConcediu = new TipConcediu { Nume = x.TipConcediu.Nume },
                  Inlocuitor = new Angajat { Nume = x.Inlocuitor.Nume, Prenume = x.Inlocuitor.Prenume },
                  DataInceput = x.DataInceput,
                  DataSfarsit = x.DataSfarsit,
                  StareConcediu = new StareConcediu { Nume = x.StareConcediu.Nume },
                  Id = x.Id

              })
              .ToList();
        }


        [HttpGet("GetStariConcedii")]
        public List<StareConcediu> GetStariConcedii()
        {
            return _context.StareConcedius.ToList();
        }


        [HttpGet("GetConcediiDupaNumeAngajat")]
        public List<Concediu> GetConcediiDupaNumeAngajat(string nume)
        {

            return _context.Concedius.Include(x => x.Angajat)
               .Include(x => x.StareConcediu)
               .Include(x => x.TipConcediu).Where(x => x.Angajat.Nume.Contains(nume))
               .Select(x => new Concediu
               {
                   Angajat = new Angajat { Nume = x.Angajat.Nume, Prenume = x.Angajat.Prenume, Manager = new Angajat { Nume = x.Angajat.Manager.Nume, Prenume = x.Angajat.Manager.Prenume } },
                   TipConcediu = new TipConcediu { Nume = x.TipConcediu.Nume },
                   Inlocuitor = new Angajat { Nume = x.Inlocuitor.Nume, Prenume = x.Inlocuitor.Prenume },
                   DataInceput = x.DataInceput,
                   DataSfarsit = x.DataSfarsit,
                   StareConcediu = new StareConcediu { Nume = x.StareConcediu.Nume },
                   Id = x.Id

               })
               .ToList();

        }

        [HttpGet("GetConcediiIntreDataInceputSiDataFinal")]
        public List<Concediu> GetConcediiIntreDataInceputSiDataFinal(DateTime dataInceput, DateTime dataFinal)
        {
            List<Concediu> listaReturnata = new List<Concediu>();

            if (dataInceput <= dataFinal)
            {
                listaReturnata = _context.Concedius.Include(x => x.Angajat)
               .Include(x => x.StareConcediu)
               .Include(x => x.TipConcediu)
               .Where(x => (x.DataInceput.Date >= dataInceput.Date && x.DataInceput.Date <= dataFinal.Date
                                    && x.DataSfarsit.Date >= dataInceput.Date && x.DataSfarsit.Date <= dataFinal.Date))
               .Select(x => new Concediu
               {
                   Angajat = new Angajat { Nume = x.Angajat.Nume, Prenume = x.Angajat.Prenume, Manager = new Angajat { Nume = x.Angajat.Manager.Nume, Prenume = x.Angajat.Manager.Prenume } },
                   TipConcediu = new TipConcediu { Nume = x.TipConcediu.Nume },
                   Inlocuitor = new Angajat { Nume = x.Inlocuitor.Nume, Prenume = x.Inlocuitor.Prenume },
                   DataInceput = x.DataInceput,
                   DataSfarsit = x.DataSfarsit,
                   StareConcediu = new StareConcediu { Nume = x.StareConcediu.Nume },
                   Id = x.Id

               })
               .ToList();
            }
            return listaReturnata;
        }

        [HttpGet("GetConcediiByTipConcediuId")]
        public List<Concediu> GetConcediiByTipConcediuId(int tipConcediuId)
        {
            return _context.Concedius.Include(x => x.Angajat)
             .Include(x => x.StareConcediu)
             .Include(x => x.TipConcediu).Where(x => x.TipConcediuId == tipConcediuId)
             .Select(x => new Concediu
             {
                 Angajat = new Angajat { Nume = x.Angajat.Nume, Prenume = x.Angajat.Prenume, Manager = new Angajat { Nume = x.Angajat.Manager.Nume, Prenume = x.Angajat.Manager.Prenume } },
                 TipConcediu = new TipConcediu { Nume = x.TipConcediu.Nume },
                 Inlocuitor = new Angajat { Nume = x.Inlocuitor.Nume, Prenume = x.Inlocuitor.Prenume },
                 DataInceput = x.DataInceput,
                 DataSfarsit = x.DataSfarsit,
                 StareConcediu = new StareConcediu { Nume = x.StareConcediu.Nume },
                 Id = x.Id

             })
             .ToList();
        }


        [HttpGet("GetTipConcedii")]
        public List<TipConcediu> GetTipConcedii()
        {
            return _context.TipConcedius.Select(x => x).ToList();
        }

    }
}
