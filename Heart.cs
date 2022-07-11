using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    Collider2D collider2D;
    Animator animator;
    [SerializeField] int addHealthAmount;
    [SerializeField] SoundName addHealthSound;
    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("Active");
        collider2D.enabled = true;
        EventSystem.CallAddHealthEvent(addHealthAmount);
        if(addHealthSound != SoundName.None)
        EventSystem.CallPlaySoundEvent(addHealthSound);
    }
    public void DestoryThis()//animation event
    {
        Destroy(gameObject);
    }
}
