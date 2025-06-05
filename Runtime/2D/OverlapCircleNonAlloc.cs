using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
#if UNITY_2023_1_OR_NEWER
    [System.Obsolete("VisualPhysics2D.OverlapCircleNonAlloc is deprecated. Use VisualPhysics2D.OverlapCircle instead.")]
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapCircleNonAlloc(point, radius, results);

      for (int i = 0; i < length; i++) {
        DrawCircleStationary(point, radius, results[i]);
      }

      if (length == 0) {
        DrawCircleStationary(point, radius, default);
      }
      
      return length;
#else
      return Physics2D.OverlapCircleNonAlloc(point, radius, results);
#endif
    }

#if UNITY_2023_1_OR_NEWER
    [System.Obsolete("VisualPhysics2D.OverlapCircleNonAlloc is deprecated. Use VisualPhysics2D.OverlapCircle instead.")]
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapCircleNonAlloc(
      Vector2 point,
      float radius,
      Collider2D[] results,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      int length = Physics2D.OverlapCircleNonAlloc(point, radius, results, layerMask);

      for (int i = 0; i < length; i++) {
        DrawCircleStationary(point, radius, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawCircleStationary(point, radius, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapCircleNonAlloc(point, radius, results, layerMask);
#endif
    }

#if UNITY_2023_1_OR_NEWER
    [System.Obsolete("VisualPhysics2D.OverlapCircleNonAlloc is deprecated. Use VisualPhysics2D.OverlapCircle instead.")]
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapCircleNonAlloc(
      Vector2 point,
      float radius,
      Collider2D[] results,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      int length = Physics2D.OverlapCircleNonAlloc(point, radius, results, layerMask, minDepth);

      for (int i = 0; i < length; i++) {
        DrawCircleStationary(point, radius, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawCircleStationary(point, radius, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapCircleNonAlloc(point, radius, results, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Get a list of all Colliders that fall within a circular area. Note: This method will be deprecated in a future build and it is recommended to use OverlapCircle instead.</para>
    /// </summary>
    /// <param name="point">The center of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="results">Array to receive results.</param>
    /// <param name="layerMask">Filter to check objects only on specific layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
#if UNITY_2023_1_OR_NEWER
    [System.Obsolete("VisualPhysics2D.OverlapCircleNonAlloc is deprecated. Use VisualPhysics2D.OverlapCircle instead.")]
#endif
    public static int OverlapCircleNonAlloc(
      Vector2 point,
      float radius,
      Collider2D[] results,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      int length = Physics2D.OverlapCircleNonAlloc(point, radius, results, layerMask, minDepth, maxDepth);

      for (int i = 0; i < length; i++) {
        DrawCircleStationary(point, radius, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawCircleStationary(point, radius, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapCircleNonAlloc(point, radius, results, layerMask, minDepth, maxDepth);
#endif
    }
  }
}