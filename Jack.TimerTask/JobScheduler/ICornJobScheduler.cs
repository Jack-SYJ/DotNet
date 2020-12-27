using System.Threading;
using System.Threading.Tasks;

namespace Jack.TimerTask
{
    public interface ICornJobScheduler
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
