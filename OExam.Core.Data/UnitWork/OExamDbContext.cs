using OExam.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OExam.Core.Data
{
    class OExamDbContext : DbContext
    {
        public OExamDbContext()
            : base("OExamDB")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //建立表之间的约束关系
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<MenuPage> MenuPages { get; set; }

    }
}
