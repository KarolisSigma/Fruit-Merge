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
        source.PlayOneShot(mergeClip);
    }
    public void PlayPop(){
        source.PlayOneShot(popClip);
    }
}
