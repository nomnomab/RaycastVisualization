using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB) {
#if UNITY_EDITOR
      Collider2D collider = Physics2D.OverlapArea(pointA, pointB);
      DrawArea(pointA, pointB, collider);
      return collider;
#else
      return Physics2D.OverlapArea(pointA, pointB);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      Collider2D collider = Physics2D.OverlapArea(pointA, pointB, layerMask);
      DrawArea(pointA, pointB, collider, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapArea(pointA, pointB, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapArea(
      Vector2 pointA,
      Vector2 pointB,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      Collider2D collider = Physics2D.OverlapArea(pointA, pointB, layerMask, minDepth);
      DrawArea(pointA, pointB, collider, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapArea(pointA, pointB, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Checks if a Collider falls within a rectangular area.</para>
    /// </summary>
    /// <param name="pointA">One corner of the rectangle.</param>
    /// <param name="pointB">Diagonally opposite the point A corner of the rectangle.</param>
    /// <param name="layerMask">Filter to check objects only on specific layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>
    ///   <para>The Collider overlapping the area.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapArea(
      Vector2 pointA,
      Vector2 pointB,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      Collider2D collider = Physics2D.OverlapArea(pointA, pointB, layerMask, minDepth, maxDepth);
      DrawArea(pointA, pointB, collider, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapArea(pointA, pointB, layerMask, minDepth, maxDepth);
#endif
    }

    /// <summary>
    ///   <para>Checks if a Collider falls within a rectangular area.</para>
    /// </summary>
    /// <param name="pointA">One corner of the rectangle.</param>
    /// <param name="pointB">Diagonally opposite the point A corner of the rectangle.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth.  Note that normal angle is not used for overlap testing.</param>
    /// <param name="results">The array to receive results.  The size of the array determines the maximum number of results that can be returned.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapArea(
      Vector2 pointA,
      Vector2 pointB,
      ContactFilter2D contactFilter,
      Collider2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapArea(pointA, pointB, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawArea(pointA, pointB, results[i], contactFilter);
      }

      if (length == 0) {
        DrawArea(pointA, pointB, default, contactFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapArea(pointA, pointB, contactFilter, results);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapArea(
      Vector2 pointA,
      Vector2 pointB,
      ContactFilter2D contactFilter,
      List<Collider2D> results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapArea(pointA, pointB, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawArea(pointA, pointB, results[i], contactFilter);
      }

      if (length == 0) {
        DrawArea(pointA, pointB, default, contactFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapArea(pointA, pointB, contactFilter, results);
#endif
    }

    internal static void DrawArea(Vector2 a, Vector2 b, Collider2D collider, ContactFilter2D? contactFilter = default) {
#if UNITY_EDITOR
      bool didHit = collider;
      Color color = VisualUtils.GetColor(didHit);

      Rect rect = Rect.MinMaxRect(a.x, a.y, b.x, b.y);
      Rectangle rectangle = new Rectangle {
        origin = rect.center,
        size = rect.size * 0.5f,
        angle = 0
      };
      rectangle.Draw(color);

      if (didHit) {
        Rectangle rectangle2 = rectangle;
        rectangle2.origin.z = collider.transform.position.z;
        rectangle2.Draw(color * 0.5f);

        if (!contactFilter.HasValue) {
          Range range = new Range {
            from = rectangle2.origin,
            to = rectangle.origin
          };
          
          range.Draw(VisualUtils.GetDefaultColor());
        }
      }
      
      if (!contactFilter.HasValue) {
        return;
      }

      Filter2D filter = new Filter2D {
        origin = rect.center,
        filter = contactFilter.Value
      };

      filter.Draw(VisualUtils.GetDefaultColor());
#endif
    }
  }
}