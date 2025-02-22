using System;
using System.Diagnostics;
using Splunk.Metrics.Abstractions;

namespace Splunk.Metrics.Statsd
{
    public class TimingScope : IDisposable
    {
        private readonly IStatsPublisher statsd;
        private readonly string bucket;
        private readonly Stopwatch stopwatch = new Stopwatch();

        public TimingScope(IStatsPublisher statsd, string bucket)
        {
            this.statsd = statsd;
            this.bucket = bucket;
            stopwatch.Start();
        }

        public void Dispose()
        {
            stopwatch.Stop();
            statsd.Timing($"{bucket}.msecs".ToLowerInvariant(), (long)stopwatch.Elapsed.TotalMilliseconds);
        }
    }
}