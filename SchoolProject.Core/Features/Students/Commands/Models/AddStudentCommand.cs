﻿using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Students.Commands.Models
{
	public class AddStudentCommand : IRequest<Response<string>>
	{
		public required string Name { get; set; }
		public required string NameAr { get; set; }
		public string Address { get; set; }
		public required string Phone { get; set; }
		public int? DeptId { get; set; }
	}
}
