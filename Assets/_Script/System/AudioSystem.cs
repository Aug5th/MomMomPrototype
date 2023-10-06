using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : Singleton<AudioSystem>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip clip , Vector3 position , float vol = 1)
    {
        _soundSource.transform.position = position;
        PlaySound(clip, vol);
    }

    private void PlaySound(AudioClip clip, float vol)
    {
        _soundSource.PlayOneShot(clip,vol);
    }
}
