#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    /// <summary>
    ///   <para>Calculates the minimum distance between two Colliders.</para>
    /// </summary>
    /// <param name="colliderA">A Collider used to calculate the minimum distance against colliderB.</param>
    /// <param name="colliderB">A Collider used to calculate the minimum distance against colliderA.</param>
    /// <returns>
    ///   <para>The minimum distance between colliderA and colliderB.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ColliderDistance2D Distance(Collider2D colliderA, Collider2D colliderB) {
#if UNITY_EDITOR
      ColliderDistance2D distance = Physics2D.Distance(colliderA, colliderB);
      Color color = VisualUtils.GetDefaultColor();

      // default
      float depthA = colliderA.transform.position.z;
      float depthB = colliderB.transform.position.z;
      
      Vector3 from = distance.pointA;
      Vector3 to = distance.pointB;

      from.z = depthA;
      to.z = depthB;

      from.y = distance.pointA.y;
      to.y = distance.pointB.y;

      // projection
      Range rangeA = new Range {
        from = from,
        to = new Vector3(from.x, from.y, to.z)
      };
      
      Range rangeB = new Range {
        from = new Vector3(to.x, to.y, from.z),
        to = to
      };
      
      rangeA.Draw(color);
      rangeB.Draw(color);

      Vector3 midPointA = (rangeA.to + rangeA.from) * 0.5f;
      Vector3 midPointB = (rangeB.to + rangeB.from) * 0.5f;
      
      Line line = new Line {
        from = midPointA,
        to = midPointB
      };

      line.Draw(color);
      
      UnityEditor.Handles.Label((midPointA + midPointB) * 0.5f, $"{distance.distance}m {(distance.isOverlapped ? "(Overlap)" : string.Empty)}{(!distance.isValid ? "(Invalid)" : string.Empty)}");
      
      return distance;
#else
      return Physics2D.Distance(colliderA, colliderB);
#endif
    }
  }
}
#endif