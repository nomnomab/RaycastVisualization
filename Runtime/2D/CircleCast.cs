#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D CircleCast(
      Vector2 origin,
      float radius,
      Vector2 direction) {
#if UNITY_EDITOR
      RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction);
      DrawCircle(origin, radius, direction, Mathf.Infinity, in hit);
      return hit;
#else
      return Physics2D.CircleCast(origin, radius, direction);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D CircleCast(
      Vector2 origin,
      float radius,
      Vector2 direction,
      float distance) {
#if UNITY_EDITOR
      RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, distance);
      DrawCircle(origin, radius, direction, distance, in hit);
      return hit;
#else
      return Physics2D.CircleCast(origin, radius, direction, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D CircleCast(
      Vector2 origin,
      float radius,
      Vector2 direction,
      float distance,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter =
        CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);

      RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, distance, layerMask);
      DrawCircle(origin, radius, direction, distance, in hit, legacyFilter);
      return hit;
#else
      return Physics2D.CircleCast(origin, radius, direction, distance, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D CircleCast(
      Vector2 origin,
      float radius,
      Vector2 direction,
      float distance,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, distance, layerMask, minDepth);
      DrawCircle(origin, radius, direction, distance, in hit, legacyFilter);
      return hit;
#else
      return Physics2D.CircleCast(origin, radius, direction, distance, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Casts a circle against Colliders in the Scene, returning the first Collider to contact with it.</para>
    /// </summary>
    /// <param name="origin">The point in 2D space where the circle originates.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="direction">A vector representing the direction of the circle.</param>
    /// <param name="distance">The maximum distance over which to cast the circle.</param>
    /// <param name="layerMask">Filter to detect Colliders only on certain layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>
    ///   <para>The cast results returned.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D CircleCast(
      Vector2 origin,
      float radius,
      Vector2 direction,
      [DefaultValue("Mathf.Infinity")] float distance,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, distance, layerMask, minDepth,
        maxDepth);

      DrawCircle(origin, radius, direction, distance, in hit, legacyFilter);
      return hit;
#else
      return Physics2D.CircleCast(origin, radius, direction, distance, layerMask, minDepth, maxDepth);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CircleCast(
      Vector2 origin,
      float radius,
      Vector2 direction,
      ContactFilter2D contactFilter,
      RaycastHit2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.CircleCast(origin, radius, direction, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawCircle(origin, radius, direction, Mathf.Infinity, in results[i], contactFilter);
      }

      if (length == 0) {
        DrawCircle(origin, radius, direction, Mathf.Infinity, default, contactFilter);
      }

      return length;
#else
      return Physics2D.CircleCast(origin, radius, direction, contactFilter, results);
#endif
    }

    /// <summary>
    ///   <para>Casts a circle against Colliders in the Scene, returning all Colliders that contact with it.</para>
    /// </summary>
    /// <param name="origin">The point in 2D space where the circle originates.</param>
    /// <param name="radius">The radius of the circle.</param>
    /// <param name="direction">A vector representing the direction of the circle.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth, or normal angle.</param>
    /// <param name="results">The array to receive results.  The size of the array determines the maximum number of results that can be returned.</param>
    /// <param name="distance">The maximum distance over which to cast the circle.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CircleCast(
      Vector2 origin,
      float radius,
      Vector2 direction,
      ContactFilter2D contactFilter,
      RaycastHit2D[] results,
      [DefaultValue("Mathf.Infinity")] float distance) {
#if UNITY_EDITOR
      int length = Physics2D.CircleCast(origin, radius, direction, contactFilter, results, distance);

      for (int i = 0; i < length; i++) {
        DrawCircle(origin, radius, direction, distance, in results[i], contactFilter);
      }

      if (length == 0) {
        DrawCircle(origin, radius, direction, distance, default, contactFilter);
      }

      return length;
#else
      return Physics2D.CircleCast(origin, radius, direction, contactFilter, results, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CircleCast(
      Vector2 origin,
      float radius,
      Vector2 direction,
      ContactFilter2D contactFilter,
      List<RaycastHit2D> results,
      [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity) {
#if UNITY_EDITOR
      int length = Physics2D.CircleCast(origin, radius, direction, contactFilter, results, distance);

      for (int i = 0; i < length; i++) {
        DrawCircle(origin, radius, direction, distance, results[i], contactFilter);
      }

      if (length == 0) {
        DrawCircle(origin, radius, direction, distance, default, contactFilter);
      }

      return length;
#else
      return Physics2D.CircleCast(origin, radius, direction, contactFilter, results, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void DrawCircle(Vector2 origin, float radius, Vector2 direction, float distance, in RaycastHit2D hit, ContactFilter2D? contactFilter = default) {
#if UNITY_EDITOR
      direction.Normalize();
      bool didHit = hit.collider;
      Color color = VisualUtils.GetColor(didHit);
      float hitDistance = didHit ? hit.distance : distance;

      Circle circle = Circle.Default;
      circle.origin = origin + direction * hitDistance;
      circle.radius = radius;

      if (didHit) {
        float depth = hit.transform.position.z;
        
        Arrow arrow = Arrow.Default;
        arrow.origin = origin;
        arrow.direction = direction * VisualUtils.GetMaxRayLength(distance);
        arrow.Draw(VisualUtils.GetDefaultColor() * 0.5f);
        
        Circle circle2 = circle;
        Line line = new Line {
          from = origin,
          to = circle.origin
        };

        circle2.origin.z = depth;

        Line line2 = line;
        line2.from.z = depth;
        line2.to.z = depth;

        NormalCircle normalCircle = NormalCircle.Default;
        Vector3 circleOrigin = hit.point;

        circleOrigin.z = depth;
        normalCircle.origin = circleOrigin;
        normalCircle.upDirection = hit.normal;

        line.Draw(color);
        line2.Draw(color * 0.5f);
        normalCircle.Draw(color);
        circle2.Draw(color * 0.5f);
        
        Range range = new Range {
          from = line.from,
          to = line2.from
        };
        
        range.Draw(VisualUtils.GetDefaultColor());
      } else {
        Arrow arrow = Arrow.Default;
        arrow.origin = origin;
        arrow.direction = direction * VisualUtils.GetMaxRayLength(distance);
        arrow.Draw(color);
      }
      
      circle.Draw(color);
      
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