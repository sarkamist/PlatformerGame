using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance { get; private set; }

    private readonly string titleScene = "Title";
    private readonly string gameplayScene = "Gameplay";
    private readonly string endingScene = "Ending";
    private string currentScene;

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

            currentScene = SceneManager.GetActiveScene().name;
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
        if (currentScene == titleScene && currentScene != gameplayScene)
        {
            SceneManager.LoadScene(gameplayScene);
        }
    }

    private void OnScoreUpdated(int score)
    {
        Debug.Log(score);
        if (score >= 15)
        {
            SceneManager.LoadScene(endingScene);
        }
    }

    private void OnPlayerDeath(GameObject player)
    {
        Debug.Log("test");
        SceneManager.LoadScene(endingScene);
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
