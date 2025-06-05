#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsTouchingLayers(Collider2D collider) {
#if UNITY_EDITOR
      bool didHit = Physics2D.IsTouchingLayers(collider);

      Circle circle = Circle.Default;
      circle.origin = collider.transform.position;
      circle.Draw(VisualUtils.GetColor(didHit));
      
      circle.radius *= 0.75f;
      circle.Draw(VisualUtils.GetColor(didHit));
      
      return didHit;
#else
      return Physics2D.IsTouchingLayers(collider);
#endif
    }

    /// <summary>
    ///   <para>Checks whether the Collider is touching any Colliders on the specified layerMask or not.</para>
    /// </summary>
    /// <param name="Collider">The Collider to check if it is touching Colliders on the layerMask.</param>
    /// <param name="layerMask">Any Colliders on any of these layers count as touching.</param>
    /// <param name="collider"></param>
    /// <returns>
    ///   <para>Whether the Collider is touching any Colliders on the specified layerMask or not.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsTouchingLayers(Collider2D collider, [DefaultValue("Physics2D.AllLayers")] int layerMask) {
#if UNITY_EDITOR
      bool didHit = Physics2D.IsTouchingLayers(collider, layerMask);

      Circle circle = Circle.Default;
      circle.origin = collider.transform.position;
      circle.Draw(VisualUtils.GetColor(didHit));
      
      circle.radius *= 0.75f;
      circle.Draw(VisualUtils.GetColor(didHit));
      
      return didHit;
#else
      return Physics2D.IsTouchingLayers(collider, layerMask);
#endif
    }
  }
}
#endif