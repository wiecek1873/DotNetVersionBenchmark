using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#if NETFRAMEWORK
using Newtonsoft.Json;
#else
using System.Text.Json;
#endif
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

[SimpleJob(RuntimeMoniker.Net461)]
[SimpleJob(RuntimeMoniker.Net462)]
[SimpleJob(RuntimeMoniker.Net47)]
[SimpleJob(RuntimeMoniker.Net471)]
[SimpleJob(RuntimeMoniker.Net472)]
[SimpleJob(RuntimeMoniker.Net48)]
[SimpleJob(RuntimeMoniker.Net481)]
[SimpleJob(RuntimeMoniker.Net50)]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
public class MyBenchmarks
{
    private int[] numbers = Enumerable.Range(1, 1000).OrderByDescending(x => x).ToArray();
    private readonly List<int> data = Enumerable.Range(1, 1000).ToList();
    private readonly string input = new string('a', 1000) + "pattern";
    private readonly Dictionary<int, int> dictionary = Enumerable.Range(1, 1000).ToDictionary(x => x, x => x);

    private const string FilePath = "test.txt";

    [Benchmark]
    public string StringConcatenation()
    {
        string result = "";
        for (int i = 0; i < 1000; i++)
        {
            result += i.ToString();
        }
        return result;
    }

    [Benchmark]
    public string StringBuilderPerformance()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < 1000; i++)
        {
            sb.Append(i.ToString());
        }
        return sb.ToString();
    }

    [Benchmark]
    public int LinqSum()
    {
        return Enumerable.Range(1, 1000).Sum();
    }

    [Benchmark]
    public int ForLoopSum()
    {
        int sum = 0;
        for (int i = 1; i <= 1000; i++)
        {
            sum += i;
        }
        return sum;
    }

    [Benchmark]
    public int[] ArraySort()
    {
        Array.Sort(numbers);
        return numbers;
    }

    [Benchmark]
    public int[] LinqOrderBy()
    {
        return numbers.OrderBy(x => x).ToArray();
    }

    [Benchmark]
    public string JsonSerialize()
    {
#if NETFRAMEWORK
        return Newtonsoft.Json.JsonConvert.SerializeObject(data);
#else
        return JsonSerializer.Serialize(data);
#endif
    }

    [Benchmark]
    public int[] AllocateLargeArray()
    {
        return new int[100000];
    }

    [Benchmark]
    public void ParallelForEach()
    {
        Parallel.ForEach(Enumerable.Range(1, 1000), i => Math.Sqrt(i));
    }

    [Benchmark]
    public void SequentialForEach()
    {
        foreach (var i in Enumerable.Range(1, 1000))
        {
            Math.Sqrt(i);
        }
    }

    [Benchmark]
    public bool RegexMatch()
    {
        var regex = new Regex("pattern");
        return regex.IsMatch(input);
    }

    [Benchmark]
    public void FileWriteAndRead()
    {
        File.WriteAllText(FilePath, new string('a', 100000));
        _ = File.ReadAllText(FilePath);
        File.Delete(FilePath);
    }

    [Benchmark]
    public bool DictionaryLookup()
    {
        return dictionary.ContainsKey(500);
    }
}
