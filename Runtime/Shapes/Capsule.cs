#if UNITY_EDITOR && RAYCASTVISUALIZATION_3D_PHYSICS
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
  public struct Capsule {
    public Vector3 from;
    public Vector3 to;
    public Vector3 direction;
    public float radius;
    public float maxDistance;
    public RaycastHit hit;
    public bool didHit;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Draw(Color color, float length) {
      Vector3 dir = direction * length;
      Vector3 point1Pos = from + dir;
      Vector3 point2Pos = to + dir;
      
      Sphere sphere1 = new Sphere {
        origin = point1Pos,
        radius = radius
      };
        
      Sphere sphere2 = new Sphere {
        origin = point2Pos,
        radius = radius
      };
      
      float drawTime = VisualPhysics.NextDrawTime;
      Debug.DrawLine(point1Pos + Vector3.forward * radius, point2Pos + Vector3.forward * radius, color, drawTime, true);
      Debug.DrawLine(point1Pos + Vector3.back * radius, point2Pos + Vector3.back * radius, color, drawTime, true);
      Debug.DrawLine(point1Pos + Vector3.right * radius, point2Pos + Vector3.right * radius, color, drawTime, true);
      Debug.DrawLine(point1Pos + Vector3.left * radius, point2Pos + Vector3.left * radius, color, drawTime, true);
      
      sphere1.Draw(color);
      sphere2.Draw(color);
    }
  }
}
#endif