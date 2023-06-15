using Timekeeper.Application.Common.Models;

namespace Timekeeper.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<(Result Result, string UserId)> AuthenticateAsync(string userName, string password);

    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);
}