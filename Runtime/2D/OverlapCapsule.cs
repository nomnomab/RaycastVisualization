using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapCapsule(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle) {
#if UNITY_EDITOR
      Collider2D collider = Physics2D.OverlapCapsule(point, size, direction, angle);
      DrawCapsuleStationary(point, size, direction, angle, collider);
      return collider;
#else
      return Physics2D.OverlapCapsule(point, size, direction, angle);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapCapsule(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      Collider2D collider = Physics2D.OverlapCapsule(point, size, direction, angle, layerMask);
      DrawCapsuleStationary(point, size, direction, angle, collider, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapCapsule(point, size, direction, angle, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapCapsule(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      Collider2D collider = Physics2D.OverlapCapsule(point, size, direction, angle, layerMask, minDepth);
      DrawCapsuleStationary(point, size, direction, angle, collider, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapCapsule(point, size, direction, angle, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Checks if a Collider falls within a capsule area.</para>
    /// </summary>
    /// <param name="point">The center of the capsule.</param>
    /// <param name="size">The size of the capsule.</param>
    /// <param name="direction">The direction of the capsule.</param>
    /// <param name="angle">The angle of the capsule.</param>
    /// <param name="layerMask">Filter to check objects only on specific layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than this value.</param>
    /// <returns>
    ///   <para>The Collider overlapping the capsule.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D OverlapCapsule(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      Collider2D collider = Physics2D.OverlapCapsule(point, size, direction, angle, layerMask, minDepth, maxDepth);
      DrawCapsuleStationary(point, size, direction, angle, collider, legacyFilter);
      return collider;
#else
      return Physics2D.OverlapCapsule(point, size, direction, angle, layerMask, minDepth, maxDepth);
#endif
    }

    /// <summary>
    ///   <para>Checks if a Collider falls within a capsule area.</para>
    /// </summary>
    /// <param name="point">The center of the capsule.</param>
    /// <param name="size">The size of the capsule.</param>
    /// <param name="direction">The direction of the capsule.</param>
    /// <param name="angle">The angle of the capsule.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth.  Note that normal angle is not used for overlap testing.</param>
    /// <param name="results">The array to receive results.  The size of the array determines the maximum number of results that can be returned.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapCapsule(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle,
      ContactFilter2D contactFilter,
      Collider2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapCapsule(point, size, direction, angle, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawCapsuleStationary(point, size, direction, angle, results[i], contactFilter);
      }

      if (length == 0) {
        DrawCapsuleStationary(point, size, direction, angle, default, contactFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapCapsule(point, size, direction, angle, contactFilter, results);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapCapsule(
      Vector2 point,
      Vector2 size,
      CapsuleDirection2D direction,
      float angle,
      ContactFilter2D contactFilter,
      List<Collider2D> results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapCapsule(point, size, direction, angle, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawCapsuleStationary(point, size, direction, angle, results[i], contactFilter);
      }

      if (length == 0) {
        DrawCapsuleStationary(point, size, direction, angle, default, contactFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapCapsule(point, size, direction, angle, contactFilter, results);
#endif
    }

    internal static void DrawCapsuleStationary(Vector2 origin, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D collider, ContactFilter2D? contactFilter = default) {
      bool didHit = collider;
      Color color = VisualUtils.GetColor(didHit);

      Vector2 dir = direction == CapsuleDirection2D.Horizontal ? Vector2.right : Vector2.up;

      if (direction == CapsuleDirection2D.Horizontal) {
        (size.x, size.y) = (size.y, size.x);
      }

      Vector2 from = origin + dir * (size.y * 0.5f - size.x * 0.5f);
      Vector2 to = origin - dir * (size.y * 0.5f - size.x * 0.5f);
      Stadium stadium = new Stadium {
        from = from,
        to = to,
        radius = size.x * 0.5f,
        angle = angle
      };
      stadium.Draw(color);

      if (didHit) {
        Stadium stadium2 = stadium;
        stadium2.from.z = collider.transform.position.z;
        stadium2.to.z = stadium2.from.z;
        stadium2.Draw(color * 0.5f);
        
        if (!contactFilter.HasValue) {
          Range range = new Range {
            from = (stadium.from + stadium.to) * 0.5f,
            to = (stadium2.from + stadium2.to) * 0.5f
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
    }
  }
}