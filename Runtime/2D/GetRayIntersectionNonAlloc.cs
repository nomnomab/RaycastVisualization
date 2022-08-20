using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.GetRayIntersectionNonAlloc(ray, results);

      for (int i = 0; i < length; i++) {
        DrawRayIntersection(ray, results[i], ray.direction.magnitude);
      }

      if (length == 0) {
        DrawRayIntersection(ray, default, ray.direction.magnitude);
      }
      
      return length;
#else
      return Physics2D.GetRayIntersectionNonAlloc(ray, results);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, float distance) {
#if UNITY_EDITOR
      int length = Physics2D.GetRayIntersectionNonAlloc(ray, results, distance);

      for (int i = 0; i < length; i++) {
        DrawRayIntersection(ray, results[i], distance);
      }

      if (length == 0) {
        DrawRayIntersection(ray, default, distance);
      }
      
      return length;
#else
      return Physics2D.GetRayIntersectionNonAlloc(ray, results, distance);
#endif
    }

    /// <summary>
    ///   <para>Cast a 3D ray against the Colliders in the Scene returning the Colliders along the ray. Note: This method will be deprecated in a future build and it is recommended to use GetRayIntersection instead.</para>
    /// </summary>
    /// <param name="ray">The 3D ray defining origin and direction to test.</param>
    /// <param name="distance">The maximum distance over which to cast the ray.</param>
    /// <param name="layerMask">Filter to detect Colliders only on certain layers.</param>
    /// <param name="results">Array to receive results.</param>
    /// <returns>
    ///   <para>The number of results returned.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetRayIntersectionNonAlloc(
      Ray ray,
      RaycastHit2D[] results,
      [DefaultValue("Mathf.Infinity")] float distance,
      [DefaultValue("DefaultRaycastLayers")] int layerMask) {
#if UNITY_EDITOR
      int length = Physics2D.GetRayIntersectionNonAlloc(ray, results, distance, layerMask);

      for (int i = 0; i < length; i++) {
        DrawRayIntersection(ray, results[i], distance);
      }

      if (length == 0) {
        DrawRayIntersection(ray, default, distance);
      }
      
      return length;
#else
      return Physics2D.GetRayIntersectionNonAlloc(ray, results, distance, layerMask);
#endif
    }
  }
}