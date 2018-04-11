using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler : MonoBehaviour {

    public AudioMixer mixer;

	public void MusicVolume(float soundLevel)
    {
        mixer.SetFloat("musicVol", soundLevel);
    }

    public void SfxVolume(float soundLevel)
    {
        mixer.SetFloat("sfxVol", soundLevel);
    }
}
