using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }
        public String Discription { get; set; }
        public Repport repport { get; set; }
    }
}
