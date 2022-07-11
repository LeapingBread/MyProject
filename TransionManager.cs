using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransionManager : Singleton<TransionManager>,ISaveble
{
    [SerializeField] string startScene;
    [SerializeField] CanvasGroup fadeCanvasGroup;
    [SerializeField] float fadeDuartion;
    bool isFading;
    
    private void OnEnable()
    {
        EventSystem.SceneTrasionEvent += SceneChange;
        EventSystem.StartNewGameEvent += OnStartNewGameEvent;
    }
    private void OnDisable()
    {
        EventSystem.SceneTrasionEvent -= SceneChange;
        EventSystem.StartNewGameEvent -= OnStartNewGameEvent;
    }
   
    void Start()
    {
        ISaveble saveble = this;
        saveble.Register();
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
    }
    void OnStartNewGameEvent(string startLevelScene)
    {
        SceneChange("Menu", startLevelScene);
        Time.timeScale = 1;
    }
    public void SceneChange(string fromScene, string toScene)
    {
        if (!isFading)
        {
            
            StartCoroutine(SceneChangeRoutin(fromScene, toScene));
            
        }
    }
    IEnumerator SceneChangeRoutin(string from, string to)
    {
        yield return FadeRoutine(1f);
        if (from != string.Empty)
        {
            EventSystem.CallBeforeSceneUnload();
            SaveLoadManager.Instance.SaveData();
            
            yield return SceneManager.UnloadSceneAsync(from);
        }
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
        EventSystem.CallAfterSceneLoaded();
        yield return FadeRoutine(0f);
    }
    IEnumerator FadeRoutine(float targetAlpha)
    {
        isFading = true;
        fadeCanvasGroup.blocksRaycasts = true;
        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuartion;

        while(!Mathf.Approximately(fadeCanvasGroup.alpha,targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed *Time.fixedDeltaTime);
            yield return null;
        }
        fadeCanvasGroup.blocksRaycasts = false;
        isFading = false;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData gameSaveData = new GameSaveData();
        gameSaveData.currentScene = SceneManager.GetActiveScene().name;
        return gameSaveData;
    }

    public void ResoreGameData(GameSaveData data)
    {
        SceneChange("Menu", data.currentScene);
    }
}
