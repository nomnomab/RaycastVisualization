using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapBoxNonAlloc(
      Vector2 point,
      Vector2 size,
      float angle,
      Collider2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapBoxNonAlloc(point, size, angle, results);

      for (int i = 0; i < length; i++) {
        DrawBoxStationary(point, size, angle, results[i]);
      }

      if (length == 0) {
        DrawBoxStationary(point, size, angle, default);
      }
      
      return length;
#else
      return Physics2D.OverlapBoxNonAlloc(point, size, angle, results);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapBoxNonAlloc(
      Vector2 point,
      Vector2 size,
      float angle,
      Collider2D[] results,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      int length = Physics2D.OverlapBoxNonAlloc(point, size, angle, results, layerMask);

      for (int i = 0; i < length; i++) {
        DrawBoxStationary(point, size, angle, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawBoxStationary(point, size, angle, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapBoxNonAlloc(point, size, angle, results, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapBoxNonAlloc(
      Vector2 point,
      Vector2 size,
      float angle,
      Collider2D[] results,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      int length = Physics2D.OverlapBoxNonAlloc(point, size, angle, results, layerMask, minDepth);

      for (int i = 0; i < length; i++) {
        DrawBoxStationary(point, size, angle, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawBoxStationary(point, size, angle, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapBoxNonAlloc(point, size, angle, results, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Get a list of all Colliders that fall within a box area. Note: This method will be deprecated in a future build and it is recommended to use OverlapBox instead.</para>
    /// </summary>
    /// <param name="point">The center of the box.</param>
    /// <param name="size">The size of the box.</param>
    /// <param name="angle">The angle of the box.</param>
    /// <param name="results">Array to receive results.</param>
    /// <param name="layerMask">Filter to check objects only on specific layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than this value.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapBoxNonAlloc(
      Vector2 point,
      Vector2 size,
      float angle,
      Collider2D[] results,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      int length = Physics2D.OverlapBoxNonAlloc(point, size, angle, results, layerMask, minDepth, maxDepth);

      for (int i = 0; i < length; i++) {
        DrawBoxStationary(point, size, angle, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawBoxStationary(point, size, angle, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapBoxNonAlloc(point, size, angle, results, layerMask, minDepth, maxDepth);
#endif
    }
  }
}