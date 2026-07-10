using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM")]
    public AudioClip bgmMainMenu;
    public AudioClip bgmInGame;

    private AudioSource audioSource;

    void Awake()
    {
        // Singleton - jangan destroy saat pindah scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    public void PlayMainMenu()
    {
        if (audioSource.clip == bgmMainMenu && audioSource.isPlaying) return;
        audioSource.clip = bgmMainMenu;
        audioSource.Play();
    }

    public void PlayInGame()
    {
        if (audioSource.clip == bgmInGame && audioSource.isPlaying) return;
        audioSource.clip = bgmInGame;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
}