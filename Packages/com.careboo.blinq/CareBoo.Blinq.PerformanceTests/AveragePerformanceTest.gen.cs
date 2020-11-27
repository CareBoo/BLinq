﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Unity.PerformanceTesting;
using Unity.PerformanceTesting.Measurements;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using CareBoo.Burst.Delegates;
using System.Collections;
using NUnit.Framework;
using Linq = System.Linq.Enumerable;
using Blinq = CareBoo.Blinq.Sequence;

internal class AverageTest
{
    protected MethodMeasurement MakeMeasurement(string name, Action method)
    {
        return Measure.Method(method)
            .SampleGroup(name)
            .WarmupCount(100)
            .MeasurementCount(20)
            .IterationsPerMeasurement(20)
            .GC();
    }

    internal class SelectValues
    {
        public static IEnumerable Values
        {
            get
            {
                yield return new int[1];
                yield return Linq.ToArray(Linq.Repeat(1, 100000));
            }
        }
    }


    internal class Values_int
    {
        public static IEnumerable Values
        {
            get
            {
                yield return new int[1];
                yield return Linq.ToArray(Linq.Repeat((int)1, 100000));
            }
        }
    }

    internal struct StructIntTo_int : IFunc<int, int>
    {
        public int Invoke(int val) => (int)val;
    }

    internal static readonly ValueFunc<int, int>.Struct<StructIntTo_int> IntTo_int =
        ValueFunc<int, int>.New<StructIntTo_int>();

    [Test, Performance, Category("Performance")]
    public void Blinq_int(
        [ValueSource(typeof(Values_int), nameof(Values_int.Values))] int[] sourceArr
        )
    {
        var src = new NativeArray<int>(sourceArr, Allocator.Persistent);
        MakeMeasurement("Blinq_int", () => Blinq.RunAverage(src)).Run();
        src.Dispose();
    }

    [Test, Performance, Category("Performance")]
    public void Linq_int(
        [ValueSource(typeof(Values_int), nameof(Values_int.Values))] int[] sourceArr
        )
    {
        var src = new NativeArray<int>(sourceArr, Allocator.Persistent);
        MakeMeasurement("Linq_int", () => Linq.Average(src)).Run();
        src.Dispose();
    }

    [Test, Performance, Category("Performance")]
    public void BlinqSelect_int(
        [ValueSource(typeof(SelectValues), nameof(SelectValues.Values))] int[] sourceArr
        )
    {
        var src = new NativeArray<int>(sourceArr, Allocator.Persistent);
        MakeMeasurement("Blinq_int", () => Blinq.RunAverage(src, IntTo_int)).Run();
        src.Dispose();
    }

    [Test, Performance, Category("Performance")]
    public void LinqSelect_int(
        [ValueSource(typeof(SelectValues), nameof(SelectValues.Values))] int[] sourceArr
        )
    {
        var src = new NativeArray<int>(sourceArr, Allocator.Persistent);
        MakeMeasurement("Linq_int", () => Linq.Average(src, IntTo_int.Invoke)).Run();
        src.Dispose();
    }


    internal class Values_float
    {
        public static IEnumerable Values
        {
            get
            {
                yield return new float[1];
                yield return Linq.ToArray(Linq.Repeat((float)1, 100000));
            }
        }
    }

    internal struct StructIntTo_float : IFunc<int, float>
    {
        public float Invoke(int val) => (float)val;
    }

    internal static readonly ValueFunc<int, float>.Struct<StructIntTo_float> IntTo_float =
        ValueFunc<int, float>.New<StructIntTo_float>();

    [Test, Performance, Category("Performance")]
    public void Blinq_float(
        [ValueSource(typeof(Values_float), nameof(Values_float.Values))] float[] sourceArr
        )
    {
        var src = new NativeArray<float>(sourceArr, Allocator.Persistent);
        MakeMeasurement("Blinq_float", () => Blinq.RunAverage(src)).Run();
        src.Dispose();
    }

    [Test, Performance, Category("Performance")]
    public void Linq_float(
        [ValueSource(typeof(Values_float), nameof(Values_float.Values))] float[] sourceArr
        )
    {
        var src = new NativeArray<float>(sourceArr, Allocator.Persistent);
        MakeMeasurement("Linq_float", () => Linq.Average(src)).Run();
        src.Dispose();
    }

    [Test, Performance, Category("Performance")]
    public void BlinqSelect_float(
        [ValueSource(typeof(SelectValues), nameof(SelectValues.Values))] int[] sourceArr
        )
    {
        var src = new NativeArray<int>(sourceArr, Allocator.Persistent);
        MakeMeasurement("Blinq_float", () => Blinq.RunAverage(src, IntTo_float)).Run();
        src.Dispose();
    }

    [Test, Performance, Category("Performance")]
    public void LinqSelect_float(
        [ValueSource(typeof(SelectValues), nameof(SelectValues.Values))] int[] sourceArr
        )
    {
        var src = new NativeArray<int>(sourceArr, Allocator.Persistent);
        MakeMeasurement("Linq_float", () => Linq.Average(src, IntTo_float.Invoke)).Run();
        src.Dispose();
    }


    internal class Values_double
    {
        public static IEnumerable Values
        {
            get
            {
                yield return new double[1];
                yield return Linq.ToArray(Linq.Repeat((double)1, 100000));
            }
        }
    }

    internal struct StructIntTo_double : IFunc<int, double>
    {
        public double Invoke(int val) => (double)val;
    }

    internal static readonly ValueFunc<int, double>.Struct<StructIntTo_double> IntTo_double =
        ValueFunc<int, double>.New<StructIntTo_double>();

    [Test, Performance, Category("Performance")]
    public void Blinq_double(
        [ValueSource(typeof(Values_double), nameof(Values_double.Values))] double[] sourceArr
        )
    {
        var src = new NativeArray<double>(sourceArr, Allocator.Persistent);
        MakeMeasurement("Blinq_double", () => Blinq.RunAverage(src)).Run();
        src.Dispose();
    }

    [Test, Performance, Category("Performance")]
    public void Linq_double(
        [ValueSource(typeof(Values_double), nameof(Values_double.Values))] double[] sourceArr
        )
    {
        var src = new NativeArray<double>(sourceArr, Allocator.Persistent);
        MakeMeasurement("Linq_double", () => Linq.Average(src)).Run();
        src.Dispose();
    }

    [Test, Performance, Category("Performance")]
    public void BlinqSelect_double(
        [ValueSource(typeof(SelectValues), nameof(SelectValues.Values))] int[] sourceArr
        )
    {
        var src = new NativeArray<int>(sourceArr, Allocator.Persistent);
        MakeMeasurement("Blinq_double", () => Blinq.RunAverage(src, IntTo_double)).Run();
        src.Dispose();
    }

    [Test, Performance, Category("Performance")]
    public void LinqSelect_double(
        [ValueSource(typeof(SelectValues), nameof(SelectValues.Values))] int[] sourceArr
        )
    {
        var src = new NativeArray<int>(sourceArr, Allocator.Persistent);
        MakeMeasurement("Linq_double", () => Linq.Average(src, IntTo_double.Invoke)).Run();
        src.Dispose();
    }

}
