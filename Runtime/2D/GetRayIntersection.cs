using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;
using UnityEngine.Internal;
using Ray = UnityEngine.Ray;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D GetRayIntersection(Ray ray) {
#if UNITY_EDITOR
      RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
      DrawRayIntersection(ray, hit, ray.direction.magnitude);
      return hit;
#else
      return Physics2D.GetRayIntersection(ray);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D GetRayIntersection(Ray ray, float distance) {
#if UNITY_EDITOR
      RaycastHit2D hit = Physics2D.GetRayIntersection(ray, distance);
      DrawRayIntersection(ray, hit, distance);
      return hit;
#else
      return Physics2D.GetRayIntersection(ray, distance);
#endif
    }

    /// <summary>
    ///   <para>Cast a 3D ray against the Colliders in the Scene returning the first Collider along the ray.</para>
    /// </summary>
    /// <param name="ray">The 3D ray defining origin and direction to test.</param>
    /// <param name="distance">The maximum distance over which to cast the ray.</param>
    /// <param name="layerMask">Filter to detect Colliders only on certain layers.</param>
    /// <returns>
    ///   <para>The cast results returned.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RaycastHit2D GetRayIntersection(
      Ray ray,
      [DefaultValue("Mathf.Infinity")] float distance,
      [DefaultValue("DefaultRaycastLayers")] int layerMask) {
#if UNITY_EDITOR
      RaycastHit2D hit = Physics2D.GetRayIntersection(ray, distance, layerMask);
      DrawRayIntersection(ray, hit, distance);
      return hit;
#else
      return Physics2D.GetRayIntersection(ray, distance, layerMask);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void DrawRayIntersection(in Ray ray, in RaycastHit2D hit, float distance) {
#if UNITY_EDITOR
      bool didHit = hit.collider;
      Color color = VisualUtils.GetColor(didHit);

      if (didHit) {
        Arrow arrow = Arrow.Default;
        arrow.origin = ray.origin;
        arrow.direction = ray.direction.normalized * VisualUtils.GetMaxRayLength(distance);
        arrow.Draw(VisualUtils.GetDefaultColor() * 0.5f);
        
        Line line = new Line {
          from = ray.origin,
          to = hit.point
        };
        
        line.Draw(color);

        Circle circle = Circle.Default;
        circle.origin = hit.point;
        circle.Draw(color);
        
        circle.origin.z = hit.collider.transform.position.z;
        circle.Draw(color);
      } else {
        Arrow arrow = Arrow.Default;
        arrow.origin = ray.origin;
        arrow.direction = ray.direction.normalized * VisualUtils.GetMaxRayLength(distance);
        arrow.Draw(color);
      }
#endif
    }
  }
}