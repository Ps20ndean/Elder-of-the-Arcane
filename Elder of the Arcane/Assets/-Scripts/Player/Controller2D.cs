using UnityEngine;
using System.Collections;

public class Controller2D : RaycastController
{
    
    public float maxSlopeAngle = 80;
    public CollisionInfo collisions;
    [HideInInspector]
    public Vector2 playerInput;

    public override void Start()
    {
        base.Start();
        collisions.faceDir = 1;

    }

    public void Move(Vector2 moveAmount, bool standingOnPlatform)
    {
        // runs the move method
        Move(moveAmount, Vector2.zero, standingOnPlatform);
    }
    public void Move(Vector2 moveAmount, Vector2 input, bool standingOnPlatform = false)
    {
        UpdateRaycastOrigins(); // update the origins of the raycast

        collisions.Reset(); // rests the collisions

        collisions.moveAmountOld = moveAmount; // collisions are reset

        playerInput = input; // player input is the input variable

        if (moveAmount.y < 0)
        {
            // descend slope if the move amount y is less than 0
            DescendSlope(ref moveAmount);
        }

        if (moveAmount.x != 0)
        {
            // face direction based on the sin value of move amount x
            collisions.faceDir = (int)Mathf.Sign(moveAmount.x);
        }

        // run horizontal collision checking
        HorizontalCollisions(ref moveAmount);

        
        if (moveAmount.y != 0)
        {
            // run vertical collisions
            VerticalCollisions(ref moveAmount);
        }

        // move by the move amount
        transform.Translate(moveAmount);

        if (standingOnPlatform)
        {
            // if the player is standing on a platform then there is a collision below them
            collisions.below = true;
        }
    }

    void HorizontalCollisions(ref Vector2 moveAmount)
    {
        // dir x is equal to the face direction 
        float directionX = collisions.faceDir;

        // ray length is the absolute value of move x plus the skin width
        float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

        // if the absolute value of move amount x is less than skin width do the following
        if (Mathf.Abs(moveAmount.x) < skinWidth)
        {
            // ray length is equal to 2 times the skin width
            rayLength = 2 * skinWidth;
        }

        // runs a for loop based on how many horizontal raycasts there are
        for (int i = 0; i < horizontalRayCount; i++)
        {
            // ray origin is set to the bottom left and bottom right
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            
            // ray origin is added by a translation up times the horizontal ray spacing times i 
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            // hit is equal to the raycast from the rayorigin 
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            // this is what actually draws the ray in the scene view
            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.blue);

            if (hit) // if theres a hit by the raycast do the following
            {

                if (hit.distance == 0) // if the collision is touching the player continue
                {
                    continue;
                }

                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up); // the slope angle is equal to the angle of the hit and a translation up

                if (i == 0 && slopeAngle <= maxSlopeAngle) // if i is 0 and slope angle is less than max slope angle then do the following
                {
                    if (collisions.descendingSlope) // if the player is descending the slope then do the following
                    {
                        collisions.descendingSlope = false; // descending slope is set to false

                        moveAmount = collisions.moveAmountOld; // move amount is set to the previous move amount
                    }
                    float distanceToSlopeStart = 0; // distance to slope start is set to 0

                    if (slopeAngle != collisions.slopeAngleOld) // if slope angle isnt equal to the slope angle old then do the following
                    {
                        distanceToSlopeStart = hit.distance - skinWidth; // distance to slope start is equal to the hit distance minus the skin width
                    }

                    ClimbSlope(ref moveAmount, slopeAngle, hit.normal); // climbs the slope based on the move amount, and the angle of the slope and the hit normal

                    moveAmount.x += distanceToSlopeStart * directionX; // move amount x is added by the distancetoslopestart times the direction of x
                }

                
                if (!collisions.climbingSlope || slopeAngle > maxSlopeAngle) // if youre not climbing a slope or the slope angle is less than the max angle then do the following
                {
                    
                    moveAmount.x = (hit.distance - skinWidth) * 1; // move amount x is equal to the hit distance minus skin width times 1

                    rayLength = hit.distance; // ray length is equal to the hit distance

                    if (collisions.climbingSlope) // if theres a collision between a climbing slope then do the following
                    {

                        // move amount y is equal to the tan value of slope angle times radians, times the absolute value of move amount x
                        moveAmount.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x); 
                    }


                }

            }
        }
    }

    IEnumerator Wait(float seconds)
    {
        // wait a certain amount of seconds
        yield return new WaitForSeconds(seconds);
    }
    void VerticalCollisions(ref Vector2 moveAmount)
    {
        float directionY = Mathf.Sign(moveAmount.y); // direction y is equal to the sin value of move amount y

        float rayLength = Mathf.Abs(moveAmount.y) + skinWidth; // raylength is equal to the absolute value of move amount y plus the skin width

        // runs a for loop based on how many vertical raycasts there are
        for (int i = 0; i < verticalRayCount; i++)
        {

            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft; // rayorigins are spawned on the bottom and top left of the object

            rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x); // ray origin is added by the translation to the right and times the verticalrayspacing times i plus move amount of x

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask); // hit is equal to the raycast shooting out from the origin

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red); // this is what actually draws the raycast onto the scene

            if (hit) // if the raycast hits anything then do the following
            {
                if (hit.collider.tag == "Through") // if it goes through something do the following
                {
                    if (directionY == 1 || hit.distance == 0) // if the y direction is 1 or if its touching a object then continue
                    {

                        continue;

                    }
                    if (collisions.fallingThroughPlatform) // if the players falling through a platform do the following
                    {

                        continue;
                    }
                    if (playerInput.y == -1) // if the y player input is -1 do the following
                    {
                        collisions.fallingThroughPlatform = true; // the player is falling through a platform

                        Invoke("ResetFallingThroughPlatform", .5f); // reset the falling through platform variable to the normal value after half a second and then continue
                        continue;
                    }
                }

                moveAmount.y = (hit.distance - skinWidth) * directionY; // move amount y is hit distance minus the skin width times the y direction

                rayLength = hit.distance; // ray length is hit distance

                if (collisions.climbingSlope) // if the player is climbing a slope do the following
                {
                    // move amount of x is equal to the y move amount divided by the tan of slope angle times the radians , times the sin value of move amount x
                    moveAmount.x = moveAmount.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(moveAmount.x);
                }

                // resets collisions above and below
                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }

        if (collisions.climbingSlope) // if climbing the slope
        {
            float directionX = Mathf.Sign(moveAmount.x); // direction x is equal to the sin of move amount x

            rayLength = Mathf.Abs(moveAmount.x) + skinWidth; // raylength is the absolute value of move amount x plus the skin width

            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight) + Vector2.up * moveAmount.y; // ray origin is spawned out of the raycast origins 

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask); // this is what draws the raycast onto the scene view

            if (hit) // if the raycast hits something do the following
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up); // slope angle is the angle of hit.normal and a translation up

                if (slopeAngle != collisions.slopeAngle) // if slopeangle isnt equal to the slope angle do the following
                {
                    moveAmount.x = (hit.distance - skinWidth) * directionX; // move amount x is equal to the hit distance minus the skin width times the direction x

                    collisions.slopeAngle = slopeAngle; // slope angle = slope angle

                    collisions.slopeNormal = hit.normal; // slope normal = hit normal
                }
            }
        }
    }

    void ClimbSlope(ref Vector2 moveAmount, float slopeAngle, Vector2 slopeNormal) // handles climbing slopes
    {
        float moveDistance = Mathf.Abs(moveAmount.x); // movedistance is the absolute value of move amount x
            
        float climbmoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance; // climb move amount y is the sin of slope angle times the radians times move distance

        if (moveAmount.y <= climbmoveAmountY) // if the move amount of y is less than or equal to the climb amount of y doing the following
        {
            moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x); // move amount x is equal to the cos of slope angle times the radians times the move distance times the sin value of move amount x
            collisions.climbingSlope = true; // sets climbing slope to true
            collisions.slopeAngle = slopeAngle; // slope angle is reset
            collisions.slopeNormal = slopeNormal; // slope normal is reset
        }
    }

    void DescendSlope(ref Vector2 moveAmount)
    {

        RaycastHit2D maxSlopeHitLeft = Physics2D.Raycast(raycastOrigins.bottomLeft, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidth, collisionMask); // controls the bottom left raycast
        RaycastHit2D maxSlopeHitRight = Physics2D.Raycast(raycastOrigins.bottomRight, Vector2.down, Mathf.Abs(moveAmount.y) + skinWidth, collisionMask); // controls the bottom right raycast
        if (maxSlopeHitLeft ^ maxSlopeHitRight)
        {
            SlideDownMaxSlope(maxSlopeHitLeft, ref moveAmount); // slide down the slope if on the left

            SlideDownMaxSlope(maxSlopeHitRight, ref moveAmount); // slide down the slope if on the right
        }

        if (!collisions.slidingDownMaxSlope)
        {
            float directionX = Mathf.Sign(moveAmount.x); // direction x is equal to the sin value of move amount x

            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft; // rayorigin is the bottom right and bottom left of the object

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, collisionMask); //the hit is equal to the raycast from the origin 

            if (hit) // if theres a hit on the raycast
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up); // slope angle is the angle of hit.normal and a translation up

                if (slopeAngle != 0 && slopeAngle <= maxSlopeAngle) // if slope angle isnt 0 and slope angle is less than max slope angle then do the following
                {
                    if (Mathf.Sign(hit.normal.x) == directionX) // if the sin of hit.normal.x is equal to the direction of x do the following
                    {
                        if (hit.distance - skinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(moveAmount.x)) // if the hit distance minus skin width is less than or equal to the tan of slope angle times radian times the absolute value of move amount x then do the following
                        {
                            float moveDistance = Mathf.Abs(moveAmount.x); // move distance is the absolute value of move amount x

                            float descendmoveAmountY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance; // descend amount of y is equal to the sin of slope angle times the amount of radians times move distance

                            moveAmount.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(moveAmount.x); // move amount x is equal to the cos of slope angle times the radians,  times the move distance and then times the sin of move amount x

                            moveAmount.y -= descendmoveAmountY; // move amount y is subtracted by the descend move amount y

                            // sets the slopes to their respective values
                            collisions.slopeAngle = slopeAngle;
                            collisions.descendingSlope = true;
                            collisions.below = true;
                            collisions.slopeNormal = hit.normal;
                        }
                    }
                }
            }
        }
    }

    void SlideDownMaxSlope(RaycastHit2D hit, ref Vector2 moveAmount)
    {

        if (hit) // if raycast hit
        {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up); // slope angle is the angle between the raycast hit and the up translation
            if (slopeAngle > maxSlopeAngle) // if slope angle is less than max slope angle do the following
            {

                // move on the x axis, by the sin value of hit.normal.x times the absolute value of move amount y minus the hit distance, divided by the tan of slope angle times the radian value of the angle
                moveAmount.x = Mathf.Sign(hit.normal.x) * (Mathf.Abs(moveAmount.y) - hit.distance) / Mathf.Tan(slopeAngle * Mathf.Deg2Rad);

                // resets the slope angle collisions
                collisions.slopeAngle = slopeAngle;
                collisions.slidingDownMaxSlope = true;
                collisions.slopeNormal = hit.normal;
            }
        }

    }

    void ResetFallingThroughPlatform()
    {
        // makes it false that the player is falling through a platform 
        collisions.fallingThroughPlatform = false;
    }

    public struct CollisionInfo
    {
        // sets the respective values to their data types
        public bool above, below;
        public bool left, right;

        public bool climbingSlope;
        public bool descendingSlope;
        public bool slidingDownMaxSlope;

        public float slopeAngle, slopeAngleOld;
        public Vector2 slopeNormal;
        public Vector2 moveAmountOld;
        public int faceDir;
        public bool fallingThroughPlatform;

        public void Reset()
        {
            // resets the collisions to their default values
            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;
            slidingDownMaxSlope = false;
            slopeNormal = Vector2.zero;
            // slops are reset as well
            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
    

}
