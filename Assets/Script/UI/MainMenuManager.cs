using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string gameSceneName = "Level";

    void Start()
    {
        AudioManager.Instance.PlayMainMenu();
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene(gameSceneName);
        AudioManager.Instance.PlayInGame();
    }

    public void OnExitButton()
    {
        Application.Quit();

        // Ini untuk testing di Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}