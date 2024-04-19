using UnityEngine;

public static class Extensions
{

    private static LayerMask layerMask = LayerMask.GetMask("Default");

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
        RaycastHit2D hit = Physics2D.CircleCast(rb.position, radius, direction, distance, layerMask);

        // Return if ray hits any collider and is not the rigidbody that is defined above
        return hit.collider != null && hit.rigidbody != rb;

    }
}
