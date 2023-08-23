using Grpc.Core;
using UngerApi.gRPC;

namespace UngerApi.gRPC.Services
{
    public class JobSchedulerService : JobScheduler.JobSchedulerBase
    {
        private readonly ILogger<JobSchedulerService> _logger;
        public JobSchedulerService(ILogger<JobSchedulerService> logger)
        {
            _logger = logger;
        }

        public override Task<JobResponse> ScheduleJob(JobRequest request, ServerCallContext context)
        {
            return Task.FromResult(new JobResponse
            {
                JobId = new Random().Next(1, 1000),
                Status = "Scheduled",
            });
        }
    }
}