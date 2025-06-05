#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.RaycastNonAlloc(origin, direction, results);
      for (int i = 0; i < length; i++) {
        DrawRaycast(origin, direction, in results[i], Mathf.Infinity);
      }

      if (length == 0) {
        DrawRaycast(origin, direction, default, Mathf.Infinity);
      }
      
      return length;
#else
      return Physics2D.RaycastNonAlloc(origin, direction, results);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RaycastNonAlloc(
      Vector2 origin,
      Vector2 direction,
      RaycastHit2D[] results,
      float distance)
    {
#if UNITY_EDITOR
      int length = Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, results);
      for (int i = 0; i < length; i++) {
        DrawRaycast(origin, direction, in results[i], distance);
      }

      if (length == 0) {
        DrawRaycast(origin, direction, default, distance);
      }
      
      return length;
#else
      return Physics2D.RaycastNonAlloc(origin, direction, results, distance);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RaycastNonAlloc(
      Vector2 origin,
      Vector2 direction,
      RaycastHit2D[] results,
      float distance,
      int layerMask)
    {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      int length = Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, legacyFilter, results);
      for (int i = 0; i < length; i++) {
        DrawRaycast(origin, direction, in results[i], distance, legacyFilter);
      }

      if (length == 0) {
        DrawRaycast(origin, direction, default, distance, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.RaycastNonAlloc(origin, direction, results, distance, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RaycastNonAlloc(
      Vector2 origin,
      Vector2 direction,
      RaycastHit2D[] results,
      float distance,
      int layerMask,
      float minDepth)
    {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      int length = Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, legacyFilter, results);
      for (int i = 0; i < length; i++) {
        DrawRaycast(origin, direction, in results[i], distance, legacyFilter);
      }

      if (length == 0) {
        DrawRaycast(origin, direction, default, distance, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.RaycastNonAlloc(origin, direction, results, distance, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Casts a ray into the Scene. Note: This method will be deprecated in a future build and it is recommended to use Raycast instead.</para>
    /// </summary>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <param name="origin">The point in 2D space where the ray originates.</param>
    /// <param name="direction">A vector representing the direction of the ray.</param>
    /// <param name="results">Array to receive results.</param>
    /// <param name="distance">The maximum distance over which to cast the ray.</param>
    /// <param name="layerMask">Filter to check objects only on specific layers.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RaycastNonAlloc(
      Vector2 origin,
      Vector2 direction,
      RaycastHit2D[] results,
      [DefaultValue("Mathf.Infinity")] float distance,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth)
    {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      int length = Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, legacyFilter, results);
      for (int i = 0; i < length; i++) {
        DrawRaycast(origin, direction, in results[i], distance, legacyFilter);
      }

      if (length == 0) {
        DrawRaycast(origin, direction, default, distance, legacyFilter);
      }
      
      return length;
#else
      return Physics2D.RaycastNonAlloc(origin, direction, results, distance, layerMask, minDepth, maxDepth);
#endif
    }
  }
}
#endif