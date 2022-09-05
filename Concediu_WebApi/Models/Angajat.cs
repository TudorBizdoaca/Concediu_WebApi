using System;
using System.Collections.Generic;

namespace Concediu_WebApi.Models
{
    public partial class Angajat
    {
        public Angajat()
        {
            ConcediuAngajats = new HashSet<Concediu>();
            ConcediuInlocuitors = new HashSet<Concediu>();
            InverseManager = new HashSet<Angajat>();
        }

        public int Id { get; set; }
        public string Nume { get; set; } = null!;
        public string Prenume { get; set; } = null!;
        public string? Email { get; set; }
        public string? Parola { get; set; }
        public DateTime DataAngajare { get; set; }
        public DateTime DataNasterii { get; set; }
        public string Cnp { get; set; } = null!;
        public string Serie { get; set; } = null!;
        public string No { get; set; } = null!;
        public string? NrTelefon { get; set; }
        public byte[]? Poza { get; set; }
        public bool? EsteAdmin { get; set; }
        public int? ManagerId { get; set; }
        public int? ZileConcediu { get; set; }

        public virtual Angajat? Manager { get; set; }
        public virtual ICollection<Concediu> ConcediuAngajats { get; set; }
        public virtual ICollection<Concediu> ConcediuInlocuitors { get; set; }
        public virtual ICollection<Angajat> InverseManager { get; set; }
    }
}
