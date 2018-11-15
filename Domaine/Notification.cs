using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public Patient patient { get; set; }
        public Doctor doctor { get; set; }
    }
}
