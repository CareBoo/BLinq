﻿using System;
using System.Collections;
using Unity.Collections;

namespace CareBoo.Blinq
{
    public static partial class Sequence
    {
        public static ValueSequence<T, ConcatSequence<T, TSource, TSecond>> Concat<T, TSource, TSecond>(
            this in ValueSequence<T, TSource> source,
            ValueSequence<T, TSecond> second
            )
            where T : struct
            where TSource : struct, ISequence<T>
            where TSecond : struct, ISequence<T>
        {
            var sourceSeq = source.GetEnumerator();
            var secondSeq = second.GetEnumerator();
            var newSequence = new ConcatSequence<T, TSource, TSecond>(ref sourceSeq, ref secondSeq);
            return ValueSequence<T>.New(ref newSequence);
        }

        public static ValueSequence<T, ConcatSequence<T, TSource, NativeArraySequence<T>>> Concat<T, TSource>(
            this in ValueSequence<T, TSource> source,
            NativeArray<T> second
            )
            where T : struct
            where TSource : struct, ISequence<T>
        {
            var sourceSeq = source.GetEnumerator();
            var secondSeq = second.ToValueSequence().GetEnumerator();
            var newSequence = new ConcatSequence<T, TSource, NativeArraySequence<T>>(ref sourceSeq, ref secondSeq);
            return ValueSequence<T>.New(ref newSequence);
        }
    }

    public struct ConcatSequence<T, TSource, TSecond> : ISequence<T>
        where T : struct
        where TSource : struct, ISequence<T>
        where TSecond : struct, ISequence<T>
    {
        TSource source;
        TSecond second;

        bool currentIndex;

        public ConcatSequence(ref TSource source, ref TSecond second)
        {
            this.source = source;
            this.second = second;
            currentIndex = false;
        }

        public T Current => currentIndex
            ? second.Current
            : source.Current;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            source.Dispose();
            second.Dispose();
        }

        public bool MoveNext()
        {
            if (!source.MoveNext())
            {
                currentIndex = true;
                return second.MoveNext();
            }
            return true;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }

        public NativeList<T> ToList()
        {
            var sourceList = source.ToList();
            var secondList = second.ToList();
            sourceList.AddRange(secondList);
            secondList.Dispose();
            return sourceList;
        }
    }
}
