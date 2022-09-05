using System;
using System.Collections.Generic;

namespace Concediu_WebApi.Models
{
    public partial class StareConcediu
    {
        public StareConcediu()
        {
            Concedius = new HashSet<Concediu>();
        }

        public int Id { get; set; }
        public string Nume { get; set; } = null!;
        public string Cod { get; set; } = null!;

        public virtual ICollection<Concediu> Concedius { get; set; }
    }
}
