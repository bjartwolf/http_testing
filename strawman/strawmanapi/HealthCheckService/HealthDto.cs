namespace strawmanapi.HealthCheckService
{
    public enum Status
    {
       Healthy,
       Failed
    }
    public class HealthDto
    {
        public Status HealthStatus { get; set; }
    }
}
