using UnityEngine;
using System.Collections;

public class RespawnOnHit : MonoBehaviour
{
    public Transform spawnPoint;
    public string enemyTag = "Enemy";
    public float invulnTime = 1f;
    public float flickerHz = 12f;

    Rigidbody rb;
    Renderer[] rends;
    bool invuln;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rends = GetComponentsInChildren<Renderer>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (!invuln && col.collider.CompareTag(enemyTag))
            StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        invuln = true;
        rb.linearVelocity = Vector3.zero; rb.angularVelocity = Vector3.zero;
        if (spawnPoint) transform.position = spawnPoint.position;

        float t = 0f;
        while (t < invulnTime)
        {
            bool on = Mathf.FloorToInt(t * flickerHz) % 2 == 0;
            foreach (var r in rends) r.enabled = on;
            t += Time.deltaTime;
            yield return null;
        }
        foreach (var r in rends) r.enabled = true;
        invuln = false;
    }
}
