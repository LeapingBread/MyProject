using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Tooltip("DO NOT SET VALUE IN UNITY")]
    public Animator animator;
    PlayerMovement playerMovement;
    PlayerHealth playerHealth;
    Rigidbody2D rigidbody2D;
    [SerializeField] SoundName diedSound;
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        
    }
    private void Update()
    {
        animator.SetBool("IsRun", playerMovement.IsRun);
        animator.SetBool("IsJump", playerMovement.IsJump);
        animator.SetBool("OnGround", playerMovement.IsOnGround);
        animator.SetFloat("Y", rigidbody2D.velocity.y);
        animator.SetBool("IsDead", playerHealth.IsDead);
        if (playerHealth.IsDead)
        {
            
            return;
        }     
    }
    public void PlayerDiedFX()//animation event
    {
        if (diedSound != SoundName.None)
        {
            EventSystem.CallPlaySoundEvent(diedSound);
        }
    }
}
