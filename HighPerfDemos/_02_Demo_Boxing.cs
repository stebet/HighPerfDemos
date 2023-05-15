using BenchmarkDotNet.Attributes;

using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HighPerfDemos
{
    [ShortRunJob]
    [MemoryDiagnoser]
    public class _02_Demo_Boxing
    {
        private byte[] destination = new byte[4];
        private MyStructType myStructType = new MyStructType(Random.Shared.Next());

        [Benchmark(Baseline = true)]
        public void SerializeToBytesInterface()
        {
            SerializeUsingInterface(myStructType);
        }

        [Benchmark]
        public void SerializeToBytesGenerics()
        {
            SerializeUsingGenerics(myStructType);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SerializeUsingInterface(IMySerializer mySerializer)
        {
            mySerializer.SerializeToBytes(destination);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SerializeUsingGenerics<T>(T mySerializer) where T : IMySerializer
        {
            mySerializer.SerializeToBytes(destination);
        }
    }

    public interface IMySerializer
    {
        void SerializeToBytes(Span<byte> span);
    }

    struct MyStructType : IMySerializer
    {
        int value;
        
        public MyStructType(int value)
        {
            this.value = value;
        }

        public void SerializeToBytes(Span<byte> span)
        {
            BinaryPrimitives.WriteInt32BigEndian(span, this.value);
        }
    }
}
