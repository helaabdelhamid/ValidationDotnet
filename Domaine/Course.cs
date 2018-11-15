using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaine
{
    public class Course
    {
        public int CourseId { get; set; }
        public DateTime dateLastModification { get; set; }
        public List<Patient> patients { get; set; }
        public List<Doctor> doctors { get; set; }
        public List<Repport> reports { get; set; }
    }
}
