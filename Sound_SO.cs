using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound_SO", menuName = "Sound_SO")]
public class Sound_SO: ScriptableObject
{
    public List<Sound> soundList;
    public Sound getSound(string sceneName)
    {
        return soundList.Find(n => n.sceneName == sceneName);
    }

}
[System.Serializable]
public class Sound
{
    public string sceneName;
    public SoundName ambient;
    public SoundName music;
}
