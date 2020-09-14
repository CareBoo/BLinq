﻿using System;
using Unity.Collections;

namespace CareBoo.Blinq
{
    public static partial class Sequence
    {
        public static NativeHashMap<TKey, TElement> ToNativeHashMap<T, TKey, TElement, TKeySelector, TElementSelector>(
            this ref NativeArray<T> source,
            ValueFunc<T, TKey>.Impl<TKeySelector> keySelector,
            ValueFunc<T, TElement>.Impl<TElementSelector> elementSelector,
            Allocator allocator
            )
            where T : struct
            where TKey : struct, IEquatable<TKey>
            where TElement : struct
            where TKeySelector : struct, IFunc<T, TKey>
            where TElementSelector : struct, IFunc<T, TElement>
        {
            var result = new NativeHashMap<TKey, TElement>(source.Length, allocator);
            for (var i = 0; i < source.Length; i++)
            {
                var item = source[i];
                var key = keySelector.Invoke(item);
                var val = elementSelector.Invoke(item);
                result.Add(key, val);
            }
            return result;
        }

        public static NativeHashMap<TKey, T> ToNativeHashMap<T, TKey, TKeySelector>(
            this ref NativeArray<T> source,
            ValueFunc<T, TKey>.Impl<TKeySelector> keySelector,
            Allocator allocator
            )
            where T : struct
            where TKey : struct, IEquatable<TKey>
            where TKeySelector : struct, IFunc<T, TKey>
        {
            var result = new NativeHashMap<TKey, T>(source.Length, allocator);
            for (var i = 0; i < source.Length; i++)
            {
                var item = source[i];
                var key = keySelector.Invoke(item);
                result.Add(key, item);
            }
            return result;
        }
    }
}
