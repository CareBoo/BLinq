﻿using System;
using CareBoo.Burst.Delegates;

namespace CareBoo.Blinq
{
    public struct RightSelector<T, TResult>
        : IFunc<T, TResult, TResult>
        where T : struct
        where TResult : struct
    {
        public TResult Invoke(T arg0, TResult arg1)
        {
            return arg1;
        }
    }

    public struct LeftSelector<T, TResult>
        : IFunc<T, TResult, T>
        where T : struct
        where TResult : struct
    {
        public T Invoke(T arg0, TResult arg1)
        {
            return arg0;
        }
    }

    public struct SameSelector<T>
        : IFunc<T, T>
        where T : struct
    {
        public T Invoke(T arg0) => arg0;
    }


    public struct GroupSelector<TKey, T> : IFunc<TKey, ValueGroupingValues<TKey, T>, ValueGrouping<TKey, T>>
        where T : struct
        where TKey : struct, IEquatable<TKey>
    {
        public ValueGrouping<TKey, T> Invoke(TKey arg0, ValueGroupingValues<TKey, T> arg1)
        {
            return new ValueGrouping<TKey, T>(arg0, arg1);
        }
    }

    public static class UtilFunctions
    {
        public static ValueFunc<T, TResult, TResult>.Struct<RightSelector<T, TResult>> RightSelector<T, TResult>()
            where T : struct
            where TResult : struct
        {
            return default;
        }

        public static ValueFunc<T, TResult, T>.Struct<LeftSelector<T, TResult>> LeftSelector<T, TResult>()
            where T : struct
            where TResult : struct
        {
            return default;
        }

        public static ValueFunc<T, T>.Struct<SameSelector<T>> SameSelector<T>()
            where T : struct
        {
            return default;
        }

        public static ValueFunc<TKey, ValueGroupingValues<TKey, T>, ValueGrouping<TKey, T>>.Struct<GroupSelector<TKey, T>> GroupSelector<TKey, T>()
            where T : struct
            where TKey : struct, IEquatable<TKey>
        {
            return default;
        }
    }
}
