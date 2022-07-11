using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    BoxCollider2D boxCollider2D;
    [SerializeField] string fromScene;
    [SerializeField] string toScene;
    [SerializeField] SoundName openDoorSound;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        
    }
    private void Start()
    {
        GameManager.Instance.IsExit(this);
    }

    public void OpenDoor()
    {
        animator.SetTrigger("GamePass");
       
        boxCollider2D.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventSystem.CallSceneTrasionEvent(fromScene,toScene);
    }
    public void PlayDoorOpenSoundFX()
    {
        if (openDoorSound != SoundName.None)
            EventSystem.CallPlaySoundEvent(openDoorSound);
    }
}
