﻿using Unity.Collections;
using Unity.Mathematics;
using CareBoo.Burst.Delegates;

namespace CareBoo.Blinq
{
    public static partial class Sequence
    {
        public static ValueSequence<TResult, ZipSequence<T, TSource, TSecondElement, TResult, TSecond, TResultSelector>> Zip<T, TSource, TSecondElement, TResult, TSecond, TResultSelector>(
            this ValueSequence<T, TSource> source,
            ValueSequence<TSecondElement, TSecond> second,
            ValueFunc<T, TSecondElement, TResult>.Struct<TResultSelector> resultSelector
            )
            where T : struct
            where TSource : struct, ISequence<T>
            where TSecondElement : struct
            where TResult : struct
            where TSecond : struct, ISequence<TSecondElement>
            where TResultSelector : struct, IFunc<T, TSecondElement, TResult>
        {
            var seq = new ZipSequence<T, TSource, TSecondElement, TResult, TSecond, TResultSelector> { Source = source.Source, Second = second.Source, ResultSelector = resultSelector };
            return ValueSequence<TResult>.New(seq);
        }
    }

    public struct ZipSequence<T, TSource, TSecondElement, TResult, TSecond, TResultSelector> : ISequence<TResult>
        where T : struct
        where TSource : struct, ISequence<T>
        where TSecondElement : struct
        where TResult : struct
        where TSecond : struct, ISequence<TSecondElement>
        where TResultSelector : struct, IFunc<T, TSecondElement, TResult>
    {
        public TSource Source;
        public TSecond Second;
        public ValueFunc<T, TSecondElement, TResult>.Struct<TResultSelector> ResultSelector;

        public NativeList<TResult> ToList()
        {
            using (var source = Source.ToList())
            using (var second = Second.ToList())
            {
                var length = math.min(source.Length, second.Length);
                var result = new NativeList<TResult>(length, Allocator.Temp);
                for (var i = 0; i < length; i++)
                {
                    result.AddNoResize(ResultSelector.Invoke(source[i], second[i]));
                }
                return result;
            }
        }
    }
}
