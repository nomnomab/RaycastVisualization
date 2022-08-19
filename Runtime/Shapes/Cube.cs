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

      Debug.DrawLine(blF, brF, color, 0, true);
      Debug.DrawLine(brF, trF, color, 0, true);
      Debug.DrawLine(trF, tlF, color, 0, true);
      Debug.DrawLine(tlF, blF, color, 0, true);

      Debug.DrawLine(blB, brB, color, 0, true);
      Debug.DrawLine(brB, trB, color, 0, true);
      Debug.DrawLine(trB, tlB, color, 0, true);
      Debug.DrawLine(tlB, blB, color, 0, true);

      Debug.DrawLine(blB, blF, color, 0, true);
      Debug.DrawLine(brB, brF, color, 0, true);
      Debug.DrawLine(trB, trF, color, 0, true);
      Debug.DrawLine(tlB, tlF, color, 0, true);
    }
  }
}