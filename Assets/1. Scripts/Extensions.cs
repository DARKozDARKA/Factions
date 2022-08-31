using System.Reflection;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static T With<T>(this T self, Action<T> set)
    {
        set.Invoke(self);
        return self;
    }

    public static T With<T>(this T self, Action<T> apply, Func<bool> when)
    {
        if (when())
            apply?.Invoke(self);

        return self;
    }

    public static T With<T>(this T self, Action<T> apply, bool when)
    {
        if (when)
            apply?.Invoke(self);

        return self;
    }

    public static List<T> Join<T>(this List<T> first, List<T> second)
    {
        if (first == null)
        {
            return second;
        }
        if (second == null)
        {
            return first;
        }

        return first.Concat(second).ToList();
    }

    public static bool InBounds<T>(this IEnumerable<T> enumerable, int index)
    {
        if (index < 0 || index >= enumerable.Count())
            return false;

        return true;
    }

    public static bool In2DArrayBounds(this object[,] array, int x, int y)
    {
        if (x < array.GetLowerBound(0) ||
            x > array.GetUpperBound(0) ||
            y < array.GetLowerBound(1) ||
            y > array.GetUpperBound(1)) return false;
        return true;
    }

    public static bool In2DArrayBounds(this object[,] array, Vector2Int index)
    {
        if (index.x < array.GetLowerBound(0) ||
            index.x > array.GetUpperBound(0) ||
            index.y < array.GetLowerBound(1) ||
            index.y > array.GetUpperBound(1)) return false;
        return true;
    }

    public static bool In3DArrayBounds(this object[,,] array, Vector3Int index)
    {
        if (index.x < array.GetLowerBound(0) ||
            index.x > array.GetUpperBound(0) ||
            index.y < array.GetLowerBound(1) ||
            index.y > array.GetUpperBound(1) ||
            index.z < array.GetLowerBound(2) ||
            index.z > array.GetUpperBound(2)) return false;
        return true;
    }

    public static bool In3DArrayBounds(this object[,,] array, int x, int y, int z)
    {
        if (x < array.GetLowerBound(0) ||
            x > array.GetUpperBound(0) ||
            y < array.GetLowerBound(1) ||
            y > array.GetUpperBound(1) ||
            z < array.GetLowerBound(2) ||
            z > array.GetUpperBound(2)) return false;
        return true;
    }
}
