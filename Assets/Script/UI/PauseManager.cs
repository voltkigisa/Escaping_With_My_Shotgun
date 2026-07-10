using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Referensi")]
    public GameObject canvasPause;

    private bool isPaused = false;

    void Start()
    {
        canvasPause.SetActive(false);
    }

    // Dipanggil dari tombol Pause
    public void OnPauseButton()
    {
        isPaused = true;
        canvasPause.SetActive(true);
        Time.timeScale = 0f;
    }

    // Dipanggil dari tombol Resume
    public void OnResumeButton()
    {
        isPaused = false;
        canvasPause.SetActive(false);
        Time.timeScale = 1f;
    }

    // Dipanggil dari tombol Back to Main Menu
    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlayMainMenu();
        SceneManager.LoadScene("Main"); 
    }
}