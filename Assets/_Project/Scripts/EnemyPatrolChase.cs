using UnityEngine;

public class EnemyPatrolChase : MonoBehaviour
{
    public Transform[] waypoints;
    public float patrolSpeed = 2f, chaseSpeed = 3.5f, arriveDist = 0.2f;

    [Header("Detection")]
    public Transform player;
    public float detectRadius = 5f, loseRadius = 7f;
    public LayerMask losObstructionMask; // walls/obstacles layers

    [Header("Feedback")]
    public Renderer bodyRenderer;        // drag enemy Renderer
    public Color patrolColor = Color.red * 0.6f;
    public Color chaseColor = Color.red;
    public AudioSource audioSource;      // drag AudioSource
    public AudioClip chaseSfx;           // alert clip

    int wpIndex = 0;
    bool chasing = false;

    void Start()
    {
        if (!bodyRenderer) bodyRenderer = GetComponentInChildren<Renderer>();
        SetColor(patrolColor);
    }

    void Update()
    {
        bool shouldChase = false;
        if (player)
        {
            float d = Vector3.Distance(transform.position, player.position);
            if (d <= detectRadius && HasLineOfSight()) shouldChase = true;
            if (chasing && d >= loseRadius) shouldChase = false;
        }

        if (chasing != shouldChase)
        {
            chasing = shouldChase;
            if (chasing)
            {
                SetColor(chaseColor);
                if (audioSource && chaseSfx) audioSource.PlayOneShot(chaseSfx);
            }
            else SetColor(patrolColor);
        }

        if (chasing && player) MoveTowards(player.position, chaseSpeed);
        else Patrol();
    }

    bool HasLineOfSight()
    {
        Vector3 from = transform.position + Vector3.up * 0.8f;
        Vector3 to = player.position + Vector3.up * 0.8f;
        if (Physics.Raycast(from, (to - from).normalized, out RaycastHit hit, Mathf.Infinity, ~0))
        {
            // blocked if we hit something that's in the obstruction mask and not the player
            if (hit.collider.transform != player && ((1 << hit.collider.gameObject.layer) & losObstructionMask) != 0)
                return false;
        }
        return true;
    }

    void Patrol()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        Vector3 target = waypoints[wpIndex].position;
        MoveTowards(target, patrolSpeed);
        if (Vector3.Distance(transform.position, target) <= arriveDist)
            wpIndex = (wpIndex + 1) % waypoints.Length;
    }

    void MoveTowards(Vector3 target, float speed)
    {
        target.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        Vector3 dir = target - transform.position;
        if (dir.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), 360f * Time.deltaTime);
    }

    void SetColor(Color c)
    {
        if (bodyRenderer && bodyRenderer.material.HasProperty("_Color"))
            bodyRenderer.material.color = c;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, detectRadius);
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, loseRadius);
    }
}
