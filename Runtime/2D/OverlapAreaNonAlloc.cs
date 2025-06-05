#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.OverlapAreaNonAlloc(pointA, pointB, results);

      for (int i = 0; i < length; i++) {
        DrawArea(pointA, pointB, results[i]);
      }

      if (length == 0) {
        DrawArea(pointA, pointB, default);
      }
      
      return length;
#else
      return Physics2D.OverlapAreaNonAlloc(pointA, pointB, results);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapAreaNonAlloc(
      Vector2 pointA,
      Vector2 pointB,
      Collider2D[] results,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      int length = Physics2D.OverlapAreaNonAlloc(pointA, pointB, results, layerMask);

      for (int i = 0; i < length; i++) {
        DrawArea(pointA, pointB, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawArea(pointA, pointB, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapAreaNonAlloc(pointA, pointB, results, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapAreaNonAlloc(
      Vector2 pointA,
      Vector2 pointB,
      Collider2D[] results,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      int length = Physics2D.OverlapAreaNonAlloc(pointA, pointB, results, layerMask, minDepth);

      for (int i = 0; i < length; i++) {
        DrawArea(pointA, pointB, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawArea(pointA, pointB, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapAreaNonAlloc(pointA, pointB, results, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Get a list of all Colliders that fall within a specified area. Note: This method will be deprecated in a future build and it is recommended to use OverlapArea instead.</para>
    /// </summary>
    /// <param name="pointA">One corner of the rectangle.</param>
    /// <param name="pointB">Diagonally opposite the point A corner of the rectangle.</param>
    /// <param name="results">Array to receive results.</param>
    /// <param name="layerMask">Filter to check objects only on specified layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapAreaNonAlloc(
      Vector2 pointA,
      Vector2 pointB,
      Collider2D[] results,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      int length = Physics2D.OverlapAreaNonAlloc(pointA, pointB, results, layerMask, minDepth, maxDepth);

      for (int i = 0; i < length; i++) {
        DrawArea(pointA, pointB, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawArea(pointA, pointB, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.OverlapAreaNonAlloc(pointA, pointB, results, layerMask, minDepth, maxDepth);
#endif
    }
  }
}
#endif