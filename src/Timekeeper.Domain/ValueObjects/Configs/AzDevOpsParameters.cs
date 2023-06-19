using Timekeeper.Domain.Common;

namespace Timekeeper.Domain.ValueObjects.Configs;

public class AzDevOpsParameters : ValueObject
{
    public string ApiKey { get; private set; } = default!;
    public string Instance { get; private set; } = default!;
    public string Project { get; private set; } = default!;
    public string Repository { get; private set; } = default!;

    public AzDevOpsParameters(string apiKey, string instance, string project, string repository)
    {
        ApiKey     = apiKey;
        Instance   = instance;
        Project    = project;
        Repository = repository;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ApiKey;
        yield return Instance;
        yield return Project;
        yield return Repository;
    }
}