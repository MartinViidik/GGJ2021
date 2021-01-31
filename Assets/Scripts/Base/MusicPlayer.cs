using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string MusicEvent = "";
    FMOD.Studio.EventInstance musicPlayer;

    [FMODUnity.EventRef]
    public string AtmosphereEvent = "";
    FMOD.Studio.EventInstance atmospherePlayer;

    void Start()
    {
        musicPlayer = FMODUnity.RuntimeManager.CreateInstance(MusicEvent);
        musicPlayer.start();

        atmospherePlayer = FMODUnity.RuntimeManager.CreateInstance(AtmosphereEvent);
        atmospherePlayer.start();
    }

}
