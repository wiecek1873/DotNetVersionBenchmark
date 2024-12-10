using BenchmarkDotNet.Running;
using Benchmarks;

namespace DotNetVersionBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(MyBenchmarks).Assembly);
        }
    }
}
