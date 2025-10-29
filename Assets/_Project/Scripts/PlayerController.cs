using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

     void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(h, 0, v);

        Vector3 delta = input.normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + delta);
    }

}
