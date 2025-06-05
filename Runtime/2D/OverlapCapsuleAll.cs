#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapCapsuleAll(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle)
    {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
      Collider2D[] array = Physics2D.OverlapCapsuleAll(point, size, direction, angle);

      foreach (Collider2D collider in array) {
        DrawCapsuleStationary(point, size, direction, angle, collider, legacyFilter);
      }

      if (array.Length == 0) {
        DrawCapsuleStationary(point, size, direction, angle, default, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.OverlapCapsuleAll(point, size, direction, angle);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapCapsuleAll(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle,
      int layerMask)
    {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      Collider2D[] array = Physics2D.OverlapCapsuleAll(point, size, direction, angle, layerMask);

      foreach (Collider2D collider in array) {
        DrawCapsuleStationary(point, size, direction, angle, collider, legacyFilter);
      }

      if (array.Length == 0) {
        DrawCapsuleStationary(point, size, direction, angle, default, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.OverlapCapsuleAll(point, size, direction, angle, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapCapsuleAll(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle,
      int layerMask,
      float minDepth)
    {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      Collider2D[] array = Physics2D.OverlapCapsuleAll(point, size, direction, angle, layerMask, minDepth);

      foreach (Collider2D collider in array) {
        DrawCapsuleStationary(point, size, direction, angle, collider, legacyFilter);
      }

      if (array.Length == 0) {
        DrawCapsuleStationary(point, size, direction, angle, default, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.OverlapCapsuleAll(point, size, direction, angle, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Get a list of all Colliders that fall within a capsule area.</para>
    /// </summary>
    /// <param name="point">The center of the capsule.</param>
    /// <param name="size">The size of the capsule.</param>
    /// <param name="direction">The direction of the capsule.</param>
    /// <param name="angle">The angle of the capsule.</param>
    /// <param name="layerMask">Filter to check objects only on specific layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than this value.</param>
    /// <returns>
    ///   <para>The cast results returned.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapCapsuleAll(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth)
    {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      Collider2D[] array = Physics2D.OverlapCapsuleAll(point, size, direction, angle, layerMask, minDepth, maxDepth);

      foreach (Collider2D collider in array) {
        DrawCapsuleStationary(point, size, direction, angle, collider, legacyFilter);
      }

      if (array.Length == 0) {
        DrawCapsuleStationary(point, size, direction, angle, default, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.OverlapCapsuleAll(point, size, direction, angle, layerMask, minDepth, maxDepth);
#endif
    }
  }
}
#endif