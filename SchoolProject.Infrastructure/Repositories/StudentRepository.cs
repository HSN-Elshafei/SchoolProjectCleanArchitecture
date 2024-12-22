using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Repositories
{
	public class StudentRepository : GenericRepositoryAsync<Student>,IStudentRepository
	{
		#region Fields
		private readonly DbSet<Student> _student;
		#endregion

		#region Ctor
		public StudentRepository(ApplicationDBContext dbContext):base(dbContext)
		{
			_student = dbContext.Set<Student>();
		}
		#endregion

		#region Handles Functions
		public async Task<List<Student>> GetStudentsListAsync()
		{
			return await _student.Include(x=>x.Department).ToListAsync();
		}
		#endregion
	}
}
