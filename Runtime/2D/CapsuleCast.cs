using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D CapsuleCast(
      Vector2 origin,
      Vector2 size,
      CapsuleDirection2D capsuleDirection,
      float angle,
      Vector2 direction) {
#if UNITY_EDITOR
      RaycastHit2D hit = Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction);
      DrawCapsule(origin, size, angle, capsuleDirection, direction, Mathf.Infinity, in hit);
      return hit;
#else
      return Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D CapsuleCast(
      Vector2 origin,
      Vector2 size,
      CapsuleDirection2D capsuleDirection,
      float angle,
      Vector2 direction,
      float distance) {
#if UNITY_EDITOR
      RaycastHit2D hit = Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance);
      DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, in hit);
      return hit;
#else
      return Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D CapsuleCast(
      Vector2 origin,
      Vector2 size,
      CapsuleDirection2D capsuleDirection,
      float angle,
      Vector2 direction,
      float distance,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      RaycastHit2D hit = Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, layerMask);
      DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, in hit, legacyFilter);
      return hit;
#else
      return Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D CapsuleCast(
      Vector2 origin,
      Vector2 size,
      CapsuleDirection2D capsuleDirection,
      float angle,
      Vector2 direction,
      float distance,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      RaycastHit2D hit = Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, layerMask,
        minDepth);

      DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, in hit, legacyFilter);
      return hit;
#else
      return Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Casts a capsule against Colliders in the Scene, returning the first Collider to contact with it.</para>
    /// </summary>
    /// <param name="origin">The point in 2D space where the capsule originates.</param>
    /// <param name="size">The size of the capsule.</param>
    /// <param name="capsuleDirection">The direction of the capsule.</param>
    /// <param name="angle">The angle of the capsule (in degrees).</param>
    /// <param name="direction">Vector representing the direction to cast the capsule.</param>
    /// <param name="distance">The maximum distance over which to cast the capsule.</param>
    /// <param name="layerMask">Filter to detect Colliders only on certain layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than this value.</param>
    /// <returns>
    ///   <para>The cast results returned.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D CapsuleCast(
      Vector2 origin,
      Vector2 size,
      CapsuleDirection2D capsuleDirection,
      float angle,
      Vector2 direction,
      [DefaultValue("Mathf.Infinity")] float distance,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      RaycastHit2D hit = Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, layerMask,
        minDepth, maxDepth);

      DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, in hit, legacyFilter);
      return hit;
#else
      return Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, layerMask, minDepth, maxDepth);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CapsuleCast(
      Vector2 origin,
      Vector2 size,
      CapsuleDirection2D capsuleDirection,
      float angle,
      Vector2 direction,
      ContactFilter2D contactFilter,
      RaycastHit2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, Mathf.Infinity, in results[i], contactFilter);
      }

      if (length == 0) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, Mathf.Infinity, default, contactFilter);
      }

      return length;
#else
      return Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, contactFilter, results);
#endif
    }

    /// <summary>
    ///   <para>Casts a capsule against the Colliders in the Scene and returns all Colliders that are in contact with it.</para>
    /// </summary>
    /// <param name="origin">The point in 2D space where the capsule originates.</param>
    /// <param name="size">The size of the capsule.</param>
    /// <param name="capsuleDirection">The direction of the capsule.</param>
    /// <param name="angle">The angle of the capsule (in degrees).</param>
    /// <param name="direction">Vector representing the direction to cast the capsule.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth, or normal angle.</param>
    /// <param name="results">The array to receive results.  The size of the array determines the maximum number of results that can be returned.</param>
    /// <param name="distance">The maximum distance over which to cast the capsule.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CapsuleCast(
      Vector2 origin,
      Vector2 size,
      CapsuleDirection2D capsuleDirection,
      float angle,
      Vector2 direction,
      ContactFilter2D contactFilter,
      RaycastHit2D[] results,
      [DefaultValue("Mathf.Infinity")] float distance) {
#if UNITY_EDITOR
      int length = Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, contactFilter, results,
        distance);

      for (int i = 0; i < length; i++) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, in results[i], contactFilter);
      }

      if (length == 0) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, default, contactFilter);
      }

      return length;
#else
      return Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, contactFilter, results, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CapsuleCast(
      Vector2 origin,
      Vector2 size,
      CapsuleDirection2D capsuleDirection,
      float angle,
      Vector2 direction,
      ContactFilter2D contactFilter,
      List<RaycastHit2D> results,
      [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity) {
#if UNITY_EDITOR
      int length = Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, contactFilter, results,
        distance);

      for (int i = 0; i < length; i++) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, results[i], contactFilter);
      }

      if (length == 0) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, default, contactFilter);
      }

      return length;
#else
      return Physics2D.CapsuleCast(origin, size, capsuleDirection, angle, direction, contactFilter, results, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void DrawCapsule(Vector2 origin, Vector2 size, float angle, CapsuleDirection2D capsuleDirection,
      Vector2 direction, float distance, in RaycastHit2D hit, ContactFilter2D? contactFilter = default) {
      direction.Normalize();
      bool didHit = hit.collider;
      Color color = VisualUtils.GetColor(didHit);

      Vector2 dir = capsuleDirection == CapsuleDirection2D.Horizontal ? Vector2.right : Vector2.up;
      float hitDistance = didHit ? hit.distance : distance;

      if (capsuleDirection == CapsuleDirection2D.Horizontal) {
        (size.x, size.y) = (size.y, size.x);
      }

      Vector2 from = origin + dir * (size.y * 0.5f - size.x * 0.5f) + direction * hitDistance;
      Vector2 to = origin - dir * (size.y * 0.5f - size.x * 0.5f) + direction * hitDistance;
      Stadium stadium = new Stadium {
        from = from,
        to = to,
        radius = size.x * 0.5f,
        angle = angle
      };

      if (didHit) {
        float depth = hit.transform.position.z;

        Arrow arrow = Arrow.Default;
        arrow.origin = origin;
        arrow.direction = direction * VisualUtils.GetMaxRayLength(distance);
        arrow.Draw(VisualUtils.GetDefaultColor() * 0.5f);
        
        Stadium stadium2 = stadium;
        Line line = new Line {
          from = origin,
          to = (stadium.from + stadium.to) * 0.5f
        };

        stadium2.from.z = depth;
        stadium2.to.z = depth;

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
        circle.Draw(color);
        stadium2.Draw(color * 0.5f);
        
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

      stadium.Draw(color);

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