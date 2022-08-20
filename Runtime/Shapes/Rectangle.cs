using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
  internal struct Rectangle {
    public Vector3 origin;
    public Vector2 size;
    public float angle;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Draw(Color color) {
      Matrix4x4 matrix4X4 = Matrix4x4.TRS(origin, Quaternion.Euler(0, 0, angle), size);

      Vector3 bl = matrix4X4.MultiplyPoint(new Vector3(-1, -1, 0));
      Vector3 br = matrix4X4.MultiplyPoint(new Vector3(1, -1, 0));
      Vector3 tl = matrix4X4.MultiplyPoint(new Vector3(-1, 1, 0));
      Vector3 tr = matrix4X4.MultiplyPoint(new Vector3(1, 1, 0));

      Debug.DrawLine(bl, br, color, 0, true);
      Debug.DrawLine(br, tr, color, 0, true);
      Debug.DrawLine(tr, tl, color, 0, true);
      Debug.DrawLine(tl, bl, color, 0, true);
    }
  }
}