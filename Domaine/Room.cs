using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public virtual User UserAccount { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
