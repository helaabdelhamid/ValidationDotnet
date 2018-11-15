namespace DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        startHour = c.DateTime(nullable: false),
                        state = c.Int(nullable: false),
                        doctor_Id = c.Int(),
                        patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.AspNetUsers", t => t.doctor_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.patient_Id)
                .Index(t => t.doctor_Id)
                .Index(t => t.patient_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Role = c.String(),
                        Email = c.String(maxLength: 256),
                        Password = c.String(nullable: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        idSocial = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Course_CourseId = c.Int(),
                        Course_CourseId1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId1)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Course_CourseId)
                .Index(t => t.Course_CourseId1);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        dateLastModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Repports",
                c => new
                    {
                        RepportId = c.Int(nullable: false, identity: true),
                        Disease = c.String(),
                        Discription = c.String(),
                        course_CourseId = c.Int(),
                    })
                .PrimaryKey(t => t.RepportId)
                .ForeignKey("dbo.Courses", t => t.course_CourseId)
                .Index(t => t.course_CourseId);
            
            CreateTable(
                "dbo.Recomandations",
                c => new
                    {
                        RecomandationId = c.Int(nullable: false, identity: true),
                        NameDoctor = c.String(),
                        Description = c.String(),
                        repport_RepportId = c.Int(),
                    })
                .PrimaryKey(t => t.RecomandationId)
                .ForeignKey("dbo.Repports", t => t.repport_RepportId)
                .Index(t => t.repport_RepportId);
            
            CreateTable(
                "dbo.Treatments",
                c => new
                    {
                        TreatmentId = c.Int(nullable: false, identity: true),
                        Discription = c.String(),
                        repport_RepportId = c.Int(),
                    })
                .PrimaryKey(t => t.TreatmentId)
                .ForeignKey("dbo.Repports", t => t.repport_RepportId)
                .Index(t => t.repport_RepportId);
            
            CreateTable(
                "dbo.Disscussions",
                c => new
                    {
                        DisscussionId = c.Int(nullable: false, identity: true),
                        Sender = c.String(),
                        Receiver = c.String(),
                        doctor_Id = c.Int(),
                        patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.DisscussionId)
                .ForeignKey("dbo.AspNetUsers", t => t.doctor_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.patient_Id)
                .Index(t => t.doctor_Id)
                .Index(t => t.patient_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Timestamp = c.String(),
                        FromUser_Id = c.Int(),
                        ToRoom_Id = c.Int(nullable: false),
                        Disscussion_DisscussionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FromUser_Id)
                .ForeignKey("dbo.Rooms", t => t.ToRoom_Id, cascadeDelete: true)
                .ForeignKey("dbo.Disscussions", t => t.Disscussion_DisscussionId)
                .Index(t => t.FromUser_Id)
                .Index(t => t.ToRoom_Id)
                .Index(t => t.Disscussion_DisscussionId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserAccount_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserAccount_Id, cascadeDelete: true)
                .Index(t => t.UserAccount_Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        doctor_Id = c.Int(),
                        patient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.AspNetUsers", t => t.doctor_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.patient_Id)
                .Index(t => t.doctor_Id)
                .Index(t => t.patient_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Notifications", "patient_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notifications", "doctor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Disscussions", "patient_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Disscussion_DisscussionId", "dbo.Disscussions");
            DropForeignKey("dbo.Messages", "ToRoom_Id", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "UserAccount_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "FromUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Disscussions", "doctor_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Treatments", "repport_RepportId", "dbo.Repports");
            DropForeignKey("dbo.Recomandations", "repport_RepportId", "dbo.Repports");
            DropForeignKey("dbo.Repports", "course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.AspNetUsers", "Course_CourseId1", "dbo.Courses");
            DropForeignKey("dbo.AspNetUsers", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.Appointments", "patient_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointments", "doctor_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Notifications", new[] { "patient_Id" });
            DropIndex("dbo.Notifications", new[] { "doctor_Id" });
            DropIndex("dbo.Rooms", new[] { "UserAccount_Id" });
            DropIndex("dbo.Messages", new[] { "Disscussion_DisscussionId" });
            DropIndex("dbo.Messages", new[] { "ToRoom_Id" });
            DropIndex("dbo.Messages", new[] { "FromUser_Id" });
            DropIndex("dbo.Disscussions", new[] { "patient_Id" });
            DropIndex("dbo.Disscussions", new[] { "doctor_Id" });
            DropIndex("dbo.Treatments", new[] { "repport_RepportId" });
            DropIndex("dbo.Recomandations", new[] { "repport_RepportId" });
            DropIndex("dbo.Repports", new[] { "course_CourseId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Course_CourseId1" });
            DropIndex("dbo.AspNetUsers", new[] { "Course_CourseId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Appointments", new[] { "patient_Id" });
            DropIndex("dbo.Appointments", new[] { "doctor_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Notifications");
            DropTable("dbo.Rooms");
            DropTable("dbo.Messages");
            DropTable("dbo.Disscussions");
            DropTable("dbo.Treatments");
            DropTable("dbo.Recomandations");
            DropTable("dbo.Repports");
            DropTable("dbo.Courses");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Appointments");
        }
    }
}
