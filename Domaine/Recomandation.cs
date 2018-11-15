using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Recomandation
    {
        [Key]
        public int RecomandationId { get; set; }
        public String NameDoctor { get; set; }
        public String Description { get; set; }

        public Repport repport { get; set; }

    }
}
