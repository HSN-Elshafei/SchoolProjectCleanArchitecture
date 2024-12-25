using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Responses;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Departments.Queries.Handlers
{
	public class DepartmentQueryHandler : ResponseHandler,
										  IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentResponse>>
	{
		#region Fields
		private readonly IDepartmentService _departmentService;
		private readonly IStudentService _studentService;
		private readonly IMapper _mapper;
		//private readonly IStringLocalizer<ShearedResources> _stringLocalizer;
		#endregion

		#region Ctor
		public DepartmentQueryHandler(IDepartmentService departmentService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer, IStudentService studentService) : base(stringLocalizer)
		{
			_departmentService = departmentService;
			_mapper = mapper;
			_studentService = studentService;
		}
		#endregion

		public async Task<Response<GetDepartmentResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
		{
			var response = await _departmentService.GetDepartmentByIdAsync(request.Id);
			if (response == null)
			{
				return NotFound<GetDepartmentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			}
			var mapper = _mapper.Map<GetDepartmentResponse>(response);
			Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Localize(e.NameAr, e.Name));
			var studentQuerable = _studentService.GetStudentsByDepartmentIdQuerable(request.Id);
			var paginatedList = await studentQuerable.Select(expression).ToPaginatedResult(request.StudentPageNumber, request.StudentPageSize);
			paginatedList.Meta = new { Count = paginatedList.Data.Count() };
			mapper.StudentsList = paginatedList;
			return Success(mapper);
		}
	}
}
