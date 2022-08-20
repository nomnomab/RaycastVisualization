using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapPointNonAlloc(point, results);

      for (int i = 0; i < length; i++) {
        DrawPoint(results[i], point);
      }

      if (length == 0) {
        DrawPoint(default, point);
      }
      
      return length;
#else
      return Physics2D.OverlapPointNonAlloc(point, results);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      int length = Physics2D.OverlapPointNonAlloc(point, results, layerMask);

      for (int i = 0; i < length; i++) {
        DrawPoint(results[i], point, legacyFilter);
      }

      if (length == 0) {
        DrawPoint(default, point, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapPointNonAlloc(point, results, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapPointNonAlloc(
      Vector2 point,
      Collider2D[] results,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      int length = Physics2D.OverlapPointNonAlloc(point, results, layerMask, minDepth);

      for (int i = 0; i < length; i++) {
        DrawPoint(results[i], point, legacyFilter);
      }

      if (length == 0) {
        DrawPoint(default, point, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapPointNonAlloc(point, results, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Get a list of all Colliders that overlap a point in space. Note: This method will be deprecated in a future build and it is recommended to use OverlapPoint instead.</para>
    /// </summary>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <param name="point">A point in space.</param>
    /// <param name="results">Array to receive results.</param>
    /// <param name="layerMask">Filter to check objects only on specific layers.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapPointNonAlloc(
      Vector2 point,
      Collider2D[] results,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      int length = Physics2D.OverlapPointNonAlloc(point, results, layerMask, minDepth, maxDepth);

      for (int i = 0; i < length; i++) {
        DrawPoint(results[i], point, legacyFilter);
      }

      if (length == 0) {
        DrawPoint(default, point, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapPointNonAlloc(point, results, layerMask, minDepth, maxDepth);
#endif
    }
  }
}