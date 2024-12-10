﻿using BenchmarkDotNet.Attributes;

namespace Shared
{
    public class MyBenchmarks
    {
        [Benchmark]
        public void ExampleMethod()
        {
            var sum = 0;

            for (int i = 0; i < 1000; i++)
                sum += i;
        }
    }
}