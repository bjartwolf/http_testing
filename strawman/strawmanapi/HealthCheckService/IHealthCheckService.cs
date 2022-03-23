using System.Threading;
using System.Threading.Tasks;

namespace strawmanapi.HealthCheckService
{
    public interface IHealthCheckService
    {
        public Task<HealthDto> GetHealth(CancellationToken ct);
    }
}
