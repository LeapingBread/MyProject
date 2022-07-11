using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    PlayerHealth playerHealth;
    Door exit;
    bool gameOver;
    
    public bool GameOver { get { return gameOver; } }

    public List<Enemy> enemies = new List<Enemy>();
   
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
        enemies.Clear();
    }
    private void Start()
    {
        exit = FindObjectOfType<Door>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }
    public void IsExit(Door door)
    {
        exit = door;
    }
    public void IsPlayerHealth(PlayerHealth health)
    {
        playerHealth = health;
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Menu"&&playerHealth != null)
        {
            gameOver = playerHealth.IsDead;
            UIManager.Instance.ShowGameOverPanle(gameOver);
        }
            GamePass();
        
        
    }
    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
    private void GamePass()
    {
        
        if (enemies.Count ==0&& exit != null)
        {
            exit.OpenDoor();
        }
    }

    
}
