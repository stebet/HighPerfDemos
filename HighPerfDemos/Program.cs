// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;

using HighPerfDemos;

BenchmarkSwitcher.FromAssembly(typeof(HighPerfDemos._01_Demo_UTF8Bytes).Assembly).Run();