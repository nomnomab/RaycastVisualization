using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
  internal struct Line {
    public Vector3 from;
    public Vector3 to;

    public void Draw(Color color) {
      Debug.DrawLine(from, to, color, 0, true);
    }
  }
}