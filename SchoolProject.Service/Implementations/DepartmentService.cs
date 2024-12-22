using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
	public class DepartmentService : IDepartmentService
	{
		#region Fields
		private readonly IDepartmentRepository _departmentRepository;
		#endregion

		#region Ctor
		public DepartmentService(IDepartmentRepository departmentRepository)
		{
			_departmentRepository = departmentRepository;
		}
		#endregion

		#region Handles Functions
		public async Task<Department> GetDepartmentByIdAsync(int id)
		{
			return await _departmentRepository.GetTableNoTracking()
											  //.Include(x => x.Students)
											  .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
											  .Include(x => x.Instructors)
											  .Include(x => x.Instructor)
											  .Where(x => x.DID == id).FirstOrDefaultAsync();
		}

		public async Task<bool> IsDepartmentExistAsync(int? id)
		{
			return await _departmentRepository.GetTableNoTracking().AnyAsync(x => x.DID == id);
		}

		#endregion
	}
}
