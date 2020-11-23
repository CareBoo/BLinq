﻿using System;
using Unity.Collections;
using Unity.Jobs;

namespace CareBoo.Blinq
{
    public static partial class Sequence
    {
        public struct ToNativeArrayJob<T, TSource> : IJob
            where T : struct
            where TSource : struct, ISequence<T>
        {
            [ReadOnly]
            ValueSequence<T, TSource> seq;

            [WriteOnly]
            NativeArray<T> output;

            public ToNativeArrayJob(ValueSequence<T, TSource> seq, NativeArray<T> output)
            {
                this.seq = seq;
                this.output = output;
            }

            public void Execute()
            {
                seq.ToNativeArray(ref output);
            }
        }

        public struct ToNativeArrayJobHandle<T>
            where T : struct
        {
            CollectionJobHandle<NativeList<T>> collectionJobHandle;
            Allocator allocator;

            public bool IsCompleted => collectionJobHandle.IsCompleted;

            public ToNativeArrayJobHandle(CollectionJobHandle<NativeList<T>> collectionJobHandle, Allocator allocator)
            {
                this.collectionJobHandle = collectionJobHandle;
                this.allocator = allocator;
            }

            public NativeArray<T> Complete()
            {
                var outputList = collectionJobHandle.Complete();
                var result = new NativeArray<T>(outputList, allocator);
                outputList.Dispose();
                return result;
            }
        }

        public static NativeArray<T> ToNativeArray<T, TSource>(
            this in ValueSequence<T, TSource> source,
            in Allocator allocator
            )
            where T : struct
            where TSource : struct, ISequence<T>
        {
            var list = source.ToNativeList(Allocator.Temp);
            var result = new NativeArray<T>(list, allocator);
            list.Dispose();
            return result;
        }

        public static NativeArray<T> RunToNativeArray<T, TSource>(
            this in ValueSequence<T, TSource> source,
            in Allocator allocator
            )
            where T : struct
            where TSource : struct, ISequence<T>
        {
            var outputList = source.RunToNativeList(Allocator.Persistent);
            var result = new NativeArray<T>(outputList, allocator);
            outputList.Dispose();
            return result;
        }

        public static ToNativeArrayJobHandle<T> ScheduleToNativeArray<T, TSource>(
            this in ValueSequence<T, TSource> source,
            in Allocator allocator
            )
            where T : struct
            where TSource : struct, ISequence<T>
        {
            var collectionJobHandle = source.ScheduleToNativeList(Allocator.Persistent);
            return new ToNativeArrayJobHandle<T>(collectionJobHandle, allocator);
        }

        public static NativeArray<T> ToNativeArray<T, TSource>(
            this in ValueSequence<T, TSource> source,
            ref NativeArray<T> output
            )
            where T : struct
            where TSource : struct, ISequence<T>
        {
            var list = source.ToNativeList(Allocator.Temp);
            NativeArray<T>.Copy(list, output);
            return output;
        }

        public static NativeArray<T> RunToNativeArray<T, TSource>(
            this in ValueSequence<T, TSource> source,
            ref NativeArray<T> output
            )
            where T : struct
            where TSource : struct, ISequence<T>
        {
            var list = source.RunToNativeList(Allocator.Persistent);
            NativeArray<T>.Copy(list, output);
            list.Dispose();
            return output;
        }

        public static JobHandle ScheduleToNativeArray<T, TSource>(
            this in ValueSequence<T, TSource> source,
            ref NativeArray<T> output
            )
            where T : struct
            where TSource : struct, ISequence<T>
        {
            return new ToNativeArrayJob<T, TSource>(source, output).Schedule();
        }
    }
}
