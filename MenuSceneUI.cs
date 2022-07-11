using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSceneUI : MonoBehaviour
{
    [SerializeField] SoundName buttonSoundFX;
    [SerializeField] Button loadButoon;
    [SerializeField] Button EasyButton;
    private void Start()
    {
        EasyButton.interactable = true;
    }

    public void StartEasyGame()
    { 
        EasyButton.interactable = false;
        
        EventSystem.CallStartNewGameEvent("L1");
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ContinuGmae()
    {
        
       
            SaveLoadManager.Instance.LoadData();

           
    }
    public void playButtonSoundEvent()
    {
        EventSystem.CallPlaySoundEvent(buttonSoundFX);
    }
}
