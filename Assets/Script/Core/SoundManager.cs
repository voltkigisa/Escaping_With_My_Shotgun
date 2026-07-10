using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Sound Effects")]
    public AudioClip sfxJalan;
    public AudioClip sfxJatuh;
    public AudioClip sfxMendarat;
    public AudioClip sfxShotgun;
    public AudioClip sfxReload;

    private AudioSource audioSource;
    private AudioSource footstepSource; // terpisah untuk suara jalan agar bisa loop
    private float footstepTimer = 0f;
    public float footstepDelay = 0.4f; //

    void Awake()
    {
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

        AudioSource[] sources = GetComponents<AudioSource>();
        audioSource = sources[0];
        footstepSource = sources[1];
        footstepSource.loop = true;
        footstepSource.clip = sfxJalan;
    }

    public void PlayJalan()
    {
        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0f)
        {
            audioSource.PlayOneShot(sfxJalan);
            footstepTimer = footstepDelay;
        }
    }

    public void StopJalan()
    {
        footstepTimer = 0f;
    }

    public void PlayJatuh() => audioSource.PlayOneShot(sfxJatuh);
    public void PlayMendarat() => audioSource.PlayOneShot(sfxMendarat);
    public void PlayShotgun() => audioSource.PlayOneShot(sfxShotgun);
    public void PlayReload() => audioSource.PlayOneShot(sfxReload);
}