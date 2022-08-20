using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D[] BoxCastAll(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
      RaycastHit2D[] array = Physics2D.BoxCastAll(origin, size, angle, direction);

      foreach (RaycastHit2D hit in array) {
        DrawBox(origin, size, angle, direction, Mathf.Infinity, in hit, legacyFilter);
      }

      if (array.Length == 0) {
        DrawBox(origin, size, angle, direction, Mathf.Infinity, default, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.BoxCastAll(origin, size, angle, direction);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D[] BoxCastAll(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      float distance) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
      RaycastHit2D[] array = Physics2D.BoxCastAll(origin, size, angle, direction, distance);

      foreach (RaycastHit2D hit in array) {
        DrawBox(origin, size, angle, direction, distance, in hit, legacyFilter);
      }

      if (array.Length == 0) {
        DrawBox(origin, size, angle, direction, distance, default, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.BoxCastAll(origin, size, angle, direction, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D[] BoxCastAll(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      float distance,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      RaycastHit2D[] array = Physics2D.BoxCastAll(origin, size, angle, direction, distance, layerMask);

      foreach (RaycastHit2D hit in array) {
        DrawBox(origin, size, angle, direction, distance, in hit, legacyFilter);
      }

      if (array.Length == 0) {
        DrawBox(origin, size, angle, direction, distance, default, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.BoxCastAll(origin, size, angle, direction, distance, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D[] BoxCastAll(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      float distance,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      RaycastHit2D[] array = Physics2D.BoxCastAll(origin, size, angle, direction, distance, layerMask, minDepth);

      foreach (RaycastHit2D hit in array) {
        DrawBox(origin, size, angle, direction, distance, in hit, legacyFilter);
      }

      if (array.Length == 0) {
        DrawBox(origin, size, angle, direction, distance, default, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.BoxCastAll(origin, size, angle, direction, distance, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Casts a box against Colliders in the Scene, returning all Colliders that contact with it.</para>
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
    public static RaycastHit2D[] BoxCastAll(
      Vector2 origin,
      Vector2 size,
      float angle,
      Vector2 direction,
      [DefaultValue("Mathf.Infinity")] float distance,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      RaycastHit2D[] array = Physics2D.BoxCastAll(origin, size, angle, direction, distance, layerMask, minDepth, maxDepth);

      foreach (RaycastHit2D hit in array) {
        DrawBox(origin, size, angle, direction, distance, in hit, legacyFilter);
      }

      if (array.Length == 0) {
        DrawBox(origin, size, angle, direction, distance, default, legacyFilter);
      }
      
      return array;
#else
      return Physics2D.BoxCastAll(origin, size, angle, direction, distance, layerMask, minDepth, maxDepth);
#endif
    }
  }
}