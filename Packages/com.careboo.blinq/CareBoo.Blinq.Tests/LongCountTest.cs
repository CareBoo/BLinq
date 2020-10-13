﻿using NUnit.Framework;
using Linq = System.Linq.Enumerable;
using static Utils;
using static ValueFuncs;
using Unity.Collections;
using Blinq = CareBoo.Blinq.Sequence;
using CareBoo.Blinq;

internal class LongCountTest
{
    [Test, Parallelizable]
    public void BlinqShouldEqualLinqNativeArraySequenceLongCount([ArrayValues] int[] sourceArr)
    {
        var sourceNativeArr = new NativeArray<int>(sourceArr, Allocator.Persistent);
        var expected = ExceptionAndValue(() => Linq.LongCount(sourceNativeArr));
        var actual = ExceptionAndValue(() => Blinq.LongCount(ref sourceNativeArr));
        AssertAreEqual(expected, actual);
        sourceNativeArr.Dispose();
    }

    [Test, Parallelizable]
    public void BlinqShouldEqualLinqValueSequenceLongCount([ArrayValues] int[] sourceArr)
    {
        var sourceNativeArr = new NativeArray<int>(sourceArr, Allocator.Persistent);
        var source = sourceNativeArr.ToValueSequence();
        var expected = ExceptionAndValue(() => Linq.LongCount(source));
        var actual = ExceptionAndValue(() => Blinq.LongCount(source));
        AssertAreEqual(expected, actual);
        sourceNativeArr.Dispose();
    }

    [Test, Parallelizable]
    public void BlinqShouldEqualLinqNativeArraySequenceLongCountPredicate([ArrayValues] int[] sourceArr)
    {
        var sourceNativeArr = new NativeArray<int>(sourceArr, Allocator.Persistent);
        var expected = ExceptionAndValue(() => Linq.LongCount(sourceNativeArr, EqualsOne.Invoke));
        var actual = ExceptionAndValue(() => Blinq.LongCount(ref sourceNativeArr, EqualsOne));
        AssertAreEqual(expected, actual);
        sourceNativeArr.Dispose();
    }

    [Test, Parallelizable]
    public void BlinqShouldEqualLinqValueSequenceLongCountPredicate([ArrayValues] int[] sourceArr)
    {
        var sourceNativeArr = new NativeArray<int>(sourceArr, Allocator.Persistent);
        var source = sourceNativeArr.ToValueSequence();
        var expected = ExceptionAndValue(() => Linq.LongCount(source, EqualsOne.Invoke));
        var actual = ExceptionAndValue(() => Blinq.LongCount(source, EqualsOne));
        AssertAreEqual(expected, actual);
        sourceNativeArr.Dispose();
    }
}
