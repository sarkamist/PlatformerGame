using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    public AudioSource SFXSource;
    public AudioSource MusicSource;

    [Header("SFX")]
    public AudioClip CoinPickUp;
    public AudioClip Death;
    public AudioClip JumpStart;
    public AudioClip DoubleJump;
    public AudioClip PowerUpUse;

    [Header("Music")]
    public AudioClip BackgroundMusic;

    void Awake()
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

    private void Start()
    {
        PlayMusic(BackgroundMusic, true);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (MusicSource == null || clip == null) return;
        MusicSource.clip = clip;
        MusicSource.loop = loop;
        MusicSource.volume = 0.3f;
        MusicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (SFXSource == null) return;
        SFXSource.PlayOneShot(clip, volume);
    }
}
