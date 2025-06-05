#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapCapsuleNonAlloc(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle,
      Collider2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapCapsuleNonAlloc(point, size, direction, angle, results);

      for (int i = 0; i < length; i++) {
        DrawCapsuleStationary(point, size, direction, angle, results[i]);
      }

      if (length == 0) {
        DrawCapsuleStationary(point, size, direction, angle, default);
      }
      
      return length;
#else
      return Physics2D.OverlapCapsuleNonAlloc(point, size, direction, angle, results);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapCapsuleNonAlloc(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle,
      Collider2D[] results,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      int length = Physics2D.OverlapCapsuleNonAlloc(point, size, direction, angle, results, layerMask);

      for (int i = 0; i < length; i++) {
        DrawCapsuleStationary(point, size, direction, angle, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawCapsuleStationary(point, size, direction, angle, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapCapsuleNonAlloc(point, size, direction, angle, results, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapCapsuleNonAlloc(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle,
      Collider2D[] results,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      int length = Physics2D.OverlapCapsuleNonAlloc(point, size, direction, angle, results, layerMask, minDepth);

      for (int i = 0; i < length; i++) {
        DrawCapsuleStationary(point, size, direction, angle, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawCapsuleStationary(point, size, direction, angle, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapCapsuleNonAlloc(point, size, direction, angle, results, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Get a list of all Colliders that fall within a capsule area. Note: This method will be deprecated in a future build and it is recommended to use OverlapCapsule instead.</para>
    /// </summary>
    /// <param name="point">The center of the capsule.</param>
    /// <param name="size">The size of the capsule.</param>
    /// <param name="direction">The direction of the capsule.</param>
    /// <param name="angle">The angle of the capsule.</param>
    /// <param name="results">Array to receive results.</param>
    /// <param name="layerMask">Filter to check objects only on specific layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than this value.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapCapsuleNonAlloc(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle,
      Collider2D[] results,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      int length = Physics2D.OverlapCapsuleNonAlloc(point, size, direction, angle, results, layerMask, minDepth, maxDepth);

      for (int i = 0; i < length; i++) {
        DrawCapsuleStationary(point, size, direction, angle, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawCapsuleStationary(point, size, direction, angle, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapCapsuleNonAlloc(point, size, direction, angle, results, layerMask, minDepth, maxDepth);
#endif
    }
  }
}
#endif