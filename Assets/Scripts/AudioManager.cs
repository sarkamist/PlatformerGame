using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource SfxSource;
    public AudioSource MusicSource;

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

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (MusicSource == null || clip == null) return;
        MusicSource.clip = clip;
        MusicSource.loop = loop;
        MusicSource.Play();
    }
    public void StopMusic()
    {
        if (MusicSource == null) return;
        MusicSource.Stop();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (SfxSource == null) return;
        SfxSource.PlayOneShot(clip, volume);
    }
}
