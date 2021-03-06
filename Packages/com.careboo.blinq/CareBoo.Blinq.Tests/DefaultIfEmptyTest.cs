﻿using NUnit.Framework;
using Linq = System.Linq.Enumerable;
using static Utils;
using Unity.Collections;
using CareBoo.Blinq;

internal class DefaultIfEmptyTest
{
    [Test, Parallelizable, Timeout(5000)]
    public void BlinqShouldEqualLinqValueSequenceDefaultIfEmpty([ArrayValues] int[] sourceArr)
    {
        var sourceNativeArr = new NativeArray<int>(sourceArr, Allocator.Persistent);
        var source = sourceNativeArr.ToValueSequence();
        var expected = ExceptionAndValue(() => Linq.ToArray(Linq.DefaultIfEmpty(source, 1)));
        var actual = ExceptionAndValue(() => Linq.ToArray(source.DefaultIfEmpty(1)));
        AssertAreEqual(expected, actual);
        sourceNativeArr.Dispose();
    }

    [Test, Parallelizable, Timeout(5000)]
    public void BlinqShouldEqualLinqNativeArrayDefaultIfEmpty([ArrayValues] int[] sourceArr)
    {
        var source = new NativeArray<int>(sourceArr, Allocator.Persistent);
        var expected = ExceptionAndValue(() => Linq.ToArray(Linq.DefaultIfEmpty(source, 1)));
        var actual = ExceptionAndValue(() => Linq.ToArray(source.DefaultIfEmpty(1)));
        AssertAreEqual(expected, actual);
        source.Dispose();
    }
}
