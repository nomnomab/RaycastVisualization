#if RAYCASTVISUALIZATION_2D_PHYSICS
using System.Collections.Generic;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;
using UnityEngine.Internal;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    [ExcludeFromDocs]
    public static RaycastHit2D Linecast(Vector2 start, Vector2 end) {
#if UNITY_EDITOR
      RaycastHit2D hit = Physics2D.Linecast(start, end);
      DrawLine(start, end, hit);
      return hit;
#else
      return Physics2D.Linecast(start, end);
#endif
    }

    [ExcludeFromDocs]
    public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
      RaycastHit2D hit = Physics2D.Linecast(start, end, layerMask);
      DrawLine(start, end, hit, legacyFilter);
      return hit;
#else
      return Physics2D.Linecast(start, end, layerMask);
#endif
    }

    [ExcludeFromDocs]
    public static RaycastHit2D Linecast(
      Vector2 start,
      Vector2 end,
      int layerMask,
      float minDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
      RaycastHit2D hit = Physics2D.Linecast(start, end, layerMask, minDepth);
      DrawLine(start, end, hit, legacyFilter);
      return hit;
#else
      return Physics2D.Linecast(start, end, layerMask, minDepth);
#endif
    }

    /// <summary>
    ///   <para>Casts a line segment against Colliders in the Scene.</para>
    /// </summary>
    /// <param name="start">The start point of the line in world space.</param>
    /// <param name="end">The end point of the line in world space.</param>
    /// <param name="layerMask">Filter to detect Colliders only on certain layers.</param>
    /// <param name="minDepth">Only include objects with a Z coordinate (depth) greater than or equal to this value.</param>
    /// <param name="maxDepth">Only include objects with a Z coordinate (depth) less than or equal to this value.</param>
    /// <returns>
    ///   <para>The cast results returned.</para>
    /// </returns>
    public static RaycastHit2D Linecast(
      Vector2 start,
      Vector2 end,
      [DefaultValue("DefaultRaycastLayers")] int layerMask,
      [DefaultValue("-Mathf.Infinity")] float minDepth,
      [DefaultValue("Mathf.Infinity")] float maxDepth) {
#if UNITY_EDITOR
      ContactFilter2D legacyFilter = CreateLegacyFilter(layerMask, minDepth, maxDepth);
      RaycastHit2D hit = Physics2D.Linecast(start, end, layerMask, minDepth, maxDepth);
      DrawLine(start, end, hit, legacyFilter);
      return hit;
#else
      return Physics2D.Linecast(start, end, layerMask, minDepth, maxDepth);
#endif
    }

    /// <summary>
    ///   <para>Casts a line segment against Colliders in the Scene with results filtered by ContactFilter2D.</para>
    /// </summary>
    /// <param name="start">The start point of the line in world space.</param>
    /// <param name="end">The end point of the line in world space.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth, or normal angle.</param>
    /// <param name="results">The array to receive results.  The size of the array determines the maximum number of results that can be returned.</param>
    /// <returns>
    ///   <para>Returns the number of results placed in the results array.</para>
    /// </returns>
    public static int Linecast(
      Vector2 start,
      Vector2 end,
      ContactFilter2D contactFilter,
      RaycastHit2D[] results) {
#if UNITY_EDITOR
      int length = Physics2D.Linecast(start, end, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawLine(start, end, results[i], contactFilter);
      }

      if (length == 0) {
        DrawLine(start, end, default, contactFilter);
      }
      
      return length;
#else
      return Physics2D.Linecast(start, end, contactFilter, results);
#endif
    }

    public static int Linecast(
      Vector2 start,
      Vector2 end,
      ContactFilter2D contactFilter,
      List<RaycastHit2D> results) {
#if UNITY_EDITOR
      int length = Physics2D.Linecast(start, end, contactFilter, results);

      for (int i = 0; i < length; i++) {
        DrawLine(start, end, results[i], contactFilter);
      }

      if (length == 0) {
        DrawLine(start, end, default, contactFilter);
      }
      
      return length;
#else
      return Physics2D.Linecast(start, end, contactFilter, results);
#endif
    }

    internal static void DrawLine(Vector2 start, Vector2 end, in RaycastHit2D hit, ContactFilter2D? contactFilter = default) {
#if UNITY_EDITOR
      bool didHit = hit.collider;
      Color color = VisualUtils.GetColor(didHit);

      if (didHit) {
        float depth = hit.transform.position.z;
        
        Line baseLine = new Line {
          from = start,
          to = end
        };
        baseLine.Draw(VisualUtils.GetDefaultColor() * 0.5f);
        
        Line line = new Line {
          from = start,
          to = hit.point
        };

        Line line2 = line;
        line2.from.z = depth;
        line2.to.z = depth;

        NormalCircle circle = NormalCircle.Default;
        Vector3 circleOrigin = hit.point;
        
        circleOrigin.z = depth;
        circle.origin = circleOrigin;
        circle.upDirection = hit.normal;

        line.Draw(color);
        line2.Draw(color * 0.5f);
        circle.Draw(color);
        
        Range range = new Range {
          from = line.from,
          to = line2.from
        };
        
        range.Draw(VisualUtils.GetDefaultColor());
      } else {
        Line line = new Line {
          from = start,
          to = end
        };
        line.Draw(color);
      }

      if (!contactFilter.HasValue) {
        return;
      }

      Filter2D filter = new Filter2D {
        origin = start,
        filter = contactFilter.Value
      };
      filter.Draw(VisualUtils.GetDefaultColor());
#endif
    }
  }
}
#endif