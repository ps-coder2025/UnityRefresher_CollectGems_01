using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public TextMeshProUGUI healthText;
    int health;

    void Start() { health = maxHealth; UpdateUI(); }

    public void Damage(int amount)
    {
        health = Mathf.Max(0, health - amount);
        UpdateUI();
        if (health <= 0) GameManager.I?.Lose();
    }

    void UpdateUI() { if (healthText) healthText.text = $"HP: {health}"; }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Enemy"))
            Damage(1);
    }
}
