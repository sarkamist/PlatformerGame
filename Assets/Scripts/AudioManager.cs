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
}
