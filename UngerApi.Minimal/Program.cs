
using Grpc.Net.Client;
using GrpcJobSchedulerClient;

namespace UngerApi.Minimal;

public class GrpcClient
{
    private readonly JobScheduler.JobSchedulerClient _jobClient;

    public GrpcClient(GrpcChannel channel)
    {
        _jobClient = new JobScheduler.JobSchedulerClient(channel);
    }

    public async Task<JobResponse> CreateJobAsync(JobRequest request)
    {
        return await _jobClient.ScheduleJobAsync(request);
    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton(GrpcChannel.ForAddress("http://jobworker-service"));
        builder.Services.AddSingleton<GrpcClient>();

        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        app.MapGet("/hello/{name}", (string name) => $"Hello {name}!");

        app.MapPost("/createjob", async (JobRequest request) =>
        {
            var client = app.Services.GetRequiredService<GrpcClient>();
            var job = await client.CreateJobAsync(request);
            return Results.Json(job);
        });

        app.Run();

    }
}