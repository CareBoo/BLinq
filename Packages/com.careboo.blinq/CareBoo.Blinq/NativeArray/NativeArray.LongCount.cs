﻿using Unity.Collections;

namespace CareBoo.Blinq
{
    public static partial class Sequence
    {
        public static long LongCount<T, TPredicate>(
            this ref NativeArray<T> source,
            ValueFunc<T, bool>.Impl<TPredicate> predicate
            )
            where T : struct
            where TPredicate : struct, IFunc<T, bool>
        {
            var count = 0;
            for (var i = 0; i < source.Length; i++)
                if (predicate.Invoke(source[i]))
                    count++;
            return count;
        }

        public static long LongCount<T>(this ref NativeArray<T> source)
            where T : struct
        {
            return source.Length;
        }
    }
}
