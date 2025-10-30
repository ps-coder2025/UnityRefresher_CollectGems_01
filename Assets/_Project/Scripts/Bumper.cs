using System.Collections;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float knockbackForce = 8f;
    public float recoveryDrag = 8f;   // temporary drag applied after hit
    public float dragDuration = 0.3f; // time before restoring normal drag

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            // Calculate push direction
            Vector3 dir = (other.transform.position - transform.position).normalized;

            // Apply the knockback impulse
            rb.AddForce(dir * knockbackForce, ForceMode.Impulse);

            // Start coroutine from *this* script (not the Rigidbody)
            StartCoroutine(RestoreDrag(rb));
        }
    }

    private IEnumerator RestoreDrag(Rigidbody rb)
    {
        float originalDrag = rb.linearDamping;
        rb.linearDamping = recoveryDrag;
        yield return new WaitForSeconds(dragDuration);
        rb.linearDamping = originalDrag;
    }
}
