#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapCircleAll(Vector2 point, float radius) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
      Collider2D[] array = Physics2D.OverlapCircleAll(point, radius);

      foreach (Collider2D collider in array) {
        DrawCircleStationary(point, radius, collider, legacyFilter);
      }

      if (array.Length == 0) {
        DrawCircleStationary(point, radius, default, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.OverlapCircleAll(point, radius);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapCircleAll(
      Vector2 point,
      float radius,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      Collider2D[] array = Physics2D.OverlapCircleAll(point, radius, layerMask);

      foreach (Collider2D collider in array) {
        DrawCircleStationary(point, radius, collider, legacyFilter);
      }

      if (array.Length == 0) {
        DrawCircleStationary(point, radius, default, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.OverlapCircleAll(point, radius, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapCircleAll(
      Vector2 point,
      float radius,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      Collider2D[] array = Physics2D.OverlapCircleAll(point, radius, layerMask, minDepth);

      foreach (Collider2D collider in array) {
        DrawCircleStationary(point, radius, collider, legacyFilter);
      }

      if (array.Length == 0) {
        DrawCircleStationary(point, radius, default, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.OverlapCircleAll(point, radius, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Get a list of all Colliders that fall within a circular area.</para>
    /// </summary>
    /// <param name="point">The center of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="layerMask">Filter to check objects only on specified layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>
    ///   <para>The cast results.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapCircleAll(
      Vector2 point,
      float radius,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      Collider2D[] array = Physics2D.OverlapCircleAll(point, radius, layerMask, minDepth, maxDepth);

      foreach (Collider2D collider in array) {
        DrawCircleStationary(point, radius, collider, legacyFilter);
      }

      if (array.Length == 0) {
        DrawCircleStationary(point, radius, default, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.OverlapCircleAll(point, radius, layerMask, minDepth, maxDepth);
#endif
    }
  }
}
#endif