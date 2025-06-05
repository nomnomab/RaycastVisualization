#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapPointAll(Vector2 point) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
      Collider2D[] array = Physics2D.OverlapPointAll(point);
      
      foreach (Collider2D collider in array) {
        DrawPoint(collider, point, legacyFilter);
      }

      if (array.Length == 0) {
        DrawPoint(default, point, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.OverlapPointAll(point);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      Collider2D[] array = Physics2D.OverlapPointAll(point, layerMask);
      
      foreach (Collider2D collider in array) {
        DrawPoint(collider, point, legacyFilter);
      }

      if (array.Length == 0) {
        DrawPoint(default, point, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.OverlapPointAll(point, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapPointAll(
      Vector2 point,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      Collider2D[] array = Physics2D.OverlapPointAll(point, layerMask, minDepth);
      
      foreach (Collider2D collider in array) {
        DrawPoint(collider, point, legacyFilter);
      }

      if (array.Length == 0) {
        DrawPoint(default, point, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.OverlapPointAll(point, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Get a list of all Colliders that overlap a point in space.</para>
    /// </summary>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <param name="point">A point in space.</param>
    /// <param name="layerMask">Filter to check objects only on specific layers.</param>
    /// <returns>
    ///   <para>The cast results returned.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapPointAll(
      Vector2 point,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      Collider2D[] array = Physics2D.OverlapPointAll(point, layerMask, minDepth, maxDepth);
      
      foreach (Collider2D collider in array) {
        DrawPoint(collider, point, legacyFilter);
      }

      if (array.Length == 0) {
        DrawPoint(default, point, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.OverlapPointAll(point, layerMask, minDepth, maxDepth);
#endif
    }
  }
}
#endif