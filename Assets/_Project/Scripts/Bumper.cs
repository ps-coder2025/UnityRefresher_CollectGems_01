using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float knockbackForce = 8f;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            Vector3 dir = (other.transform.position - transform.position);

            // keep knockback flat on the ground
            dir.y = 0f;
            dir = dir.normalized;

            rb.AddForce(dir * knockbackForce, ForceMode.Impulse);

            // optional safety cap
            if (rb.linearVelocity.magnitude > 10f)
                rb.linearVelocity = rb.linearVelocity.normalized * 10f;
        }
    }
}
