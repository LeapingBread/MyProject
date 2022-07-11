using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] List<GameObject> FX = new List<GameObject>();
    Queue<GameObject> soundQueue = new Queue<GameObject>();
    private void OnEnable()
    {
        EventSystem.InitSoundEffect += InitSoundEffect;
    }
    private void OnDisable()
    {
        EventSystem.InitSoundEffect -= InitSoundEffect;
    }
    void CreatSoundPool()
    {
        var parent = new GameObject(FX[0].name).transform;
        parent.SetParent(transform);
        for(int i = 0; i < 20; i++)
        {
            GameObject newObj = Instantiate(FX[0], parent);
            newObj.SetActive(false);
            soundQueue.Enqueue(newObj);
        }
    }
    GameObject GetPoolObject()
    {
        if (soundQueue.Count < 2)
            CreatSoundPool();
        return soundQueue.Dequeue();
    }
    void InitSoundEffect(SoundDtials soundDtials)
    {
        var obj = GetPoolObject();
        obj.GetComponent<SoundFX>().SetSound(soundDtials);
        obj.SetActive(true);
        StartCoroutine(DisableSound(obj, soundDtials.audioClip.length));
    }
    IEnumerator DisableSound(GameObject obj, float duration)
    {
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
        soundQueue.Enqueue(obj);
    }
}
