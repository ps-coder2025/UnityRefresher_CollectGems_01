using UnityEngine;

public class EnemyPatrolChase : MonoBehaviour
{
    public Transform[] waypoints;       // set in Inspector
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float arriveDist = 0.2f;

    [Header("Detection")]
    public Transform player;            // drag Player here
    public float detectRadius = 5f;     // start chasing within this distance
    public float loseRadius = 7f;       // stop chasing when farther than this

    private int wpIndex = 0;
    private bool chasing = false;

    void Update()
    {
        if (player)
        {
            float d = Vector3.Distance(transform.position, player.position);
            if (!chasing && d <= detectRadius) chasing = true;
            if (chasing && d >= loseRadius) chasing = false;
        }

        if (chasing && player)
        {
            MoveTowards(player.position, chaseSpeed);
        }
        else
        {
            // Patrol between waypoints
            if (waypoints == null || waypoints.Length == 0) return;
            Vector3 target = waypoints[wpIndex].position;
            MoveTowards(target, patrolSpeed);

            if (Vector3.Distance(transform.position, target) <= arriveDist)
            {
                wpIndex = (wpIndex + 1) % waypoints.Length;
            }
        }
    }

    private void MoveTowards(Vector3 target, float speed)
    {
        // Keep motion flat on ground (XZ only)
        target.y = transform.position.y;
        transform.position = Vector3.MoveTowards(
            transform.position, target, speed * Time.deltaTime);
        // Face movement dir (optional)
        Vector3 dir = (target - transform.position);
        if (dir.sqrMagnitude > 0.001f)
            transform.forward = Vector3.Lerp(transform.forward, dir.normalized, 10f * Time.deltaTime);
    }

    // Debug helper
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, loseRadius);
    }
}
