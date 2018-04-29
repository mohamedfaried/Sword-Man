using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class setting : MonoBehaviour {
    public AudioMixer audiomixer;
    public AudioMixer m;
	public void setvolume(float volume)
    {
        audiomixer.SetFloat("volume", volume);
    }
    public void setmusic(float music)
    {
        print(music);
        m.SetFloat("music", music);
    }
}
