#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Nomnom.RaycastVisualization.Shapes {
  public struct Filter2D {
    public Vector2 origin;
    public ContactFilter2D filter;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Draw(Color color) {
      float maxDepth = filter.maxDepth;
      float minDepth = filter.minDepth;

      if (float.IsInfinity(maxDepth)) {
        maxDepth = 1000 * Mathf.Sign(maxDepth);
      } else {
        maxDepth = Mathf.Max(0, VisualUtils.GetMaxRayLength(Mathf.Abs(maxDepth))) * Mathf.Sign(maxDepth);
      }
      
      if (float.IsInfinity(minDepth)) {
        minDepth = 1000 * Mathf.Sign(minDepth);
      } else {
        minDepth = Mathf.Max(0, VisualUtils.GetMaxRayLength(Mathf.Abs(minDepth))) * Mathf.Sign(minDepth);
      }

      Vector3 origin = this.origin;
      Range range = new Range {
        from = origin + Vector3.forward * maxDepth,
        to = origin + Vector3.forward * minDepth
      };
      
      range.Draw(color);
    }
  }
}
#endif