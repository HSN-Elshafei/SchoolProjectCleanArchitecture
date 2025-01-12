﻿using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstracts
{
	public interface IDepartmentService
	{
		public Task<Department> GetDepartmentByIdAsync(int id);
		public Task<bool> IsDepartmentExistAsync(int? id);
	}
}
