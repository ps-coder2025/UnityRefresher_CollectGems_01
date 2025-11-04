using UnityEngine;
using TMPro;

public class PlayerCollector : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;

    private int score = 0;

    private void Start()
    {
        UpdateScoreText();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only react to Gems
        if (!other.CompareTag("Gem")) return;

        // Use the Gem script to handle collection logic
        if (other.TryGetComponent<Gem>(out var gem))
        {
            // Collect() returns true only the first time
            if (gem.Collect())
            {
                score++;
                UpdateScoreText();

                // Notify GameManager to check win condition
                GameManager.I?.OnGemCollected();
            }
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }
}
