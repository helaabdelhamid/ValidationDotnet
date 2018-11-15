using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Repport
    {
        [Key]
        public int RepportId { get; set; }
        public String Disease { get; set; }
        public String Discription { get; set; }
        public Course course { get; set; }
        public List<Treatment> treatments { get; set; }

        public List<Recomandation> recommandations { get; set; }
    }
}
