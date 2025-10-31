using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 6f;

    [Header("Jump")]
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public float groundCheckDistance = 1.1f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // --- 1. Ground check (raycast down) ---
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        // --- 2. Jump input ---
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // --- 3. Movement on XZ plane (same as before) ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(h, 0f, v);
        Vector3 delta = input.normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + delta);
    }

    // (Optional) draw the ray in Scene view for debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
