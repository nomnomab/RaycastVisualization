using System.Runtime.CompilerServices;
using Nomnom.RaycastVisualization.Shapes;
using UnityEngine;

namespace Nomnom.RaycastVisualization {
  public static partial class VisualPhysics2D {
    /// <summary>
    ///   <para>Retrieves all contact points in for contacts between with the collider1 and collider2, with the results filtered by the ContactFilter2D.</para>
    /// </summary>
    /// <param name="collider1">The Collider to check if it has contacts against collider2.</param>
    /// <param name="collider2">The Collider to check if it has contacts against collider1.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth, or normal angle.</param>
    /// <param name="contacts">An array of ContactPoint2D used to receive the results.</param>
    /// <returns>
    ///   <para>Returns the number of contacts placed in the contacts array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetContacts(
      Collider2D collider1,
      Collider2D collider2,
      ContactFilter2D contactFilter,
      ContactPoint2D[] contacts) {
#if UNITY_EDITOR
      int length = Physics2D.GetContacts(collider1, collider2, contactFilter, contacts);

      for (int i = 0; i < length; i++) {
        DrawContact(contacts[i], contactFilter);
      }
      
      return length;
#else
      return Physics2D.GetContacts(collider1, collider2, contactFilter, contacts);
#endif
    }

    /// <summary>
    ///   <para>Retrieves all contact points in contact with the Collider.</para>
    /// </summary>
    /// <param name="Collider">The Collider to retrieve contacts for.</param>
    /// <param name="contacts">An array of ContactPoint2D used to receive the results.</param>
    /// <param name="collider"></param>
    /// <returns>
    ///   <para>Returns the number of contacts placed in the contacts array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetContacts(Collider2D collider, ContactPoint2D[] contacts) {
#if UNITY_EDITOR
      int length = Physics2D.GetContacts(collider, contacts);

      for (int i = 0; i < length; i++) {
        DrawContact(contacts[i]);
      }
      
      return length;
#else
      return Physics2D.GetContacts(collider, contacts);
#endif
    }

    /// <summary>
    ///   <para>Retrieves all contact points in contact with the Collider, with the results filtered by the ContactFilter2D.</para>
    /// </summary>
    /// <param name="Collider">The Collider to retrieve contacts for.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth, or normal angle.</param>
    /// <param name="contacts">An array of ContactPoint2D used to receive the results.</param>
    /// <param name="collider"></param>
    /// <returns>
    ///   <para>Returns the number of contacts placed in the contacts array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetContacts(
      Collider2D collider,
      ContactFilter2D contactFilter,
      ContactPoint2D[] contacts) {
#if UNITY_EDITOR
      int length = Physics2D.GetContacts(collider, contactFilter, contacts);

      for (int i = 0; i < length; i++) {
        DrawContact(contacts[i], contactFilter);
      }
      
      return length;
#else
      return Physics2D.GetContacts(collider, contactFilter, contacts);
#endif
    }

    /// <summary>
    ///   <para>Retrieves all Colliders in contact with the Collider.</para>
    /// </summary>
    /// <param name="Collider">The Collider to retrieve contacts for.</param>
    /// <param name="Colliders">An array of Collider2D used to receive the results.</param>
    /// <param name="collider"></param>
    /// <param name="colliders"></param>
    /// <returns>
    ///   <para>Returns the number of Colliders placed in the Colliders array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetContacts(Collider2D collider, Collider2D[] colliders) {
#if UNITY_EDITOR
      int length = Physics2D.GetContacts(collider, colliders);

      for (int i = 0; i < length; i++) {
        DrawContact(collider, colliders[i]);
      }
      
      return length;
#else
      return Physics2D.GetContacts(collider, colliders);
#endif
    }

    /// <summary>
    ///   <para>Retrieves all Colliders in contact with the Collider, with the results filtered by the ContactFilter2D.</para>
    /// </summary>
    /// <param name="Collider">The Collider to retrieve contacts for.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth, or normal angle.</param>
    /// <param name="Colliders">An array of Collider2D used to receive the results.</param>
    /// <param name="collider"></param>
    /// <param name="colliders"></param>
    /// <returns>
    ///   <para>Returns the number of Colliders placed in the Colliders array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetContacts(
      Collider2D collider,
      ContactFilter2D contactFilter,
      Collider2D[] colliders) {
#if UNITY_EDITOR
      int length = Physics2D.GetContacts(collider, contactFilter, colliders);

      for (int i = 0; i < length; i++) {
        DrawContact(collider, colliders[i], contactFilter);
      }
      
      return length;
#else
      return Physics2D.GetContacts(collider, contactFilter, colliders);
#endif
    }

    /// <summary>
    ///   <para>Retrieves all contact points in contact with any of the Collider(s) attached to this rigidbody.</para>
    /// </summary>
    /// <param name="rigidbody">The rigidbody to retrieve contacts for.  All Colliders attached to this rigidbody will be checked.</param>
    /// <param name="contacts">An array of ContactPoint2D used to receive the results.</param>
    /// <returns>
    ///   <para>Returns the number of contacts placed in the contacts array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetContacts(Rigidbody2D rigidbody, ContactPoint2D[] contacts) {
#if UNITY_EDITOR
      int length = Physics2D.GetContacts(rigidbody, contacts);

      for (int i = 0; i < length; i++) {
        DrawContact(contacts[i]);
      }
      
      return length;
#else
      return Physics2D.GetContacts(rigidbody, contacts);
#endif
    }

    /// <summary>
    ///   <para>Retrieves all contact points in contact with any of the Collider(s) attached to this rigidbody, with the results filtered by the ContactFilter2D.</para>
    /// </summary>
    /// <param name="rigidbody">The rigidbody to retrieve contacts for.  All Colliders attached to this rigidbody will be checked.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth, or normal angle.</param>
    /// <param name="contacts">An array of ContactPoint2D used to receive the results.</param>
    /// <returns>
    ///   <para>Returns the number of contacts placed in the contacts array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetContacts(
      Rigidbody2D rigidbody,
      ContactFilter2D contactFilter,
      ContactPoint2D[] contacts) {
#if UNITY_EDITOR
      int length = Physics2D.GetContacts(rigidbody, contactFilter, contacts);

      for (int i = 0; i < length; i++) {
        DrawContact(contacts[i], contactFilter);
      }
      
      return length;
#else
      return Physics2D.GetContacts(rigidbody, contactFilter, contacts);
#endif
    }

    /// <summary>
    ///   <para>Retrieves all Colliders in contact with any of the Collider(s) attached to this rigidbody.</para>
    /// </summary>
    /// <param name="rigidbody">The rigidbody to retrieve contacts for.  All Colliders attached to this rigidbody will be checked.</param>
    /// <param name="Colliders">An array of Collider2D used to receive the results.</param>
    /// <param name="colliders"></param>
    /// <returns>
    ///   <para>Returns the number of Colliders placed in the Colliders array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetContacts(Rigidbody2D rigidbody, Collider2D[] colliders) {
#if UNITY_EDITOR
      int length = Physics2D.GetContacts(rigidbody, colliders);

      for (int i = 0; i < length; i++) {
        DrawContact(rigidbody, colliders[i]);
      }
      
      return length;
#else
      return Physics2D.GetContacts(rigidbody, colliders);
#endif
    }

    /// <summary>
    ///   <para>Retrieves all Colliders in contact with any of the Collider(s) attached to this rigidbody, with the results filtered by the ContactFilter2D.</para>
    /// </summary>
    /// <param name="rigidbody">The rigidbody to retrieve contacts for.  All Colliders attached to this rigidbody will be checked.</param>
    /// <param name="contactFilter">The contact filter used to filter the results differently, such as by layer mask, Z depth, or normal angle.</param>
    /// <param name="Colliders">An array of Collider2D used to receive the results.</param>
    /// <param name="colliders"></param>
    /// <returns>
    ///   <para>Returns the number of Colliders placed in the Colliders array.</para>
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetContacts(
      Rigidbody2D rigidbody,
      ContactFilter2D contactFilter,
      Collider2D[] colliders) {
#if UNITY_EDITOR
      int length = Physics2D.GetContacts(rigidbody, contactFilter, colliders);

      for (int i = 0; i < length; i++) {
        DrawContact(rigidbody, colliders[i], contactFilter);
      }
      
      return length;
#else
      return Physics2D.GetContacts(rigidbody, contactFilter, colliders);
#endif
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void DrawContact(ContactPoint2D contactPoint, ContactFilter2D? contactFilter = default) {
      Color color = VisualUtils.GetDefaultColor();
      
      Circle circle = Circle.Default;
      circle.origin = contactPoint.point;
      
      circle.Draw(color);
      
      if (!contactFilter.HasValue) {
        return;
      }
      
      Filter2D filter = new Filter2D {
        origin = circle.origin,
        filter = contactFilter.Value
      };
      filter.Draw(color);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void DrawContact(Collider2D from, Collider2D to, ContactFilter2D? contactFilter = default) {
      Color color = VisualUtils.GetDefaultColor();
      
      Line line = new Line {
        from = from.transform.position,
        to = to.transform.position
      };
      
      line.Draw(color);
      
      if (!contactFilter.HasValue) {
        return;
      }
      
      Filter2D filter = new Filter2D {
        origin = new Vector2(line.from.x, line.from.y),
        filter = contactFilter.Value
      };
      filter.Draw(color);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void DrawContact(Rigidbody2D from, Collider2D to, ContactFilter2D? contactFilter = default) {
      Color color = VisualUtils.GetDefaultColor();
      
      Line line = new Line {
        from = from.transform.position,
        to = to.transform.position
      };
      
      line.Draw(color);
      
      if (!contactFilter.HasValue) {
        return;
      }
      
      Filter2D filter = new Filter2D {
        origin = new Vector2(line.from.x, line.from.y),
        filter = contactFilter.Value
      };
      filter.Draw(color);
    }
  }
}