using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAudio : BaseTick {

    protected AudioSource source;
    protected bool isPaused;
    public bool isPlaying;
    protected SoundManager manager;
    // Use this for initialization
    public override void Awake()
    {
        base.Awake();
        source = GetComponent<AudioSource>();
        manager = GameManager.instance.soundManager ;
    }

    // Update is called once per frame
    public override void Tick()
    {
        base.Tick();
        if (gameObject.activeInHierarchy == true)
        {
            if (source.isPlaying == false && isPaused == false)
            {
                gameObject.SetActive(false);
            }
        }
        isPlaying = source.isPlaying;
    }
    public override void Pause(bool pause)
    {
        base.Pause(pause);
        isPaused = pause;
        if (pause)
        {
            source.Pause();

        }
        else
        {
            source.UnPause();
        }

    }
    public virtual void ChangeAudioClip(AudioClip clip)
    {
        if (source.clip == clip) return;
        source.clip = clip;

    }
    public virtual void Play(float volume = 1, float randomPitch = 0, float randomVolume = 0)
    {
        gameObject.SetActive(true);
        source.volume = volume + Random.Range(-randomVolume, randomVolume);
        source.pitch = 1 + Random.Range(-randomPitch, randomPitch);
        source.Play();
    }
    public virtual void Stop()
    {
        source.Stop();
    }
}
