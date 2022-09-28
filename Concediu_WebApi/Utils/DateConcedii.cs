using Concediu_WebApi.Models;
namespace Concediu_WebApi.Utils
{
    public class DateConcedii
    {
        public List<Concediu>? ListaConcedii { get; set; }
        public int nrConcedii { get; set; }

        public DateConcedii(List<Concediu>? listaConcedii, int nrConcedii)
        {
            ListaConcedii = listaConcedii;
            this.nrConcedii = nrConcedii;
        }
    }
}
