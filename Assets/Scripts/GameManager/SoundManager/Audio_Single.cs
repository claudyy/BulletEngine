using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Single : BaseAudio {

    public override void Awake()
    {
        base.Awake();
        source.outputAudioMixerGroup = manager.mixer.FindMatchingGroups("FX")[0];
    }
    public override void Start()
    {
        base.Start();
    }
}
