using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction) {
#if UNITY_EDITOR
      RaycastHit2D hit = Physics2D.defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity);
      DrawRaycast(origin, direction, in hit, Mathf.Infinity);
      return hit;
#else
      return Physics2D.Raycast(origin, direction);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D Raycast(
      Vector2 origin,
      Vector2 direction,
      float distance) {
#if UNITY_EDITOR
      RaycastHit2D hit = Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance);
      DrawRaycast(origin, direction, in hit, distance);
      return hit;
#else
      return Physics2D.Raycast(origin, direction, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D Raycast(
      Vector2 origin,
      Vector2 direction,
      float distance,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      RaycastHit2D hit = Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, legacyFilter);
      DrawRaycast(origin, direction, in hit, distance, legacyFilter);
      return hit;
#else
      return Physics2D.Raycast(origin, direction, distance, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D Raycast(
      Vector2 origin,
      Vector2 direction,
      float distance,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      RaycastHit2D hit = Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, legacyFilter);
      DrawRaycast(origin, direction, in hit, distance, legacyFilter);
      return hit;
#else
      return Physics2D.Raycast(origin, direction, distance, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Casts a ray against Colliders in the Scene.</para>
    /// </summary>
    /// <param name="origin">The point in 2D space where the ray originates.</param>
    /// <param name="direction">A vector representing the direction of the ray.</param>
    /// <param name="distance">The maximum distance over which to cast the ray.</param>
    /// <param name="layerMask">Filter to detect Colliders only on certain layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>
    ///   <para>The cast results returned.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D Raycast(
      Vector2 origin,
      Vector2 direction,
      [DefaultValue("Mathf.Infinity")] float distance,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
      
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      RaycastHit2D hit = Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, legacyFilter);
      DrawRaycast(origin, direction, in hit, distance, legacyFilter);
      return hit;
#else
      return Physics2D.Raycast(origin, direction, distance, layerMask, minDepth, maxDepth);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Raycast(
      Vector2 origin,
      Vector2 direction,
      ContactFilter2D contactFilter,
      RaycastHit2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, contactFilter, results);
      for (int i = 0; i < length; i++) {
        DrawRaycast(origin, direction, in results[i], Mathf.Infinity, contactFilter);
      }

      if (length == 0) {
        DrawRaycast(origin, direction, default, Mathf.Infinity, contactFilter);
      }
      
      return length;
#else
      return Physics2D.Raycast(origin, direction, contactFilter, results);
#endif
    }

    /// <summary>
    ///   <para>Casts a ray against Colliders in the Scene.</para>
    /// </summary>
    /// <param name="origin">The point in 2D space where the ray originates.</param>
    /// <param name="direction">A vector representing the direction of the ray.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth, or normal angle.</param>
    /// <param name="results">The array to receive results.  The size of the array determines the maximum number of results that can be returned.</param>
    /// <param name="distance">The maximum distance over which to cast the ray.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Raycast(
      Vector2 origin,
      Vector2 direction,
      ContactFilter2D contactFilter,
      RaycastHit2D[] results,
      [DefaultValue("Mathf.Infinity")] float distance) {
#if UNITY_EDITOR
      int length = Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
      for (int i = 0; i < length; i++) {
        DrawRaycast(origin, direction, in results[i], distance, contactFilter);
      }
      
      if (length == 0) {
        DrawRaycast(origin, direction, default, distance, contactFilter);
      }
      
      return length;
#else
      return Physics2D.Raycast(origin, direction, contactFilter, results, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Raycast(
      Vector2 origin,
      Vector2 direction,
      ContactFilter2D contactFilter,
      List<RaycastHit2D> results,
      [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity) {
#if UNITY_EDITOR
      int length = Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
      for (int i = 0; i < length; i++) {
        DrawRaycast(origin, direction, results[i], distance, contactFilter);
      }
      
      if (length == 0) {
        DrawRaycast(origin, direction, default, distance, contactFilter);
      }
      
      return length;
#else
      return Physics2D.Raycast(origin, direction, contactFilter, results, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void DrawRaycast(Vector2 origin, Vector2 direction, in RaycastHit2D hit, float distance, ContactFilter2D? contactFilter = default) {
      direction.Normalize();
      bool didHit = hit.collider;
      Color color = VisualUtils.GetColor(didHit);

      if (didHit) {
        float depth = hit.transform.position.z;
        
        Arrow arrow = Arrow.Default;
        arrow.origin = origin;
        arrow.direction = direction * VisualUtils.GetMaxRayLength(distance);
        arrow.Draw(VisualUtils.GetDefaultColor() * 0.5f);
        
        Line line = new Line {
          from = origin,
          to = hit.point
        };

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
      } else {
        Arrow arrow = Arrow.Default;
        arrow.origin = origin;
        arrow.direction = direction * VisualUtils.GetMaxRayLength(distance);

        arrow.Draw(color);
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