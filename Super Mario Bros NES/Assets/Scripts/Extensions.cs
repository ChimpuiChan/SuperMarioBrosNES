using Unity.VisualScripting;
using UnityEngine;

public static class Extensions
{

    private static LayerMask layerMask = LayerMask.GetMask("Default");

    // Raycasting to check if grounded
    public static bool Raycast(this Rigidbody2D rb, Vector2 direction)
    {
        // Kinematic means it is not affected by external forces
        if (rb.isKinematic)
        {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.375f;

        // CircleCast parameters: position of rigidbody defined as rb, radius and distance defined above, direction is Vector2 that will be given as a parameter when this Raycast funtion is used in any other script and layerMask is given "Default" which only returns something if raycast hits objects on "Default" layer, while Mario is set to "Player" layer so raycast ignores Mario's collider
        RaycastHit2D hit = Physics2D.CircleCast(rb.position, radius, direction.normalized, distance, layerMask);

        // Return if ray hits any collider and is not the rigidbody that is defined above
        return hit.collider != null && hit.rigidbody != rb;

    }

    // Returns 1 if directions are same, 0 if they are perpendicular, -1 if they are opposite to each other
    public static bool DotProd(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        // > 0.25f so we can hit the block from the edge and not just perfectly from the bottom such as when we keep > 1f, so > 0.25f actually gives some flexibility
        // Normalized the direction so its magnitude is always 1 so that it just specifies the direction
        // The dot product of the two vectors are calculated here. Resulting in a float value
        // x1 * x2 + y1 * y2 is the formula for dot product calculation
        // The testDirection is the direction that is intended for this to return true
        // It compared the calculated direction with the testDirection and returns if they are same, perpendicular or opposite to each other
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }
}
