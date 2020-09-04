﻿using NUnit.Framework;
using Unity.Collections;
using Blinq = CareBoo.Blinq.NativeArrayExtensions;
using Linq = System.Linq.Enumerable;
using static Utils;
using CareBoo.Blinq;
using static CareBoo.Blinq.NativeArrayExtensions;

internal class ZipTest
{
    [Test, Parallelizable]
    public void BlinqShouldEqualLinqNativeArrayZip([ArrayValues] int[] sourceArr, [ArrayValues] int[] secondArr)
    {
        var source = new NativeArray<int>(sourceArr, Allocator.Persistent);
        var second = new NativeArray<int>(secondArr, Allocator.Persistent);
        var secondSequence = second.ToValueSequence();
        var (expectedException, expectedValue) = ExceptionOrValue(() => Linq.ToArray(Linq.Zip(source, secondSequence, default(Sum).Invoke)));
        var (actualException, actualValue) = ExceptionOrValue(() => Linq.ToArray(Blinq.Zip<int, int, int, Sum, ValueSequence<int, NativeArraySourceQuery<int>>>(ref source, secondSequence)));
        Assert.AreEqual(expectedException, actualException);
        Assert.AreEqual(expectedValue, actualValue);
        source.Dispose();
        second.Dispose();
    }
}
