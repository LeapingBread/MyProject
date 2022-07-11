using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    [SerializeField]AudioSource audio;
   
    public void SetSound(SoundDtials soud)
    {
        audio.clip = soud.audioClip;
        audio.volume = soud.soundVolume;
        audio.pitch = Random.Range(soud.PitchMin,soud.PitchMax);
    }
}
