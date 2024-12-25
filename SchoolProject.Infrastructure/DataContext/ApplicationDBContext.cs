using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Data
{
	public class ApplicationDBContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>,
														  IdentityUserRole<int>, IdentityUserLogin<int>,
														  IdentityRoleClaim<int>, IdentityUserToken<int>>
	{
		// Parameterless constructor
		public ApplicationDBContext() { }

		// Constructor with DbContextOptions
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

		// DbSet properties
		public DbSet<User> Users { get; set; }
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
