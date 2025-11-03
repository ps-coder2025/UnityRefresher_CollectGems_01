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
        if (other.CompareTag("Gem"))
        {
            AudioSource gemAudio = other.GetComponent<AudioSource>();

            if (gemAudio != null && gemAudio.clip != null)
            {
                // Play the gem sound, then destroy after it finishes
                gemAudio.Play();
                Destroy(other.gameObject, gemAudio.clip.length);
            }
            else
            {
                // No audio, destroy instantly
                Destroy(other.gameObject);
            }

            // Increment and update score
            score++;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}
