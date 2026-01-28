using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void OnStartGame()
    {
        if (currentScene == titleScene && currentScene != gameplayScene)
        {
            SceneManager.LoadScene(gameplayScene);
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
