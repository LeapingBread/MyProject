using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventSystem 
{
    public static event Action<string, string> SceneTrasionEvent;
    public static void CallSceneTrasionEvent(string from,string to)
    {
        SceneTrasionEvent.Invoke(from, to);
    }
    public static event Action BeforeSceneUnload;
    public static void CallBeforeSceneUnload()
    {
        BeforeSceneUnload?.Invoke();
    }
    public static event Action AfterSceneLoaded;
    public static void CallAfterSceneLoaded()
    {
        AfterSceneLoaded?.Invoke();
    }
    public static event Action ResetPlayerHealthEvent;
    public static void CallResetPlayerHealthEvent()
    {
        ResetPlayerHealthEvent?.Invoke();
    }

    public static event Action<SoundDtials> InitSoundEffect;
    public static void CallInitSoundEffect(SoundDtials soundDetial)
    {
        InitSoundEffect?.Invoke(soundDetial);
    }
    public static event Action<SoundName> PlaySoundEvent;
    public static void CallPlaySoundEvent(SoundName soundName)
    {
        PlaySoundEvent?.Invoke(soundName);
    }
    public static event Action <string>StartNewGameEvent;
    public static void CallStartNewGameEvent( string startScene)
    {
        StartNewGameEvent?.Invoke(startScene);
    }
    public static event Action<GameState> GameStateEvent;
    public static void CallGameStateEvent(GameState gameState)
    {
        GameStateEvent?.Invoke(gameState);
    }
    public static event Action<DialoguePiece> ShowDialogueUIEvent;
    public static void CallShowDialogueUIEvent(DialoguePiece dialoguePiece)
    {
        ShowDialogueUIEvent?.Invoke(dialoguePiece);
    }
    public static event Action DialogueButtonEvent;
    public static void CallDialogueButtonEvent()
    {
        DialogueButtonEvent?.Invoke();
    }
    public static event Action<int> AddHealthEvent;
    public static void CallAddHealthEvent(int amount)
    {
        AddHealthEvent?.Invoke(amount);
    }
    public static event Action<int> UpdateHealthUIEvent;
    public static void CallUpdateHealthUIEvent(int currenthealth)
    {
        UpdateHealthUIEvent?.Invoke(currenthealth);
    }
    public static event Action<bool> ShowGameFinishedEvent;
    public static void CallShowGameFinishedEvent(bool gameFinished)
    {
        ShowGameFinishedEvent?.Invoke(gameFinished);
    }
}
