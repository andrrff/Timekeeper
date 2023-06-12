using Timekeeper.Application.Common.Interfaces;

namespace Timekeeper.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}