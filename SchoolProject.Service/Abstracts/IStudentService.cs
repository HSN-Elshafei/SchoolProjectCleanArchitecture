using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;

namespace SchoolProject.Service.Abstracts

{
	public interface IStudentService
	{
		public Task<List<Student>> GetStudentsListAsync();
		public Task<Student> GetStudentByIdAsync(int id);
		public Task<string> AddStudentAsync(Student student);
		public Task<string> EditStudentAsync(Student student);
		public Task<string> DeleteStudentAsync(Student student);
		public Task<bool> IsStudentExistAsync(string phone);
		public Task<bool> IsStudentExistAsync(string phone, int id);
		public IQueryable<Student> GetStudentsByDepartmentIdQuerable(int id);
		public IQueryable<Student> GetStudentsQuerable();
		public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderBy, string search);
	}
}
