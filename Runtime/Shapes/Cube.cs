#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
  internal struct Cube {
    public Vector3 origin;
    public Vector3 size;
    public Quaternion rotation;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Draw(Color color) {
      Matrix4x4 matrix4X4 = Matrix4x4.TRS(origin, rotation, size);

      Vector3 blF = matrix4X4.MultiplyPoint(new Vector3(-1, -1, 1));
      Vector3 brF = matrix4X4.MultiplyPoint(new Vector3(1, -1, 1));
      Vector3 tlF = matrix4X4.MultiplyPoint(new Vector3(-1, 1, 1));
      Vector3 trF = matrix4X4.MultiplyPoint(new Vector3(1, 1, 1));

      Vector3 blB = matrix4X4.MultiplyPoint(new Vector3(-1, -1, -1));
      Vector3 brB = matrix4X4.MultiplyPoint(new Vector3(1, -1, -1));
      Vector3 tlB = matrix4X4.MultiplyPoint(new Vector3(-1, 1, -1));
      Vector3 trB = matrix4X4.MultiplyPoint(new Vector3(1, 1, -1));
      
      float drawTime = VisualPhysics.NextDrawTime;
      Debug.DrawLine(blF, brF, color, drawTime, true);
      Debug.DrawLine(brF, trF, color, drawTime, true);
      Debug.DrawLine(trF, tlF, color, drawTime, true);
      Debug.DrawLine(tlF, blF, color, drawTime, true);

      Debug.DrawLine(blB, brB, color, drawTime, true);
      Debug.DrawLine(brB, trB, color, drawTime, true);
      Debug.DrawLine(trB, tlB, color, drawTime, true);
      Debug.DrawLine(tlB, blB, color, drawTime, true);

      Debug.DrawLine(blB, blF, color, drawTime, true);
      Debug.DrawLine(brB, brF, color, drawTime, true);
      Debug.DrawLine(trB, trF, color, drawTime, true);
      Debug.DrawLine(tlB, tlF, color, drawTime, true);
    }
  }
}
#endif