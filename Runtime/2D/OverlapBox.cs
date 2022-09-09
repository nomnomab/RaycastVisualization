using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle) {
#if UNITY_EDITOR
      Collider2D collider = Physics2D.OverlapBox(point, size, angle);
      DrawBoxStationary(point, size, angle, collider);
      return collider;
#else
      return Physics2D.OverlapBox(point, size, angle);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapBox(
      Vector2 point,
      Vector2 size,
      float angle,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      Collider2D collider = Physics2D.OverlapBox(point, size, angle, layerMask);
      DrawBoxStationary(point, size, angle, collider, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapBox(point, size, angle, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapBox(
      Vector2 point,
      Vector2 size,
      float angle,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      Collider2D collider = Physics2D.OverlapBox(point, size, angle, layerMask, minDepth);
      DrawBoxStationary(point, size, angle, collider, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapBox(point, size, angle, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Checks if a Collider falls within a box area.</para>
    /// </summary>
    /// <param name="point">The center of the box.</param>
    /// <param name="size">The size of the box.</param>
    /// <param name="angle">The angle of the box.</param>
    /// <param name="layerMask">Filter to check objects only on specific layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than this value.</param>
    /// <returns>
    ///   <para>The Collider overlapping the box.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapBox(
      Vector2 point,
      Vector2 size,
      float angle,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      Collider2D collider = Physics2D.OverlapBox(point, size, angle, layerMask, minDepth, maxDepth);
      DrawBoxStationary(point, size, angle, collider, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapBox(point, size, angle, layerMask, minDepth, maxDepth);
#endif
    }

    /// <summary>
    ///   <para>Checks if a Collider falls within a box area.</para>
    /// </summary>
    /// <param name="point">The center of the box.</param>
    /// <param name="size">The size of the box.</param>
    /// <param name="angle">The angle of the box.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth.  Note that normal angle is not used for overlap testing.</param>
    /// <param name="results">The array to receive results.  The size of the array determines the maximum number of results that can be returned.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapBox(
      Vector2 point,
      Vector2 size,
      float angle,
      ContactFilter2D contactFilter,
      Collider2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapBox(point, size, angle, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawBoxStationary(point, size, angle, results[i], contactFilter);
      }

      if (length == 0) {
        DrawBoxStationary(point, size, angle, default, contactFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapBox(point, size, angle, contactFilter, results);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapBox(
      Vector2 point,
      Vector2 size,
      float angle,
      ContactFilter2D contactFilter,
      List<Collider2D> results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapBox(point, size, angle, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawBoxStationary(point, size, angle, results[i], contactFilter);
      }

      if (length == 0) {
        DrawBoxStationary(point, size, angle, default, contactFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapBox(point, size, angle, contactFilter, results);
#endif
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void DrawBoxStationary(Vector2 origin, Vector2 size, float angle, Collider2D collider, ContactFilter2D? contactFilter = default) {
#if UNITY_EDITOR
      size *= 0.5f;
      
      bool didHit = collider;
      Color color = VisualUtils.GetColor(didHit);
      
      Rectangle rectangle = new Rectangle {
        origin = origin,
        size = size,
        angle = angle
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
        origin = origin,
        filter = contactFilter.Value
      };
      filter.Draw(VisualUtils.GetDefaultColor());
#endif
    }
  }
}