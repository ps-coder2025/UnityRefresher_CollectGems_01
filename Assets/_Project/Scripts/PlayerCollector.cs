using UnityEngine;
using TMPro;


public class PlayerCollector : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    private void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: 0";
        }
    }
        private void OnTriggerEnter(Collider other)
    {
        {
            if (other.CompareTag("Gem"))
            {
                Destroy(other.gameObject);
                score++;
                if (scoreText != null)
                {
                    scoreText.text = $"Score: {score}";
                }
            }
        }
    }
}

