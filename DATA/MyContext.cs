//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Domain;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Domaine;

//namespace DATA
//{
//  public  class MyContext : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
//    {


        
//        public static MyContext Create()
//        {
//            return new MyContext();
//        }

//        static MyContext()
//        {
//            Database.SetInitializer<MyContext>(null);
//        }

//        public MyContext() : base("EpioneDB")
//        {

//        }

//        public DbSet<Appointment> Appointments  { get; set; }

//        public DbSet<Course> Courses { get; set; }

//        public DbSet<Disscussion> Discussions { get; set; }

//        public DbSet<Doctor> Doctors { get; set; }

//        public DbSet<Message> Messages { get; set; }

//        public DbSet<Notification> Notifications { get; set; }

//        public DbSet<Patient> Patients { get; set; }

//        public DbSet<Recomandation> Recomendations { get; set; }

//        public DbSet<Repport> Repports { get; set; }

//        public DbSet<Treatment> Treatments { get; set; }

        






//    }
//}
