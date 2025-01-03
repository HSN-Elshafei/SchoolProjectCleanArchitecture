using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
	{
		#region Fields
		private readonly IStudentRepository _studentRepository;
		#endregion

		#region Ctor
		public StudentService(IStudentRepository studentRepository)
		{
			_studentRepository = studentRepository;
		}
		#endregion

		#region Handles Functions
		public async Task<List<Student>> GetStudentsListAsync()
		{
			return await _studentRepository.GetStudentsListAsync();
		}
		public async Task<Student> GetStudentByIdAsync(int id)
		{
			return await _studentRepository.GetTableNoTracking().Include(x => x.Department).Where(x => x.StudID == id).FirstOrDefaultAsync();
		}

		public async Task<string> AddStudentAsync(Student std)
		{
			if (await IsStudentExistAsync(std.Phone))
			{
				return "Exist";
			}
			await _studentRepository.AddAsync(std);
			return "Success";
		}
		public async Task<bool> IsStudentExistAsync(string phone)
		{
			var studentResult = await _studentRepository.GetTableNoTracking().Where(x => x.Phone == phone).FirstOrDefaultAsync();
			if (studentResult != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public async Task<bool> IsStudentExistAsync(string phone, int id)
		{
			var studentResult = await _studentRepository.GetTableNoTracking().Where(x => x.Phone == phone & !x.StudID.Equals(id)).FirstOrDefaultAsync();
			if (studentResult != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public async Task<string> EditStudentAsync(Student student)
		{
			await _studentRepository.UpdateAsync(student);
			return "Success";
		}
		public async Task<string> DeleteStudentAsync(Student student)
		{
			var tran = _studentRepository.BeginTransaction();
			try
			{
				await _studentRepository.DeleteAsync(student);
				await tran.CommitAsync();
				return "Success";
			}
			catch
			{
				await tran.RollbackAsync();
				return "Faild";
			}
		}

		public IQueryable<Student> GetStudentsQuerable()
		{
			return _studentRepository.GetTableNoTracking().Include(d => d.Department);
		}



		public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderBy, string search)
		{
			var querable = GetStudentsQuerable();
			if (search != null)
			{
				querable = querable.Where(s => s.Name.Contains(search) || s.Address.Contains(search) || s.Department.DName.Contains(search));
			}
			switch (orderBy)
			{
				case StudentOrderingEnum.StudID:
					querable = querable.OrderBy(s => s.StudID);
					break;
				case StudentOrderingEnum.Name:
					querable = querable.OrderBy(s => s.Name);
					break;
				case StudentOrderingEnum.Address:
					querable = querable.OrderBy(s => s.Address);
					break;
				case StudentOrderingEnum.Phone:
					querable = querable.OrderBy(s => s.Phone);
					break;
				case StudentOrderingEnum.DName:
					querable = querable.OrderBy(s => s.Department.DName);
					break;
			}

			return querable;
		}

		public IQueryable<Student> GetStudentsByDepartmentIdQuerable(int id)
		{
			return _studentRepository.GetTableNoTracking().Where(s => s.DID == id).AsQueryable();
		}
		#endregion
	}
}
