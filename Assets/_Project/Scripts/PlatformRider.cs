using UnityEngine;

[RequireComponent(typeof(MovingPlatform))]
public class PlatformRider : MonoBehaviour
{
    private MovingPlatform platform;

    void Awake()
    {
        platform = GetComponent<MovingPlatform>();
    }

    private void OnCollisionStay(Collision collision)
    {
        // Only affect the player if they're standing on the platform
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody rb = collision.collider.attachedRigidbody;
            if (rb != null)
            {
                // Add the platform's movement to the player
                rb.MovePosition(rb.position + platform.frameDelta);
            }
        }
    }
}
