#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end) {
#if UNITY_EDITOR
      RaycastHit2D[] array = Physics2D.LinecastAll(start, end);
      ContactFilter2D legacyFilter = CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);

      foreach (RaycastHit2D hit in array) {
        DrawLine(start, end, hit, legacyFilter);
      }

      if (array.Length == 0) {
        DrawLine(start, end, default, legacyFilter);
      }

      return array;
#else
      return Physics2D.LinecastAll(start, end);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask) {
#if UNITY_EDITOR
      RaycastHit2D[] array = Physics2D.LinecastAll(start, end, layerMask);
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);

      foreach (RaycastHit2D hit in array) {
        DrawLine(start, end, hit, legacyFilter);
      }

      if (array.Length == 0) {
        DrawLine(start, end, default, legacyFilter);
      }

      return array;
#else
      return Physics2D.LinecastAll(start, end, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D[] LinecastAll(
      Vector2 start,
      Vector2 end,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      RaycastHit2D[] array = Physics2D.LinecastAll(start, end, layerMask, minDepth);
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);

      foreach (RaycastHit2D hit in array) {
        DrawLine(start, end, hit, legacyFilter);
      }

      if (array.Length == 0) {
        DrawLine(start, end, default, legacyFilter);
      }

      return array;
#else
      return Physics2D.LinecastAll(start, end, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Casts a line against Colliders in the Scene.</para>
    /// </summary>
    /// <param name="start">The start point of the line in world space.</param>
    /// <param name="end">The end point of the line in world space.</param>
    /// <param name="layerMask">Filter to detect Colliders only on certain layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>
    ///   <para>The cast results returned.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D[] LinecastAll(
      Vector2 start,
      Vector2 end,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      RaycastHit2D[] array = Physics2D.LinecastAll(start, end, layerMask, minDepth, maxDepth);
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);

      foreach (RaycastHit2D hit in array) {
        DrawLine(start, end, hit, legacyFilter);
      }

      if (array.Length == 0) {
        DrawLine(start, end, default, legacyFilter);
      }

      return array;
#else
      return Physics2D.LinecastAll(start, end, layerMask, minDepth, maxDepth);
#endif
    }
  }
}
#endif