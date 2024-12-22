using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data
{
	public class ApplicationDBContext : DbContext
	{
		// Parameterless constructor
		public ApplicationDBContext() { }

		// Constructor with DbContextOptions
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

		// DbSet properties
		public DbSet<Student> Students { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Subject> Subjects { get; set; }
		public DbSet<DepartmetSubject> DepartmentSubjects { get; set; }
		public DbSet<StudentSubject> StudentSubjects { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<Ins_Subject> Ins_Subjects { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DepartmetSubject>(entity =>
			{
				entity.HasKey(ds => new { ds.SubID, ds.DID });
			});


			modelBuilder.Entity<Ins_Subject>(entity =>
			{
				entity.HasKey(si => new { si.SubId, si.InsId });
			});

			modelBuilder.Entity<StudentSubject>(entity =>
			{
				entity.HasKey(ss => new { ss.SubID, ss.StudID });
			});

			modelBuilder.Entity<Instructor>(entity =>
			{
				entity.HasOne(i => i.Supervisor)
					  .WithMany(i => i.Instructors)
					  .HasForeignKey(i => i.SupervisorId)
					  .OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<Department>(entity =>
			{
				entity.HasOne(d => d.Instructor)
					  .WithOne(d => d.DepartmentManager)
					  .HasForeignKey<Department>(d => d.InsManager)
					  .OnDelete(DeleteBehavior.Restrict);
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}
