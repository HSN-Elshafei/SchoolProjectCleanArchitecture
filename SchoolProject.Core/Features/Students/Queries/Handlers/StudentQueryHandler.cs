using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;


namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
	public class StudentQueryHandler : ResponseHandler,
									   IRequestHandler<GetStudentListQuery, Response<List<GetStudentResponse>>>,
									   IRequestHandler<GetStudentByIdQuery, Response<GetStudentResponse>>,
									   IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentResponse>>
	{
		#region Fields
		private readonly IStudentService _studentService;
		private readonly IMapper _mapper;
		//private readonly IStringLocalizer<ShearedResources> _stringLocalizer;
		#endregion

		#region Ctor
		public StudentQueryHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_studentService = studentService;
			_mapper = mapper;
		}
		#endregion

		#region Handle Methodes
		public async Task<Response<List<GetStudentResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
		{
			var studentList = await _studentService.GetStudentsListAsync();
			var studentListMapper = _mapper.Map<List<GetStudentResponse>>(studentList);
			return Success(studentListMapper);
		}

		public async Task<Response<GetStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
		{
			var student = await _studentService.GetStudentByIdAsync(request.Id);
			if (student == null)
			{
				return NotFound<GetStudentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			}
			return Success(_mapper.Map<GetStudentResponse>(student));
		}

		public async Task<PaginatedResult<GetStudentResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
		{
			Expression<Func<Student, GetStudentResponse>> expression = e => new GetStudentResponse(e.StudID, e.Localize(e.NameAr, e.Name), e.Address, e.Phone, e.Localize(e.Department.DNameAr, e.Department.DName));
			IQueryable<Student> filteQuey = _studentService.FilterStudentPaginatedQuerable(request.OrderBy, request.Search);
			//var querable = _studentService.GetStudentsQuerable();
			var paginatedList = await filteQuey.Select(expression).ToPaginatedResult(request.PageNumber, request.PageSize);
			paginatedList.Meta = new { Count = paginatedList.Data.Count() };
			return paginatedList;
		}
		#endregion


	}
}
