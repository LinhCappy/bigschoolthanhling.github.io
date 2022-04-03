using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BigSchool_1811063011_ThanhLinh.Models
{
    public partial class BigSchoolContext : DbContext
    {
        public BigSchoolContext()
            : base("name=BigSchoolContext2")
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Following> Followings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(e => e.Attendances)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);
        }
    }
}
