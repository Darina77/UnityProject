using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoneSound : MonoBehaviour {

    public AudioClip music = null;
    AudioSource musicSource = null;
    void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = music;
        musicSource.loop = true;
        musicSource.volume = 0.05f;
        musicSource.priority = 255;

        if(SoundManager.Instance.isVolumeOn()) musicSource.Play();
    }

    void FixedUpdate()
    {
        if (!SoundManager.Instance.isVolumeOn())
        {
            musicSource.Stop();
        }
        else if (!musicSource.isPlaying) musicSource.Play();
    }
}
