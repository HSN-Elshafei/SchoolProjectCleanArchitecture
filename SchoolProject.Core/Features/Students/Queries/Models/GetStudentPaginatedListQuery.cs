﻿using MediatR;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Helper;

namespace SchoolProject.Core.Features.Students.Queries.Models
{

	public class GetStudentPaginatedListQuery : IRequest<PaginatedResult<GetStudentResponse>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public StudentOrderingEnum OrderBy { get; set; }
		public string? Search { get; set; }
	}
}