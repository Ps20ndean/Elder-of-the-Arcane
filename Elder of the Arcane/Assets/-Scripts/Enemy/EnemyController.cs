using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyController : MonoBehaviour
{
    public LayerMask collisionMask;
    const float skinWidth = .25f;
    public int horizontalRayCount = 16;
    public int verticalRayCount = 16;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    BoxCollider2D collider;
    RaycastOrigins raycastOrigins;
    public CollisionInfo collisions;

    void Start()
    {
        // collider is set to the boxcollider component
        collider = GetComponent<BoxCollider2D>();

        // runs the method calculate ray spacing
        CalculateRaySpacing();

    }
    public void Move(Vector3 velocity)
    {
        // updates the origins of the raycast
        UpdateRaycastOrigins();
        // resets the collisions
        collisions.Reset();

        // if the x velocity is not equal to 0 do the following
        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }
        // if the y velocity is not equal to 0 do the following
        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        // moves the player according to what the velocity is set to 
        transform.Translate(velocity);
    }

    void HorizontalCollisions(ref Vector3 velocity)
    {
        // the direction of x is set to the sin value of the x velocity
        float directionX = Mathf.Sign(velocity.x);

        // the raylength is the absolute value of velocity x added to the skin Width
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        // runs a for loop throughout the horizontal ray counts
        for (int i = 0; i < horizontalRayCount; i++)
        {
            // sets the ray origin to the bottom right and bottom left of the object
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;

            // the ray origin is added to a up translation which is then multiplied to the horizontal ray spacing times i
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            // sets variable hit to a raycast hit and draws a line from the enemy
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            // this is what actually draws the ray on the screen so you know where the raycast is
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            // if the raycast hits anything do the following
            if (hit)
            {
                // velocity x is equal to the hit distance minus the skin width, then times that by the direction of x
                velocity.x = (hit.distance - skinWidth) * directionX;

                // ray length is the distance that the raycast hit from
                rayLength = hit.distance;

                //sets true if hitting collision on the left or right of x axis
                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }
        }

    }

    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);

        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
            if (hit)
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                //sets bools to true if collisions on above or below 
                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }

    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    struct RaycastOrigins
    {
        // sets the raycast origins to top right/left and the bottom left/right
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        // sets the variables of what defines the boundaries of the enemy
        public bool above, below;
        public bool left, right;

        public void Reset()
        {
            // resets all of the collisions
            above = below = false;
            left = right = false;
        }
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }
}
