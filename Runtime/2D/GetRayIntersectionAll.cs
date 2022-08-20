using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D[] GetRayIntersectionAll(Ray ray) {
#if UNITY_EDITOR
      RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray);

      foreach (RaycastHit2D hit in hits) {
        DrawRayIntersection(ray, hit, ray.direction.magnitude);
      }

      if (hits.Length == 0) {
        DrawRayIntersection(ray, default, ray.direction.magnitude);
      }
      
      return hits;
#else
      return Physics2D.GetRayIntersectionAll(ray);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, float distance) {
#if UNITY_EDITOR
      RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, distance);

      foreach (RaycastHit2D hit in hits) {
        DrawRayIntersection(ray, hit, distance);
      }

      if (hits.Length == 0) {
        DrawRayIntersection(ray, default, distance);
      }
      
      return hits;
#else
      return Physics2D.GetRayIntersectionAll(ray, distance);
#endif
    }

    /// <summary>
    ///   <para>Cast a 3D ray against the Colliders in the Scene returning all the Colliders along the ray.</para>
    /// </summary>
    /// <param name="ray">The 3D ray defining origin and direction to test.</param>
    /// <param name="distance">The maximum distance over which to cast the ray.</param>
    /// <param name="layerMask">Filter to detect Colliders only on certain layers.</param>
    /// <returns>
    ///   <para>The cast results returned.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D[] GetRayIntersectionAll(
      Ray ray,
      [DefaultValue("Mathf.Infinity")] float distance,
      [DefaultValue("DefaultRaycastLayers")] int layerMask) {
#if UNITY_EDITOR
      RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, distance, layerMask);

      foreach (RaycastHit2D hit in hits) {
        DrawRayIntersection(ray, hit, distance);
      }

      if (hits.Length == 0) {
        DrawRayIntersection(ray, default, distance);
      }
      
      return hits;
#else
      return Physics2D.GetRayIntersectionAll(ray, distance, layerMask);
#endif
    }
  }
}