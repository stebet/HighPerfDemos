using BenchmarkDotNet.Attributes;

using System.Buffers;
using System.Text;

namespace HighPerfDemos
{
    [ShortRunJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net70)]
    [MemoryDiagnoser]
    public class _01_Demo_UTF8Bytes
    {
        private string _randomString = new string('X', 256);

        [Benchmark(Baseline = true)]
        public void UTF8Plain()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(_randomString);
        }

        [Benchmark]
        public void UTF8UsingArrayPool()
        {
            byte[] destination = ArrayPool<byte>.Shared.Rent(256);
            Encoding.UTF8.GetBytes(_randomString, destination);
            ArrayPool<byte>.Shared.Return(destination);
        }

        [Benchmark]
        public void UTF8UsingStackalloc()
        {
            Span<byte> destination = stackalloc byte[256];
            Encoding.UTF8.GetBytes(_randomString, destination);
        }
    }
}
