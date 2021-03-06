﻿using System;

internal static class Error
{
    internal static Exception ArgumentNull(string s) { return new ArgumentNullException(s); }

    internal static Exception ArgumentOutOfRange(string s) { return new ArgumentOutOfRangeException(s); }

    internal static Exception MoreThanOneElement() { return new InvalidOperationException("Sequence contains more than one element"); }

    internal static Exception MoreThanOneMatch() { return new InvalidOperationException("Sequence contains more than one matching element"); }

    internal static Exception NoElements() { return new InvalidOperationException("Sequence contains no elements"); }

    internal static Exception NoMatch() { return new InvalidOperationException("Sequence contains no matching element"); }

    internal static Exception NotSupported() { return new NotSupportedException(); }

    internal static Exception NotCodeGenerated() { return new NotImplementedException("Code generation for this method has failed!"); }

    internal static Exception NotImplemented() { return new NotImplementedException(); }

    internal static Exception DuplicateKey() { return new ArgumentException("keySelector produces duplicate keys for two elements."); }
}
