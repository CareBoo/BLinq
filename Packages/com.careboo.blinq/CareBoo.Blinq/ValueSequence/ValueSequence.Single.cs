﻿using System;
using Unity.Collections;
using CareBoo.Burst.Delegates;

namespace CareBoo.Blinq
{
    public static partial class Sequence
    {
        public static T Single<T, TSource, TPredicate>(
            this in ValueSequence<T, TSource> source,
            in ValueFunc<T, bool>.Struct<TPredicate> predicate
            )
            where T : unmanaged, IEquatable<T>
            where TSource : struct, ISequence<T>
            where TPredicate : struct, IFunc<T, bool>
        {
            var list = source.ToNativeList(Allocator.Temp);
            var set = new NativeHashSet<T>(list.Length, Allocator.Temp);
            for (var i = 0; i < list.Length; i++)
            {
                var val = list[i];
                if (predicate.Invoke(val) && !set.Add(val))
                {
                    set.Dispose();
                    list.Dispose();
                    throw Error.MoreThanOneMatch();
                }
            }
            var iter = set.GetEnumerator();
            if (iter.MoveNext())
            {
                var result = iter.Current;
                set.Dispose();
                list.Dispose();
                return result;
            }
            set.Dispose();
            list.Dispose();
            throw Error.NoMatch();
        }

        public static T SingleOrDefault<T, TSource, TPredicate>(
            this in ValueSequence<T, TSource> source,
            in ValueFunc<T, bool>.Struct<TPredicate> predicate,
            in T defaultVal = default
            )
            where T : unmanaged, IEquatable<T>
            where TSource : struct, ISequence<T>
            where TPredicate : struct, IFunc<T, bool>
        {
            var list = source.ToNativeList(Allocator.Temp);
            var set = new NativeHashSet<T>(list.Length, Allocator.Temp);
            for (var i = 0; i < list.Length; i++)
            {
                var val = list[i];
                if (predicate.Invoke(val) && !set.Add(val))
                {
                    set.Dispose();
                    list.Dispose();
                    throw Error.MoreThanOneMatch();
                }
            }
            var iter = set.GetEnumerator();
            var result = iter.MoveNext()
                ? iter.Current
                : defaultVal;
            set.Dispose();
            list.Dispose();
            return result;
        }

        public static T Single<T, TSource>(
            this in ValueSequence<T, TSource> source
            )
            where T : struct
            where TSource : struct, ISequence<T>
        {
            var list = source.ToNativeList(Allocator.Temp);
            if (list.Length > 1)
            {
                list.Dispose();
                throw Error.MoreThanOneElement();
            }
            if (list.Length == 0)
            {
                list.Dispose();
                throw Error.NoElements();
            }
            var result = list[0];
            list.Dispose();
            return result;
        }

        public static T SingleOrDefault<T, TSource>(
            this in ValueSequence<T, TSource> source,
            in T defaultVal = default
            )
            where T : struct
            where TSource : struct, ISequence<T>
        {
            var list = source.ToNativeList(Allocator.Temp);
            if (list.Length > 1)
            {
                list.Dispose();
                throw Error.MoreThanOneElement();
            }
            var result = list.Length == 0
                ? defaultVal
                : list[0];
            list.Dispose();
            return result;
        }
    }
}
