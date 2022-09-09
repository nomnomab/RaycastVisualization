using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapCircle(Vector2 point, float radius) {
#if UNITY_EDITOR
      Collider2D collider = Physics2D.OverlapCircle(point, radius);
      DrawCircleStationary(point, radius, collider);
      return collider;
#else
      return Physics2D.OverlapCircle(point, radius);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      Collider2D collider = Physics2D.OverlapCircle(point, radius, layerMask);
      DrawCircleStationary(point, radius, collider, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapCircle(point, radius, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapCircle(
      Vector2 point,
      float radius,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      Collider2D collider = Physics2D.OverlapCircle(point, radius, layerMask, minDepth);
      DrawCircleStationary(point, radius, collider, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapCircle(point, radius, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Checks if a Collider falls within a circular area.</para>
    /// </summary>
    /// <param name="point">Centre of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="layerMask">Filter to check objects only on specific layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>
    ///   <para>The Collider overlapping the circle.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapCircle(
      Vector2 point,
      float radius,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      Collider2D collider = Physics2D.OverlapCircle(point, radius, layerMask, minDepth, maxDepth);
      DrawCircleStationary(point, radius, collider, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapCircle(point, radius, layerMask, minDepth, maxDepth);
#endif
    }

    /// <summary>
    ///   <para>Checks if a Collider is within a circular area.</para>
    /// </summary>
    /// <param name="point">Centre of the circle.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth.  Note that normal angle is not used for overlap testing.</param>
    /// <param name="results">The array to receive results.  The size of the array determines the maximum number of results that can be returned.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapCircle(
      Vector2 point,
      float radius,
      ContactFilter2D contactFilter,
      Collider2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapCircle(point, radius, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawCircleStationary(point, radius, results[i], contactFilter);
      }

      if (length == 0) {
        DrawCircleStationary(point, radius, default, contactFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapCircle(point, radius, contactFilter, results);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapCircle(
      Vector2 point,
      float radius,
      ContactFilter2D contactFilter,
      List<Collider2D> results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapCircle(point, radius, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawCircleStationary(point, radius, results[i], contactFilter);
      }

      if (length == 0) {
        DrawCircleStationary(point, radius, default, contactFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapCircle(point, radius, contactFilter, results);
#endif
    }

    internal static void DrawCircleStationary(Vector2 origin, float radius, Collider2D collider, ContactFilter2D? contactFilter = default) {
#if UNITY_EDITOR
      bool didHit = collider;
      Color color = VisualUtils.GetColor(didHit);
      
      Circle circle = Circle.Default;
      circle.origin = origin;
      circle.radius = radius;
      circle.Draw(color);

      if (didHit) {
        Circle circle2 = circle;
        circle2.origin.z = collider.transform.position.z;
        circle2.Draw(color * 0.5f);

        if (!contactFilter.HasValue) {
          Range range = new Range {
            from = circle2.origin,
            to = circle.origin
          };
          
          range.Draw(VisualUtils.GetDefaultColor());
        }
      }
      
      if (!contactFilter.HasValue) {
        return;
      }

      Filter2D filter = new Filter2D {
        origin = origin,
        filter = contactFilter.Value
      };

      filter.Draw(VisualUtils.GetDefaultColor());
#endif
    }
  }
}