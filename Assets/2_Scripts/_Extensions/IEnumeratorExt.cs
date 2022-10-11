using UnityEngine;
using System;
using System.Collections;

public static class IEnumeratorExt
{
    public static IEnumerator Then(this IEnumerator first, IEnumerator second)
    {
        yield return first;
        yield return second;
    }
    public static IEnumerator Then(this IEnumerator ienum, Action callback)
    {
        yield return ienum;
        callback?.Invoke();
    }
}