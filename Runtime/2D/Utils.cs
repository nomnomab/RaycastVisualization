#if RAYCASTVISUALIZATION_2D_PHYSICS
using UnityEngine;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    internal static ContactFilter2D CreateLegacyFilter(
      int layerMask,
      float minDepth,
      float maxDepth)
    {
      ContactFilter2D legacyFilter = new ContactFilter2D();
      legacyFilter.useTriggers = Physics2D.queriesHitTriggers;
      legacyFilter.SetLayerMask((LayerMask) layerMask);
      legacyFilter.SetDepth(minDepth, maxDepth);
      return legacyFilter;
    }
  }
}
#endif