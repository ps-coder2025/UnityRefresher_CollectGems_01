using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2f;

    [HideInInspector] public Vector3 frameDelta; // how much the platform moved this frame

    private Vector3 target;
    private Vector3 lastPos;

    void Start()
    {
        // Pick a direction to start moving
        target = pointB;
        lastPos = transform.position;
    }

    void Update()
    {
        // 1. Remember where we were at the start of the frame
        Vector3 before = transform.position;

        // 2. Move toward the current target
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        // 3. How far did we move this frame?
        frameDelta = transform.position - before;

        // 4. If we reached the target, flip it
        if (Vector3.Distance(transform.position, target) < 0.2f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }
}
