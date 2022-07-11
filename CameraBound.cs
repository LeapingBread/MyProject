using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CameraBound : MonoBehaviour
{

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
        if(currentscene.name != "Menu")
        SwitchBound();
    }
    void SwitchBound()
    {
        PolygonCollider2D confinerBound = GameObject.FindGameObjectWithTag("Bound").GetComponent<PolygonCollider2D>();
        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = confinerBound;
        confiner.InvalidatePathCache();
    }
}
