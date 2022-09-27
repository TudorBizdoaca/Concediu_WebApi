using Concediu_WebApi.Models;

namespace Concediu_WebApi.Utils
{
    public class DateAngajati
    {
        public List<Angajat>? ListaAngajati { get; set; }

        public int NrAngajati { get; set; }

        public DateAngajati(List<Angajat>? ListaAngajati, int NrAngajati)
        {
            this.ListaAngajati = ListaAngajati;
            this.NrAngajati = NrAngajati;
        }
    }
}
