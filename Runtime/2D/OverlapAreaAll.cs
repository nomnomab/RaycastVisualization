#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB) {
#if UNITY_EDITOR
      Collider2D[] array = Physics2D.OverlapAreaAll(pointA, pointB);

      foreach (Collider2D collider in array) {
        DrawArea(pointA, pointB, collider);
      }

      if (array.Length == 0) {
        DrawArea(pointA, pointB, default);
      }

      return array;
#else
      return Physics2D.OverlapAreaAll(pointA, pointB);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapAreaAll(
      Vector2 pointA,
      Vector2 pointB,
      int layerMask) {
#if UNITY_EDITOR
      Collider2D[] array = Physics2D.OverlapAreaAll(pointA, pointB, layerMask);

      foreach (Collider2D collider in array) {
        DrawArea(pointA, pointB, collider);
      }

      if (array.Length == 0) {
        DrawArea(pointA, pointB, default);
      }

      return array;
#else
      return Physics2D.OverlapAreaAll(pointA, pointB, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapAreaAll(
      Vector2 pointA,
      Vector2 pointB,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      Collider2D[] array = Physics2D.OverlapAreaAll(pointA, pointB, layerMask, minDepth);

      foreach (Collider2D collider in array) {
        DrawArea(pointA, pointB, collider);
      }

      if (array.Length == 0) {
        DrawArea(pointA, pointB, default);
      }

      return array;
#else
      return Physics2D.OverlapAreaAll(pointA, pointB, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Get a list of all Colliders that fall within a rectangular area.</para>
    /// </summary>
    /// <param name="pointA">One corner of the rectangle.</param>
    /// <param name="pointB">Diagonally opposite the point A corner of the rectangle.</param>
    /// <param name="layerMask">Filter to check objects only on specific layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>
    ///   <para>The cast results returned.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Collider2D[] OverlapAreaAll(
      Vector2 pointA,
      Vector2 pointB,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      Collider2D[] array = Physics2D.OverlapAreaAll(pointA, pointB, layerMask, minDepth, maxDepth);

      foreach (Collider2D collider in array) {
        DrawArea(pointA, pointB, collider);
      }

      if (array.Length == 0) {
        DrawArea(pointA, pointB, default);
      }

      return array;
#else
      return Physics2D.OverlapAreaAll(pointA, pointB, layerMask, minDepth, maxDepth);
#endif
    }
  }
}
#endif