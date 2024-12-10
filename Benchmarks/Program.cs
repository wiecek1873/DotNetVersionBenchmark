// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;

namespace Benchmarks
{
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(MyBenchmarks).Assembly);
        }
    }
}

