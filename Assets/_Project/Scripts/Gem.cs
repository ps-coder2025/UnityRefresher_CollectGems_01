using UnityEngine;

public class Gem : MonoBehaviour
{
    [Header("FX")]
    public AudioClip pickupSfx;

    // Cache components for efficiency
    private Collider _collider;
    private Renderer[] _renderers;
    private bool _collected;

    void Awake()
    {
        _collider = GetComponent<Collider>();
        _renderers = GetComponentsInChildren<Renderer>(includeInactive: false);
    }

    // Called by PlayerCollector
    public bool Collect()
    {
        if (_collected) return false;
        _collected = true;

        // Disable trigger + visuals immediately
        if (_collider) _collider.enabled = false;
        for (int i = 0; i < _renderers.Length; i++)
            _renderers[i].enabled = false;

        // Play sound at gem position
        if (pickupSfx)
            AudioSource.PlayClipAtPoint(pickupSfx, transform.position);

        // Destroy shortly after
        Destroy(gameObject, 0.05f);
        return true;
    }
}
