#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
#if UNITY_2023_1_OR_NEWER
    [System.Obsolete("VisualPhysics2D.BoxCastNonAlloc is deprecated. Use VisualPhysics2D.BoxCast instead.")]
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int BoxCastNonAlloc(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      RaycastHit2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results);

      for (int i = 0; i < length; i++) {
        DrawBox(origin, size, angle, direction, Mathf.Infinity, in results[i]);
      }

      if (length == 0) {
        DrawBox(origin, size, angle, direction, Mathf.Infinity, default);
      }
      
      return length;
#else
      return Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results);
#endif
    }

#if UNITY_2023_1_OR_NEWER
    [System.Obsolete("VisualPhysics2D.BoxCastNonAlloc is deprecated. Use VisualPhysics2D.BoxCast instead.")]
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int BoxCastNonAlloc(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      RaycastHit2D[] results,
      float distance) {
#if UNITY_EDITOR
      int length = Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance);

      for (int i = 0; i < length; i++) {
        DrawBox(origin, size, angle, direction, distance, in results[i]);
      }

      if (length == 0) {
        DrawBox(origin, size, angle, direction, distance, default);
      }
      
      return length;
#else
      return Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance);
#endif
    }

#if UNITY_2023_1_OR_NEWER
    [System.Obsolete("VisualPhysics2D.BoxCastNonAlloc is deprecated. Use VisualPhysics2D.BoxCast instead.")]
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int BoxCastNonAlloc(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      RaycastHit2D[] results,
      float distance,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      int length = Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask);

      for (int i = 0; i < length; i++) {
        DrawBox(origin, size, angle, direction, distance, in results[i], legacyFilter);
      }

      if (length == 0) {
        DrawBox(origin, size, angle, direction, distance, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask);
#endif
    }

#if UNITY_2023_1_OR_NEWER
    [System.Obsolete("VisualPhysics2D.BoxCastNonAlloc is deprecated. Use VisualPhysics2D.BoxCast instead.")]
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int BoxCastNonAlloc(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      RaycastHit2D[] results,
      float distance,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      int length = Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, minDepth);

      for (int i = 0; i < length; i++) {
        DrawBox(origin, size, angle, direction, distance, in results[i], legacyFilter);
      }

      if (length == 0) {
        DrawBox(origin, size, angle, direction, distance, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Casts a box into the Scene, returning Colliders that contact with it into the provided results array. Note: This method will be deprecated in a future build and it is recommended to use BoxCast instead.</para>
    /// </summary>
    /// <param name="origin">The point in 2D space where the box originates.</param>
    /// <param name="size">The size of the box.</param>
    /// <param name="angle">The angle of the box (in degrees).</param>
    /// <param name="direction">A vector representing the direction of the box.</param>
    /// <param name="results">Array to receive results.</param>
    /// <param name="distance">The maximum distance over which to cast the box.</param>
    /// <param name="layerMask">Filter to detect Colliders only on certain layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
#if UNITY_2023_1_OR_NEWER
    [System.Obsolete("VisualPhysics2D.BoxCastNonAlloc is deprecated. Use VisualPhysics2D.BoxCast instead.")]
#endif
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int BoxCastNonAlloc(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      RaycastHit2D[] results,
      [DefaultValue("Mathf.Infinity")] float distance,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      int length = Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, minDepth, maxDepth);

      for (int i = 0; i < length; i++) {
        DrawBox(origin, size, angle, direction, distance, in results[i], legacyFilter);
      }

      if (length == 0) {
        DrawBox(origin, size, angle, direction, distance, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.BoxCastNonAlloc(origin, size, angle, direction, results, distance, layerMask, minDepth, maxDepth);
#endif
    }
  }
}
#endif