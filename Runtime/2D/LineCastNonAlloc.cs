#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
#if UNITY_2023_1_OR_NEWER
    [System.Obsolete("VisualPhysics2D.LinecastNonAlloc is deprecated. Use VisualPhysics2D.Linecast instead.")]
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.LinecastNonAlloc(start, end, results);

      for (int i = 0; i < length; i++) {
        DrawLine(start, end, results[i]);
      }

      if (length == 0) {
        DrawLine(start, end, default);
      }

      return length;
#else
      return Physics2D.LinecastNonAlloc(start, end, results);
#endif
    }

#if UNITY_2023_1_OR_NEWER
    [System.Obsolete("VisualPhysics2D.LinecastNonAlloc is deprecated. Use VisualPhysics2D.Linecast instead.")]
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LinecastNonAlloc(
      Vector2 start,
      Vector2 end,
      RaycastHit2D[] results,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      int length = Physics2D.LinecastNonAlloc(start, end, results, layerMask);

      for (int i = 0; i < length; i++) {
        DrawLine(start, end, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawLine(start, end, default, legacyFilter);
      }

      return length;
#else
      return Physics2D.LinecastNonAlloc(start, end, results, layerMask);
#endif
    }

#if UNITY_2023_1_OR_NEWER
    [System.Obsolete("VisualPhysics2D.LinecastNonAlloc is deprecated. Use VisualPhysics2D.Linecast instead.")]
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LinecastNonAlloc(
      Vector2 start,
      Vector2 end,
      RaycastHit2D[] results,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      int length = Physics2D.LinecastNonAlloc(start, end, results, layerMask, minDepth);

      for (int i = 0; i < length; i++) {
        DrawLine(start, end, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawLine(start, end, default, legacyFilter);
      }

      return length;
#else
      return Physics2D.LinecastNonAlloc(start, end, results, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Casts a line against Colliders in the Scene. Note: This method will be deprecated in a future build and it is recommended to use Linecast instead.</para>
    /// </summary>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <param name="start">The start point of the line in world space.</param>
    /// <param name="end">The end point of the line in world space.</param>
    /// <param name="results">Returned array of objects that intersect the line.</param>
    /// <param name="layerMask">Filter to detect Colliders only on certain layers.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
#if UNITY_2023_1_OR_NEWER
    [System.Obsolete("VisualPhysics2D.LinecastNonAlloc is deprecated. Use VisualPhysics2D.Linecast instead.")]
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int LinecastNonAlloc(
      Vector2 start,
      Vector2 end,
      RaycastHit2D[] results,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      int length = Physics2D.LinecastNonAlloc(start, end, results, layerMask, minDepth, maxDepth);

      for (int i = 0; i < length; i++) {
        DrawLine(start, end, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawLine(start, end, default, legacyFilter);
      }

      return length;
#else
      return Physics2D.LinecastNonAlloc(start, end, results, layerMask, minDepth, maxDepth);
#endif
    }
  }
}
#endif