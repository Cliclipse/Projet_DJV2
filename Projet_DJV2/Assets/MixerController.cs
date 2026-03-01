using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    
    public void SetSFXSound(float db)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(db) * 20);
    }
    public void SetMusicSound(float db)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(db) * 20);
    }
    public void SetMasterSound(float db)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(db) * 20);
    }
}
