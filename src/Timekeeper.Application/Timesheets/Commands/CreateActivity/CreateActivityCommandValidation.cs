using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Timekeeper.Application.Common.Interfaces;
using Timekeeper.Application.Common.Validations;

namespace Timekeeper.Application.Timesheets.Commands.CreateActivity;

public class CreateActivityCommandValidation : AbstractValidator<CreateActivityCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateActivityCommandValidation(IApplicationDbContext context)
    {
        _context = context;

        // RuleFor(v => v.TimesheetId)
        //     .NotEmpty().WithMessage("TimesheetId is required.");

        RuleFor(v => v.Note)
            .MaximumLength(200).WithMessage("Note must not exceed 200 characters.");

        RuleFor(v => v.TaskItem)
            .NotNull().WithMessage("TaskItem is required.");

        RuleFor(v => v.TaskItem.Id)
            .NotEmpty().WithMessage("TaskItem.Id is required.")
            .MaximumLength(20).WithMessage("TaskItem.Id must not exceed 20 characters.");

        RuleFor(v => v.TaskItem.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(v => v.TaskItem.Link)
            .NotEmpty().WithMessage("Link is required.")
            .Must(link => Uri.TryCreate(link, UriKind.Absolute, out _)).WithMessage("Link must be a valid URL.");

        RuleFor(v => v.TaskItem.Description)
            .MaximumLength(200).WithMessage("TaskItem.Description must not exceed 200 characters.");

        RuleFor(v => v.TaskItem.TimeToBeSpent)
            .GreaterThan(0).WithMessage("TimeToBeSpent must be greater than 0.");

        RuleFor(v => v.TaskItem.TimeSpent)
            .GreaterThanOrEqualTo(0).WithMessage("TimeSpent cannot be negative.");

        RuleFor(v => v.TaskItem.Origin)
            .NotNull().WithMessage("Origin is required.");

        RuleFor(v => v.TaskItem.Type)
            .NotNull().WithMessage("Type is required.");

        RuleFor(v => v.TaskItem.StartDate)
            .NullOrMustBeValidDate().WithMessage("StartDate must be a valid date.");
            
        RuleFor(v => v.TaskItem.DueDate)
            .NullOrMustBeValidDate().GreaterThan(v => v.TaskItem.StartDate).When(v => v.TaskItem.StartDate.HasValue)
            .WithMessage("DueDate must be greater than StartDate.");
    }
}