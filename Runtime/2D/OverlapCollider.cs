using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    /// <summary>
    ///   <para>Gets a list of all Colliders that overlap the given Collider.</para>
    /// </summary>
    /// <param name="Collider">The Collider that defines the area used to query for other Collider overlaps.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth.  Note that normal angle is not used for overlap testing.</param>
    /// <param name="results">The array to receive results.  The size of the array determines the maximum number of results that can be returned.</param>
    /// <param name="collider"></param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapCollider(
      Collider2D collider,
      ContactFilter2D contactFilter,
      Collider2D[] results) {
#if UNITY_EDITOR
      int length = PhysicsScene2D.OverlapCollider(collider, contactFilter, results);
      Color color = VisualUtils.GetColor(length > 0);
      
      Circle circle = new Circle {
        origin = collider.bounds.center,
        radius = 0.5f
      };
      circle.Draw(color);

      Vector3 position = collider.transform.position;

      for (int i = 0; i < length; i++) {
        Collider2D c = results[i];
        circle = new Circle {
          origin = c.bounds.center,
          radius = 0.5f
        };
        circle.Draw(color);

        Line line = new Line {
          from = c.transform.position,
          to = position
        };
        
        line.Draw(VisualUtils.GetDefaultColor());
      }

      return length;
#else
       return PhysicsScene2D.OverlapCollider(collider, contactFilter, results);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int OverlapCollider(
      Collider2D collider,
      ContactFilter2D contactFilter,
      List<Collider2D> results) {
#if UNITY_EDITOR
      int length = PhysicsScene2D.OverlapCollider(collider, contactFilter, results);
      Color color = VisualUtils.GetColor(length > 0);
      
      Circle circle = new Circle {
        origin = collider.bounds.center,
        radius = 0.5f
      };
      circle.Draw(color);
      
      Vector3 position = collider.transform.position;

      for (int i = 0; i < length; i++) {
        Collider2D c = results[i];
        circle = new Circle {
          origin = c.bounds.center,
          radius = 0.5f
        };
        circle.Draw(color);

        Line line = new Line {
          from = c.transform.position,
          to = position
        };
        
        line.Draw(VisualUtils.GetDefaultColor());
      }

      return length;
#else
       return PhysicsScene2D.OverlapCollider(collider, contactFilter, results);
#endif
    }
  }
}