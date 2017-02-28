using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Music : BaseAudio{
    public string mixerGroup = "Music";
    public override void Awake()
    {
        base.Awake();
        source.loop = true;
        source.Play();
        source.outputAudioMixerGroup = manager.mixer.FindMatchingGroups(mixerGroup)[0];
    }
    public override void Tick()
    {
        base.Tick();
    }

    public override void Play(float volume = 1, float randomPitch = 0, float randomVolume = 0)
    {
        gameObject.SetActive(true);
        source.Play();
    }
    public void ChangeMixer(string _mixerGroup)
    {
        mixerGroup = _mixerGroup;
        source.outputAudioMixerGroup = manager.mixer.FindMatchingGroups(mixerGroup)[0];
    }
}
