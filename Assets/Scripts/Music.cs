using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("PlaySong", 0.5f, clip.length);
    }

    void PlaySong(){
        audioSource.PlayOneShot(clip);
    }
}
