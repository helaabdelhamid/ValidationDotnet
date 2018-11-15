using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]

        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Time")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]

        public DateTime startHour { get; set; }

        public state state { get; set; }
        public Patient patient { get; set; }
        public Doctor doctor { get; set; }
    }
}
