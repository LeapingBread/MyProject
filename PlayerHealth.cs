using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>, IDamagable
{
    [SerializeField] int health;
    
    [SerializeField] SoundName revieSound;
    [SerializeField] SoundName healthPlus;
    [SerializeField] int currentHealth;
    
    bool isDead;
    
    public bool IsDead { get { return isDead; } }
    PlayerAnimation playerAnimation;
    protected override void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }
    private void OnEnable()
    {
        EventSystem.ResetPlayerHealthEvent += OnResetPlayerHealthEvent;
        EventSystem.StartNewGameEvent += OnStartNewGameEvent;
        EventSystem.AddHealthEvent += OnAddHealthEvent;
    }
    private void OnDisable()
    {
        EventSystem.ResetPlayerHealthEvent -= OnResetPlayerHealthEvent;
        EventSystem.StartNewGameEvent -= OnStartNewGameEvent;
        EventSystem.AddHealthEvent -= OnAddHealthEvent;
    }
    void OnAddHealthEvent(int amount)
    {
        currentHealth += amount;
        if (currentHealth >= health)
            currentHealth = health;
        EventSystem.CallUpdateHealthUIEvent(currentHealth);
    }
    void OnStartNewGameEvent(string StartScene)
    {
        currentHealth = health;
        EventSystem.CallUpdateHealthUIEvent(currentHealth);
    }
    void OnResetPlayerHealthEvent()
    {
        currentHealth = health;
        EventSystem.CallUpdateHealthUIEvent(currentHealth);
    }
    private void Start()
    {
        
        currentHealth = health;
        GameManager.Instance.IsPlayerHealth(this);
    }

    private void Update()
    {
        
        isDead = currentHealth <= 0;
    }
    public void GetHit(int damage)
    {
        if (!playerAnimation.animator.GetCurrentAnimatorStateInfo(1).IsName("Player_GetHit"))
        {
            currentHealth -= damage;
            
        }
        if(currentHealth<1)
        {
          currentHealth = 0;
            EventSystem.CallUpdateHealthUIEvent(currentHealth);
            return;
          
        }
        playerAnimation.animator.SetTrigger("GetHit");
        EventSystem.CallUpdateHealthUIEvent(currentHealth);
    }

}
