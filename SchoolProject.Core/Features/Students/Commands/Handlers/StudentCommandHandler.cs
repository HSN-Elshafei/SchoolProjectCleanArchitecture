using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
	public class StudentCommandHandler : ResponseHandler,
										 IRequestHandler<AddStudentCommand, Response<string>>,
										 IRequestHandler<EditStudentCommand, Response<string>>,
										 IRequestHandler<DeleteStudentCommand, Response<string>>
	{
		#region Fields
		private readonly IStudentService _studentService;
		private readonly IMapper _mapper;
		//private readonly IStringLocalizer<ShearedResources> _stringLocalizer;
		#endregion

		#region Ctor
		public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_studentService = studentService;
			_mapper = mapper;
		}
		#endregion

		#region Methods
		public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
		{
			var studentMapper = _mapper.Map<Student>(request);
			string result = await _studentService.AddStudentAsync(studentMapper);
			if (result == "Exist")
			{
				return UnprocessableEntity<string>(_stringLocalizer["This Student Exist"]);
			}
			else if (result == "Success")
			{
				return Created<string>("");
			}
			else
			{
				return BadRequest<string>();
			}
		}

		public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
		{
			var student = await _studentService.GetStudentByIdAsync(request.Id);
			if (student == null) return NotFound<string>();
			var studentMapper = _mapper.Map<Student>(request);
			string result = await _studentService.EditStudentAsync(studentMapper);
			if (result == "Success")
			{
				return Updated<string>();
			}
			else return BadRequest<string>();

		}

		public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
		{
			var student = await _studentService.GetStudentByIdAsync(request.Id);
			if (student == null) return NotFound<string>();
			string result = await _studentService.DeleteStudentAsync(student);
			if (result == "Success")
			{
				return Deleted<string>();
			}
			else return BadRequest<string>();

		}
		#endregion

	}
}
