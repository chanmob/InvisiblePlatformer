using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip _clip)
    {
        audioSource.PlayOneShot(_clip);
    }
}
