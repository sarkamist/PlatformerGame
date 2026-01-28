using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance { get; private set; }

    private readonly string titleScene = "Title";
    private readonly string gameplayScene = "Gameplay";
    private readonly string endingScene = "Ending";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        ScoreSystem.OnScoreUpdated += OnScoreUpdated;
        PlayerDeathBehaviour.OnPlayerDeath += OnPlayerDeath;
    }
    private void OnDisable()
    {
        ScoreSystem.OnScoreUpdated -= OnScoreUpdated;
        PlayerDeathBehaviour.OnPlayerDeath -= OnPlayerDeath;
    }
    public void OnStartGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == titleScene && currentScene != gameplayScene)
        {
            SceneManager.LoadScene(gameplayScene);
        }
    }

    private void OnScoreUpdated(int score)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == gameplayScene && score >= 15)
        {
            SceneManager.LoadScene(endingScene);
        }
    }
    private void OnPlayerDeath(GameObject player)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == gameplayScene)
        {
            SceneManager.LoadScene(endingScene);
        }
    }

    private void OnExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
