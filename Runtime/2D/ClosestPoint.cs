using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    /// <summary>
    ///   <para>Returns a point on the perimeter of the Collider that is closest to the specified position.</para>
    /// </summary>
    /// <param name="position">The position from which to find the closest point on the specified Collider.</param>
    /// <param name="Collider">The Collider on which to find the closest specified position.</param>
    /// <param name="collider"></param>
    /// <returns>
    ///   <para>A point on the perimeter of the Collider that is closest to the specified position.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ClosestPoint(Vector2 position, Collider2D collider) {
#if UNITY_EDITOR
      Vector2 point = Physics2D.ClosestPoint(position, collider);
      Vector2 dir = point - position;

      Color color = VisualUtils.GetDefaultColor();
      Color defaultColor = VisualUtils.GetColor(true);

      // default
      Arrow arrow = Arrow.Default;
      arrow.origin = position;
      arrow.direction = dir;
      
      arrow.Draw(color);
      
      Circle circle = Circle.Default;
      circle.origin = point;
      circle.upDirection = dir.normalized;
      
      circle.Draw(defaultColor);
      
      // projection
      arrow.origin.z = collider.transform.position.z;
      circle.origin.z = arrow.origin.z;
      
      arrow.Draw(color * 0.5f);
      circle.Draw(defaultColor);

      Range range = new Range {
        from = arrow.origin,
        to = position
      };
      
      range.Draw(color);
      
      return point;
#else
      return Physics2D.ClosestPoint(position, collider);
#endif
    }

    /// <summary>
    ///   <para>Returns a point on the perimeter of all enabled Colliders attached to the rigidbody that is closest to the specified position.</para>
    /// </summary>
    /// <param name="position">The position from which to find the closest point on the specified rigidbody.</param>
    /// <param name="rigidbody">The Rigidbody on which to find the closest specified position.</param>
    /// <returns>
    ///   <para>A point on the perimeter of a Collider attached to the rigidbody that is closest to the specified position.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 ClosestPoint(Vector2 position, Rigidbody2D rigidbody) {
#if UNITY_EDITOR
      Vector2 point = Physics2D.ClosestPoint(position, rigidbody);
      Vector2 dir = point - position;

      Color color = VisualUtils.GetDefaultColor();
      Color defaultColor = VisualUtils.GetColor(true);

      // default
      Arrow arrow = Arrow.Default;
      arrow.origin = position;
      arrow.direction = dir;
      
      arrow.Draw(color);
      
      Circle circle = Circle.Default;
      circle.origin = point;
      circle.upDirection = dir.normalized;
      
      circle.Draw(defaultColor);
      
      // projection
      arrow.origin.z = rigidbody.transform.position.z;
      circle.origin.z = arrow.origin.z;
      
      arrow.Draw(color * 0.5f);
      circle.Draw(defaultColor);

      Range range = new Range {
        from = arrow.origin,
        to = position
      };
      
      range.Draw(color);
      
      return point;
#else
      return Physics2D.ClosestPoint(position, rigidbody);
#endif
    }
  }
}