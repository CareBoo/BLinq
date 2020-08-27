﻿using Unity.Collections;
using Unity.Jobs;

namespace CareBoo.Blinq
{
    public static partial class NativeSequenceExtensions
    {
        public static double Average(this NativeSequence<double> source)
        {
            if (source.Length == 0) throw Error.NoElements();

            var output = new NativeArray<double>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
            var job = new SumJob_double { Input = source, Output = output };
            job.Schedule(source.Length, 64, source.DependsOn).Complete();
            var result = output[0] / source.Length;

            output.Dispose();
            return result;
        }

        public static double Average<TSource>(this NativeSequence<TSource> source, BurstCompiledFunc<TSource, double> selector)
            where TSource : struct
        {
            if (source.Length == 0) throw Error.NoElements();

            var output = new NativeArray<double>(1, Allocator.Persistent, NativeArrayOptions.ClearMemory);
            var job = new SumJob_double<TSource> { Input = source, Selector = selector, Output = output };
            job.Schedule(source.Length, 64, source.DependsOn).Complete();
            var result = output[0] / source.Length;

            output.Dispose();
            return result;
        }

        public struct SumJob_double : IJobParallelFor
        {
            [ReadOnly]
            [DeallocateOnJobCompletion]
            public NativeArray<double> Input;

            [NativeDisableParallelForRestriction]
            public NativeArray<double> Output;

            public void Execute(int index)
            {
                Output[0] += Input[index];
            }
        }

        public struct SumJob_double<TSource> : IJobParallelFor
            where TSource : struct
        {
            [ReadOnly]
            [DeallocateOnJobCompletion]
            public NativeArray<TSource> Input;

            [ReadOnly]
            public BurstCompiledFunc<TSource, double> Selector;

            [NativeDisableParallelForRestriction]
            public NativeArray<double> Output;

            public void Execute(int index)
            {
                Output[0] += Selector.Invoke(Input[index]);
            }
        }
    }
}
