#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    /// <summary>
    ///   <para>Checks whether the passed Colliders are in contact or not.</para>
    /// </summary>
    /// <param name="collider1">The Collider to check if it is touching collider2.</param>
    /// <param name="collider2">The Collider to check if it is touching collider1.</param>
    /// <returns>
    ///   <para>Whether collider1 is touching collider2 or not.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsTouching(Collider2D collider1, Collider2D collider2) {
#if UNITY_EDITOR
      bool didHit = Physics2D.IsTouching(collider1, collider2);
      
      Line line = new Line {
        from = collider1.transform.position,
        to = collider2.transform.position
      };
      
      line.Draw(VisualUtils.GetColor(didHit));
      return didHit;
#else
      return Physics2D.IsTouching(collider1, collider2);
#endif
    }

    /// <summary>
    ///   <para>Checks whether the passed Colliders are in contact or not.</para>
    /// </summary>
    /// <param name="collider1">The Collider to check if it is touching collider2.</param>
    /// <param name="collider2">The Collider to check if it is touching collider1.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth, or normal angle.</param>
    /// <returns>
    ///   <para>Whether collider1 is touching collider2 or not.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsTouching(
      Collider2D collider1,
      Collider2D collider2,
      ContactFilter2D contactFilter) {
#if UNITY_EDITOR
      bool didHit = Physics2D.IsTouching(collider1, collider2, contactFilter);

      Line line = new Line {
        from = collider1.transform.position,
        to = collider2.transform.position
      };

      line.Draw(VisualUtils.GetColor(didHit));

      Filter2D filter = new Filter2D {
        origin = collider1.transform.position,
        filter = contactFilter
      };
      
      filter.Draw(VisualUtils.GetDefaultColor());
      
      return didHit;
#else
      return Physics2D.IsTouching(collider1, collider2, contactFilter);
#endif
    }

    /// <summary>
    ///   <para>Checks whether the passed Colliders are in contact or not.</para>
    /// </summary>
    /// <param name="Collider">The Collider to check if it is touching any other Collider filtered by the contactFilter.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth, or normal angle.</param>
    /// <param name="collider"></param>
    /// <returns>
    ///   <para>Whether the Collider is touching any other Collider filtered by the contactFilter or not.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsTouching(Collider2D collider, ContactFilter2D contactFilter) {
#if UNITY_EDITOR
      bool didHit = Physics2D.IsTouching(collider, contactFilter);

      Filter2D filter = new Filter2D {
        origin = collider.transform.position,
        filter = contactFilter
      };
      
      filter.Draw(VisualUtils.GetColor(didHit));
      
      return didHit;
#else
      return Physics2D.IsTouching(collider, contactFilter);
#endif
    }
  }
}
#endif