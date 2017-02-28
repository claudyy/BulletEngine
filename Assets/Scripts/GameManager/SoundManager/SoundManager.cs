using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class SoundManager : BaseManager
{
    public List<BaseAudio> audioSourceList = new List<BaseAudio>();
    Transform audioParent;
    public AudioMixer mixer { get; private set; }
    int currentSong;
    Audio_Music mainMusic;
    SoundManagerData data;
    public override void Init()
    {
        base.Init();
        mixer = Resources.Load("Sound/Mixer") as AudioMixer;

        GameObject go = new GameObject();
        go.name = "Sound";
        go.transform.parent = GameManager.instance.transform;
        audioParent = go.transform;

        //music
        mainMusic = GetMusicAudio();
        data = Resources.Load<SoundManagerData>("Data/Manager/SoundManager");
        ChangeMusic(0);
    }
    public void ChangeMusic(int i)
    {
        currentSong = i;
        if (i >= data.musicList.Count) { Debug.Log(i + " music index does not exist");return; }
        mainMusic.ChangeAudioClip(data.musicList[currentSong]);
        mainMusic.Play();
    }
    public override void Tick()
    {
        base.Tick();
        for (int i = 0; i < audioSourceList.Count; i++)
        {
            audioSourceList[i].Tick();
        }
    }
    public void SingleAudio(AudioClip clip, float volume = 1, float randomPitch = 0, float randomVolume = 0)
    {
        Audio_Single audioTemp = GetSingleAudio();
        audioTemp.ChangeAudioClip(clip);
        audioTemp.Play(volume, randomPitch, randomVolume);
        //audioTemp
    }

    Audio_Single GetSingleAudio()
    {
        for (int i = 0; i < audioSourceList.Count; i++)
        {
            if (audioSourceList[i] is Audio_Single)
            {
                if (audioSourceList[i].gameObject.activeInHierarchy == false)
                {
                    return audioSourceList[i].GetComponent<Audio_Single>();
                }
            }

        }

        GameObject go = new GameObject();
        go.name = "SingleSound";
        go.transform.parent = audioParent;
        go.AddComponent<AudioSource>();
        go.AddComponent<Audio_Single>();
        audioSourceList.Add(go.GetComponent<Audio_Single>());
        return go.GetComponent<Audio_Single>();

    }
    Audio_Music GetMusicAudio()
    {

        GameObject go = new GameObject();
        go.name = "MusicSound";
        go.transform.parent = audioParent;
        go.AddComponent<AudioSource>();
        go.AddComponent<Audio_Music>();
        audioSourceList.Add(go.GetComponent<Audio_Music>());
        return go.GetComponent<Audio_Music>();

    }
}
