﻿namespace CareBoo.Blinq
{
    public interface IFunc<TResult>
        where TResult : struct
    {
        TResult Invoke();
    }

    public interface IFunc<T, TResult>
        where TResult : struct
    {
        TResult Invoke(T arg0);
    }

    public interface IFunc<T, U, TResult>
        where TResult : struct
    {
        TResult Invoke(T arg0, U arg1);
    }

    public interface IFunc<T, U, V, TResult>
        where TResult : struct
    {
        TResult Invoke(T arg0, U arg1, V arg2);
    }

    public interface IFunc<T, U, V, W, TResult>
        where TResult : struct
    {
        TResult Invoke(T arg0, U arg1, V arg2, W arg3);
    }
}