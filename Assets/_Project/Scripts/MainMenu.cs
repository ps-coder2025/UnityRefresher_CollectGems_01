using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "MainScene";
    public TextMeshProUGUI bestTimeText;

    void Start()
    {
        float best = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        if (bestTimeText) bestTimeText.text = (best == float.MaxValue) ? "Best Time: --.- s" : $"Best Time: {best:0.0}s";
        Time.timeScale = 1f; // ensure unpaused
    }

    public void PlayGame() => SceneManager.LoadScene(gameSceneName);
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
