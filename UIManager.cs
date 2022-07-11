using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject reportBugPanel;
    [SerializeField] Transform healthBar;
    [SerializeField] Slider bossHealthBar;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameFinishedPanel;
    [SerializeField] SoundName buttonClickSound;


    private void OnEnable()
    {
        EventSystem.UpdateHealthUIEvent += OnUpdateHealthUIEvent;
        EventSystem.ShowGameFinishedEvent += OnShowGameFinishedEven;
    }
    private void OnDisable()
    {
        EventSystem.UpdateHealthUIEvent -= OnUpdateHealthUIEvent;
        EventSystem.ShowGameFinishedEvent -= OnShowGameFinishedEven;
    }
    void OnShowGameFinishedEven (bool gameFinished)
    {
        if (gameFinished)
            gameFinishedPanel.SetActive(true);

    }
    void OnUpdateHealthUIEvent(int currenthealth)
    {
        UpdatePlayerHealthBar(currenthealth);
    }
    private void Start()
    {
        gameOverPanel.SetActive(true);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ReportBUG()
    {
        Time.timeScale = 0;
        reportBugPanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        reportBugPanel.SetActive(false);
    }
    public void RestartGame()
    {
        Scene currentscene = SceneManager.GetActiveScene();
        EventSystem.CallSceneTrasionEvent(currentscene.name, currentscene.name);
        GameManager.Instance.enemies.Clear();
        EventSystem.CallResetPlayerHealthEvent();
        bossHealthBar.transform.parent.gameObject.SetActive(false);
    }
    
    
    public void MainMenu()
    {
        string currentscene = SceneManager.GetActiveScene().name;
        EventSystem.CallSceneTrasionEvent(currentscene, "Menu");
    }
    public void UpdatePlayerHealthBar(int health)
    {
        switch (health)
        {
                case 6:
                healthBar.GetChild(0).gameObject.SetActive(true);
                healthBar.GetChild(1).gameObject.SetActive(true);
                healthBar.GetChild(2).gameObject.SetActive(true);
                break;
                case 4:
                healthBar.GetChild(0).gameObject.SetActive(true);
                healthBar.GetChild(1).gameObject.SetActive(true);
                healthBar.GetChild(2).gameObject.SetActive(false);
                break;
            case 2:
                healthBar.GetChild(0).gameObject.SetActive(true);
                healthBar.GetChild(1).gameObject.SetActive(false);
                healthBar.GetChild(2).gameObject.SetActive(false);
                break;
            case 0:
                healthBar.GetChild(0).gameObject.SetActive(false);
                healthBar.GetChild(1).gameObject.SetActive(false);
                healthBar.GetChild(2).gameObject.SetActive(false);
                break;

        }
    }
    public void TrunOffBossHealthBar(int enemyHealth)
    {
        if(enemyHealth<= 0)
        bossHealthBar.transform.parent.gameObject.SetActive(false);
    }
    public void ShowBossHealthBar(int enemyHealth)
    {
        bossHealthBar.transform.parent.gameObject.SetActive(true);
        bossHealthBar.maxValue = enemyHealth;
    }
    public void BossHealthBarOff()
    {
        bossHealthBar.transform.parent.gameObject.SetActive(false);
    }
    public void UpdateBossHealthBar(int enemyHealth)
    {
        bossHealthBar.value = enemyHealth;
        if(enemyHealth <=0)
            bossHealthBar.transform.parent.gameObject.SetActive(false);
    }
    public void ShowGameOverPanle(bool gameover)
    {
        gameOverPanel.SetActive(gameover);
        
    }
    public void buttonSoundEvent()
    {
        EventSystem.CallPlaySoundEvent(buttonClickSound);
    }
}
