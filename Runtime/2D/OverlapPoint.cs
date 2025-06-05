#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapPoint(Vector2 point) {
#if UNITY_EDITOR
      Collider2D collider = Physics2D.OverlapPoint(point);
      DrawPoint(collider, point);
      return collider;
#else
      return Physics2D.OverlapPoint(point);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapPoint(Vector2 point, int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      Collider2D collider = Physics2D.OverlapPoint(point, layerMask);
      DrawPoint(collider, point, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapPoint(point, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapPoint(Vector2 point, int layerMask, float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      Collider2D collider = Physics2D.OverlapPoint(point, layerMask, minDepth);
      DrawPoint(collider, point, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapPoint(point, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Checks if a Collider overlaps a point in space.</para>
    /// </summary>
    /// <param name="point">A point in world space.</param>
    /// <param name="layerMask">Filter to check objects only on specific layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>
    ///   <para>The Collider overlapping the point.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapPoint(
      Vector2 point,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      Collider2D collider = Physics2D.OverlapPoint(point, layerMask, minDepth, maxDepth);
      DrawPoint(collider, point, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapPoint(point, layerMask, minDepth, maxDepth);
#endif
    }

    /// <summary>
    ///   <para>Checks if a Collider overlaps a point in world space.</para>
    /// </summary>
    /// <param name="point">A point in world space.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth.  Note that normal angle is not used for overlap testing.</param>
    /// <param name="results">The array to receive results.  The size of the array determines the maximum number of results that can be returned.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapPoint(
      Vector2 point,
      ContactFilter2D contactFilter,
      Collider2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapPoint(point, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawPoint(results[i], point, contactFilter);
      }

      if (length == 0) {
        DrawPoint(default, point, contactFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapPoint(point, contactFilter, results);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapPoint(
      Vector2 point,
      ContactFilter2D contactFilter,
      List<Collider2D> results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapPoint(point, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawPoint(results[i], point, contactFilter);
      }

      if (length == 0) {
        DrawPoint(default, point, contactFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapPoint(point, contactFilter, results);
#endif
    }

    internal static void DrawPoint(Collider2D collider2D, Vector2 origin, ContactFilter2D? contactFilter = default) {
#if UNITY_EDITOR
      bool didHit = collider2D;
      Color color = VisualUtils.GetColor(didHit);
      
      NormalCircle normalCircle = NormalCircle.Default;
      normalCircle.origin = origin;
      normalCircle.upDirection = Vector3.back;
      
      normalCircle.Draw(color);

      if (didHit) {
        Line line = new Line {
          from = normalCircle.origin
        };
        
        normalCircle.origin.z = collider2D.transform.position.z;
        normalCircle.Draw(color);

        line.to = normalCircle.origin;
        line.Draw(VisualUtils.GetDefaultColor());
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
#endif