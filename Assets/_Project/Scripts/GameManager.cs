using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public GameObject winPanel, losePanel, pausePanel;
    public TextMeshProUGUI timerText;

    int totalGems;
    int collected;
    bool paused, ended;
    float timer;

    void Awake() { if (I == null) I = this; else Destroy(gameObject); }

    void Start()
    {
        totalGems = GameObject.FindGameObjectsWithTag("Gem").Length;
        collected = 0; ended = false; paused = false; timer = 0f;
        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(false);
    }

    void Update()
    {
        if (ended) { if (Input.GetKeyDown(KeyCode.R)) Restart(); return; }

        if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();

        if (!paused)
        {
            timer += Time.deltaTime;
            if (timerText) timerText.text = $"Time: {timer:0.0}s";
        }
    }

    public void OnGemCollected()
    {
        if (ended) return;
        collected++;
        if (collected >= totalGems) Win();
    }

    public void Lose()
    {
        if (ended) return; ended = true;
        if (losePanel) losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void Win()
    {
        ended = true;
        if (winPanel) winPanel.SetActive(true);
        // Save best time
        float best = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        if (timer < best) PlayerPrefs.SetFloat("BestTime", timer);
        Time.timeScale = 0f;
    }

    void TogglePause()
    {
        if (ended) return;
        paused = !paused;
        if (pausePanel) pausePanel.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
