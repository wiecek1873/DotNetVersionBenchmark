# DotNetVersionBenchmark

Run benchmark:
`dotnet run -c Release`

Check build for .NET Framework and .NET before running benchmark to avoid build error during benchmarks.
Build for .NET Framework:
`dotnet build --framework net461`

Build for .NET:
`dotnet build --framework net5.0`

Run benchmarks:
`dotnet run -c Release --framework net5.0`
