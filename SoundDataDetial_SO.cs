using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="SoundDataDetial_SO",menuName = "SoundDataDetial_SO")]
public class SoundDataDetial_SO : ScriptableObject
{
    public List<SoundDtials> soudDetialsList;
    public SoundDtials GetSoundDtials (SoundName name)
    {
        return soudDetialsList.Find(s => s.soundName == name);
    }
}
[System.Serializable]
public class SoundDtials
{
    public SoundName soundName;
    public AudioClip audioClip;
    public float PitchMin;
    public float PitchMax;
    public float soundVolume;
}