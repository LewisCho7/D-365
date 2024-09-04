using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public AudioClip[] bgmClip;
    public AudioClip sfxClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(int index)
    {
        if (index >= 0 && index < bgmClip.Length)
        {
            bgmSource.clip = bgmClip[index];
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void ClickSound()
    {
        sfxSource.PlayOneShot(sfxClip);
    }
}