using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jack.TimerTask
{
    public class CornJobSchedulerHostedService : IHostedService
    {
        private readonly ICornJobScheduler _cornJobScheduler;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public CornJobSchedulerHostedService(ICornJobScheduler cornJobScheduler)
        {
            _cornJobScheduler = cornJobScheduler;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _cornJobScheduler.RunAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource.Cancel();

            return Task.CompletedTask;
        }
    }
}
