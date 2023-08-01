﻿using Application.Common.Interfaces.Persistence;
using Application.Teachers.Common;
using Application.Common.Errors;
using Domain.TeacherAggregate;
using ErrorOr;
using MediatR;
using Domain.UserAggregate;

namespace Application.Teachers.CreateTeacher.Commands;

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, ErrorOr<TeacherCreatedResult>>
{
    private readonly ITeacherRepository _teacherRepository;

    public CreateTeacherCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    public async Task<ErrorOr<TeacherCreatedResult>> Handle(CreateTeacherCommand command, CancellationToken cancellationToken)
    {
        if (await _teacherRepository.GetTeacherByUserId(new UserId(command.UserId)) is not null)
        {
            return Errors.Teacher.DuplicateUserId;
        }

        var teacher = Teacher.Create(new UserId(command.UserId));

        _teacherRepository.Create(teacher);

        return new TeacherCreatedResult(teacher);
    }

}
