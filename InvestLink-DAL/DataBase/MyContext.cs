using InvestLink_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestLink_DAL.DataBase
{
    public class MyContext:DbContext
    {

        public MyContext(DbContextOptions<MyContext> opt) : base(opt)
        {

        }



        public DbSet<Project> Projects { get; set; }

        public DbSet<FollowUpReport> FollowUpReports { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Nationality> Nationalitys { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Investor> Investors { get; set; }
        public DbSet<ProjectInvestor> ProjectInvestors { get; set; }
        public DbSet<ProjectFollowUp> ProjectFollowUps { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        //----------------------------------------------
        //--------------------------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectFollowUp>()
                .HasKey(sc => new { sc.Id, sc.EmployeeId, sc.ProjectId });


            modelBuilder.Entity<ProjectFollowUp>()
            .HasOne(bc => bc.Employee)
            .WithMany(b => b.ProjectFollowUps)
            .HasForeignKey(bc => bc.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<ProjectFollowUp>()
            .HasOne(bc => bc.Project)
            .WithMany(c => c.ProjectFollowUps)
            .HasForeignKey(bc => bc.ProjectId)
            .OnDelete(DeleteBehavior.ClientSetNull);
            //--------------------------------------------
            modelBuilder.Entity<ProjectInvestor>()
               .HasKey(sc => new { sc.Id, sc.InvestorId, sc.ProjectId });


            modelBuilder.Entity<ProjectInvestor>()
            .HasOne(bc => bc.Investor)
            .WithMany(b => b.ProjectInvestors)
            .HasForeignKey(bc => bc.InvestorId)
            .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<ProjectInvestor>()
            .HasOne(bc => bc.Project)
            .WithMany(c => c.ProjectInvestors)
            .HasForeignKey(bc => bc.ProjectId)
            .OnDelete(DeleteBehavior.ClientSetNull);



        }
    }
}

