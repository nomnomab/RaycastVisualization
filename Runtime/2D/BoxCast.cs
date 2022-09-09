using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D BoxCast(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction) {
#if UNITY_EDITOR
      RaycastHit2D hit = Physics2D.BoxCast(origin, size, angle, direction);
      DrawBox(origin, size, angle, direction, Mathf.Infinity, in hit);
      return hit;
#else
      return Physics2D.BoxCast(origin, size, angle, direction);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D BoxCast(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      float distance) {
#if UNITY_EDITOR
      RaycastHit2D hit = Physics2D.BoxCast(origin, size, angle, direction, distance);
      DrawBox(origin, size, angle, direction, distance, in hit);
      return hit;
#else
      return Physics2D.BoxCast(origin, size, angle, direction, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D BoxCast(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      float distance,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      RaycastHit2D hit = Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask);
      DrawBox(origin, size, angle, direction, distance, in hit, legacyFilter);
      return hit;
#else
      return Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D BoxCast(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      float distance,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      RaycastHit2D hit = Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask);
      DrawBox(origin, size, angle, direction, distance, in hit, legacyFilter);
      return hit;
#else
      return Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Casts a box against Colliders in the Scene, returning the first Collider to contact with it.</para>
    /// </summary>
    /// <param name="origin">The point in 2D space where the box originates.</param>
    /// <param name="size">The size of the box.</param>
    /// <param name="angle">The angle of the box (in degrees).</param>
    /// <param name="direction">A vector representing the direction of the box.</param>
    /// <param name="distance">The maximum distance over which to cast the box.</param>
    /// <param name="layerMask">Filter to detect Colliders only on certain layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>
    ///   <para>The cast results returned.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D BoxCast(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      [DefaultValue("Mathf.Infinity")] float distance,
      [DefaultValue("Physics2D.AllLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      RaycastHit2D hit = Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask);
      DrawBox(origin, size, angle, direction, distance, in hit, legacyFilter);
      return hit;
#else
      return Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask, minDepth, maxDepth);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int BoxCast(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      ContactFilter2D contactFilter,
      RaycastHit2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.BoxCast(origin, size, angle, direction, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawBox(origin, size, angle, direction, Mathf.Infinity, in results[i], contactFilter);
      }

      if (length == 0) {
        DrawBox(origin, size, angle, direction, Mathf.Infinity, default, contactFilter);
      }
      
      return length;
#else
      return Physics2D.BoxCast(origin, size, angle, direction, contactFilter, results);
#endif
    }

    /// <summary>
    ///   <para>Casts a box against the Colliders in the Scene and returns all Colliders that are in contact with it.</para>
    /// </summary>
    /// <param name="origin">The point in 2D space where the box originates.</param>
    /// <param name="size">The size of the box.</param>
    /// <param name="angle">The angle of the box (in degrees).</param>
    /// <param name="direction">A vector representing the direction of the box.</param>
    /// <param name="distance">The maximum distance over which to cast the box.</param>
    /// <param name="results">The array to receive results.  The size of the array determines the maximum number of results that can be returned.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth, or normal angle.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int BoxCast(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      ContactFilter2D contactFilter,
      RaycastHit2D[] results,
      [DefaultValue("Mathf.Infinity")] float distance) {
#if UNITY_EDITOR
      int length = Physics2D.BoxCast(origin, size, angle, direction, contactFilter, results, distance);

      for (int i = 0; i < length; i++) {
        DrawBox(origin, size, angle, direction, distance, in results[i], contactFilter);
      }

      if (length == 0) {
        DrawBox(origin, size, angle, direction, distance, default, contactFilter);
      }
      
      return length;
#else
      return Physics2D.BoxCast(origin, size, angle, direction, contactFilter, results, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int BoxCast(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      ContactFilter2D contactFilter,
      List<RaycastHit2D> results,
      [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity) {
#if UNITY_EDITOR
      int length = Physics2D.BoxCast(origin, size, angle, direction, contactFilter, results, distance);

      for (int i = 0; i < length; i++) {
        DrawBox(origin, size, angle, direction, distance, results[i], contactFilter);
      }

      if (length == 0) {
        DrawBox(origin, size, angle, direction, distance, default, contactFilter);
      }
      
      return length;
#else
      return Physics2D.BoxCast(origin, size, angle, direction, contactFilter, results, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void DrawBox(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, in RaycastHit2D hit, ContactFilter2D? contactFilter = default) {
#if UNITY_EDITOR
      size *= 0.5f;
      
      direction.Normalize();
      bool didHit = hit.collider;
      Color color = VisualUtils.GetColor(didHit);
      
      Rectangle rectangle = new Rectangle {
        origin = origin + direction * (didHit ? hit.distance : distance),
        size = size,
        angle = angle
      };

      if (didHit) {
        float depth = hit.transform.position.z;

        Arrow arrow = Arrow.Default;
        arrow.origin = origin;
        arrow.direction = direction * VisualUtils.GetMaxRayLength(distance);
        arrow.Draw(VisualUtils.GetDefaultColor() * 0.5f);
        
        Rectangle rectangle2 = rectangle;
        Line line = new Line {
          from = origin,
          to = rectangle.origin
        };
        
        rectangle2.origin.z = depth;
        
        Line line2 = line;
        line2.from.z = depth;
        line2.to.z = depth;

        NormalCircle circle = NormalCircle.Default;
        Vector3 circleOrigin = hit.point;
        
        circleOrigin.z = depth;
        circle.origin = circleOrigin;
        circle.upDirection = hit.normal;

        line.Draw(color);
        line2.Draw(color * 0.5f);

        Range range = new Range {
          from = line.from,
          to = line2.from
        };
        
        range.Draw(VisualUtils.GetDefaultColor());
        
        circle.Draw(color);
        rectangle2.Draw(color * 0.5f);
      } else {
        Arrow arrow = Arrow.Default;
        arrow.origin = origin;
        arrow.direction = direction * VisualUtils.GetMaxRayLength(distance);
        arrow.Draw(color);
      }

      rectangle.Draw(color);

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