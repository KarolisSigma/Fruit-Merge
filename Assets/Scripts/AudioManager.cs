using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource source;
    public AudioClip popClip;
    public AudioClip mergeClip;
    void Start(){
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlayMerge(){
        RandomizePitchAndVolume();
        source.PlayOneShot(mergeClip);
    }
    public void PlayPop(){
        RandomizePitchAndVolume();
        source.PlayOneShot(popClip);
    }

    void RandomizePitchAndVolume(){
        source.pitch = Random.Range(0.8f, 1.2f);
        source.volume = Random.Range(0.9f, 1.1f);
    }
}
