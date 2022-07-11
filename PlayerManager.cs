using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    Transform swapPoint;
    private void OnEnable()
    {
        EventSystem.AfterSceneLoaded += OnAfterSceneLoaded;
    }
    private void OnDisable()
    {
        EventSystem.AfterSceneLoaded -= OnAfterSceneLoaded;
    }
    void OnAfterSceneLoaded()
    {
        
        Scene currentscene = SceneManager.GetActiveScene();
        if (currentscene.name != "Menu")
        {
            swapPoint = GameObject.FindWithTag("SwapPoint").transform;
            player.SetActive(true);
            player.transform.position = swapPoint.position;
        }
    }
}
