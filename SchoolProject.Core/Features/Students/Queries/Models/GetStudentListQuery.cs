﻿using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Responses;

namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Response<List<GetStudentResponse>>>
    {

    }
}
