using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Disscussion
    {
        [Key]
        public int DisscussionId { get; set; }
        public String Sender { get; set; }
        public String Receiver { get; set; }
        public Patient patient { get; set; }
        public Doctor doctor { get; set; }
        public List<Message> messages { get; set; }
    }
}
