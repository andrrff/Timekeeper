using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;

namespace Timekeeper.Infrastructure.ExternalServices.AzDevOps;

public class AzDevOpsClient
{
    private readonly string _project;
    private readonly string _organization;
    private readonly string _personalAccessToken;

    public AzDevOpsClient(string organization, string project, string personalAccessToken)
    {
        _organization        = organization;
        _project             = project;
        _personalAccessToken = personalAccessToken;
    }

    public string AzureDevOpsBaseUrl => $"https://dev.azure.com/{_organization}";

    public async Task GetWorkItemAsync(int workItemId)
    {
        Uri baseUrl              = new(AzureDevOpsBaseUrl);
        VssConnection connection = new(baseUrl, new VssBasicCredential(string.Empty, _personalAccessToken));

        var workItemClient = connection.GetClient<WorkItemTrackingHttpClient>();

        try
        {
            var workItem = await workItemClient.GetWorkItemAsync(workItemId);

            Console.WriteLine("Work Item Details:");
            Console.WriteLine(workItem.Fields["System.Title"]);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to retrieve work item. Error: {ex.Message}");
        }
    }

    public async Task GetCommitsAsync(string repositoryName)
    {
        Uri baseUrl              = new(AzureDevOpsBaseUrl);
        VssConnection connection = new(baseUrl, new VssBasicCredential(string.Empty, _personalAccessToken));

        var gitClient = connection.GetClient<GitHttpClient>();

        try
        {
            var commits = await gitClient.GetCommitsAsync(_project, repositoryName, new GitQueryCommitsCriteria());

            Console.WriteLine("Commits:");
            foreach (var commit in commits)
            {
                Console.WriteLine(commit.Comment);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to retrieve commits. Error: {ex.Message}");
        }
    }

    public async Task GetChangesetsAsync()
    {
        Uri baseUrl              = new(AzureDevOpsBaseUrl);
        VssConnection connection = new(baseUrl, new VssBasicCredential(string.Empty, _personalAccessToken));

        var gitClient = connection.GetClient<TfvcHttpClient>();

        try
        {
            var changesets = await gitClient.GetChangesetsAsync(_project);

            Console.WriteLine("Changesets:");
            foreach (var changeset in changesets)
            {
                Console.WriteLine(changeset.Comment);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to retrieve changesets. Error: {ex.Message}");
        }
    }

}