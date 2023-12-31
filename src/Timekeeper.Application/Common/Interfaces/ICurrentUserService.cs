namespace Timekeeper.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }

    void SetUserId(string userId);
}