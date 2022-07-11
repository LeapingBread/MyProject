using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
public class SaveLoadManager : Singleton<SaveLoadManager>
{
    string jsonPath;
    List<ISaveble> savebles = new List<ISaveble> ();
    Dictionary<string, GameSaveData> saveDataDict = new Dictionary<string, GameSaveData> ();

    public void Register( ISaveble saveble)
    {
        savebles.Add(saveble);
    }
    protected override void Awake()
    {
        base.Awake();
        jsonPath = Application.persistentDataPath + "/SAVE/";
    }
    private void OnEnable()
    {
        EventSystem.StartNewGameEvent += OnStartNewGameEvent;
    }
    private void OnDisable()
    {
        EventSystem.StartNewGameEvent -= OnStartNewGameEvent;
    }
    void OnStartNewGameEvent(string scene)
    {
        var saveDataPath = jsonPath + "data.sav";
        if(File.Exists(saveDataPath))
        {
            File.Delete(saveDataPath);
        }
    }
    public void SaveData()
    {
        saveDataDict.Clear();
        foreach(var saveable in savebles)
        {
            saveDataDict.Add(saveable.GetType().Name,saveable.GenerateSaveData());
        }
        var saveDataPath = jsonPath + "data.sav";
        var jsonData = JsonConvert.SerializeObject(saveDataDict, Formatting.Indented);
        if(!File.Exists(saveDataPath))
        {
            Directory.CreateDirectory(jsonPath);
        }
        File.WriteAllText(saveDataPath, jsonData);
    }
    public void LoadData()
    {
        var saveDataPath = jsonPath + "data.sav";
        if (!File.Exists(saveDataPath)) return;
        var stringData = File.ReadAllText(saveDataPath);
        var jsonData = JsonConvert.DeserializeObject<Dictionary<string,GameSaveData>>(stringData);
        foreach(var saveable in savebles)
        {
            saveable.ResoreGameData(jsonData[saveable.GetType().Name]);
        }
        GameManager.Instance.enemies.Clear();
        EventSystem.CallResetPlayerHealthEvent();
        Time.timeScale = 1;
        GameManager.Instance.enemies.Clear();
        UIManager.Instance.BossHealthBarOff();
    }

}
