#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CapsuleCastNonAlloc(
      Vector2 origin,
      Vector2 size,
      CapsuleDirection2D capsuleDirection,
      float angle,
      Vector2 direction,
      RaycastHit2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results);

      for (int i = 0; i < length; i++) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, Mathf.Infinity, results[i]);
      }

      if (length == 0) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, Mathf.Infinity, default);
      }
      
      return length;
#else
      return Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CapsuleCastNonAlloc(
      Vector2 origin,
      Vector2 size,
      CapsuleDirection2D capsuleDirection,
      float angle,
      Vector2 direction,
      RaycastHit2D[] results,
      float distance) {
#if UNITY_EDITOR
      int length = Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results, distance);

      for (int i = 0; i < length; i++) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, results[i]);
      }

      if (length == 0) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, default);
      }
      
      return length;
#else
      return Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CapsuleCastNonAlloc(
      Vector2 origin,
      Vector2 size,
      CapsuleDirection2D capsuleDirection,
      float angle,
      Vector2 direction,
      RaycastHit2D[] results,
      float distance,
      int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      int length = Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results, distance, layerMask);

      for (int i = 0; i < length; i++) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results, distance, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CapsuleCastNonAlloc(
      Vector2 origin,
      Vector2 size,
      CapsuleDirection2D capsuleDirection,
      float angle,
      Vector2 direction,
      RaycastHit2D[] results,
      float distance,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      int length = Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results, distance, layerMask, minDepth);

      for (int i = 0; i < length; i++) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results, distance, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Casts a capsule into the Scene, returning Colliders that contact with it into the provided results array. Note: This method will be deprecated in a future build and it is recommended to use CapsuleCast instead.</para>
    /// </summary>
    /// <param name="origin">The point in 2D space where the capsule originates.</param>
    /// <param name="size">The size of the capsule.</param>
    /// <param name="capsuleDirection">The direction of the capsule.</param>
    /// <param name="angle">The angle of the capsule (in degrees).</param>
    /// <param name="direction">Vector representing the direction to cast the capsule.</param>
    /// <param name="results">Array to receive results.</param>
    /// <param name="distance">The maximum distance over which to cast the capsule.</param>
    /// <param name="layerMask">Filter to detect Colliders only on certain layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than this value.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int CapsuleCastNonAlloc(
      Vector2 origin,
      Vector2 size,
      CapsuleDirection2D capsuleDirection,
      float angle,
      Vector2 direction,
      RaycastHit2D[] results,
      [DefaultValue("Mathf.Infinity")] float distance,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      int length = Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results, distance, layerMask, minDepth, maxDepth);

      for (int i = 0; i < length; i++) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, results[i], legacyFilter);
      }

      if (length == 0) {
        DrawCapsule(origin, size, angle, capsuleDirection, direction, distance, default, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.CapsuleCastNonAlloc(origin, size, capsuleDirection, angle, direction, results, distance, layerMask, minDepth, maxDepth);
#endif
    }
  }
}
#endif