using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public string Timestamp { get; set; }

        public virtual User FromUser { get; set; }

        [Required]
        public virtual Room ToRoom { get; set; }
    }
}
