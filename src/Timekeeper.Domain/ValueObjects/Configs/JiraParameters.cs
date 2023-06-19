using Timekeeper.Domain.Common;

namespace Timekeeper.Domain.ValueObjects.Configs;

public class JiraParameters : ValueObject
{
    public string UserName { get; private set; } = default!;
    public string ApiKey { get; private set; } = default!;
    public string Instance { get; private set; } = default!;

    public JiraParameters(string userName, string apiKey, string instance)
    {
        UserName = userName;
        ApiKey   = apiKey;
        Instance = instance;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return UserName;
        yield return ApiKey;
        yield return Instance;
    }
}