using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI bossHealthText;
    public TextMeshProUGUI survivalTimeText;

    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject YouWonScreen;

    public Button restartButton;

    public bool isGameActive = false;
    public bool isBossActive = false;
    public int bossHealth;
    public float bossSpeed;
    public float bossFireRate;
    public int health;
    public int ammoLeft;
    private int kills=-1;
    public int level;
    public float survivalTime;

    public float enemySpeed = 1.0f;
    public float fireRate = 5.0f;
    public float ammoSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            updateTime();
        }
    }

    public void updateTime()
    {
        survivalTime+=Time.deltaTime;
        survivalTimeText.text = "Survival Time: " + survivalTime;
    }
    public void updateScore(int healthToAdd)
    {
        health += healthToAdd;
        healthText.text = "Health : "+health;
    }

    public void updateBossHealth()
    {
        bossHealth--;
        bossHealthText.text = "Boss Health: " + bossHealth;
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        fireRate /= difficulty;
        enemySpeed *= difficulty;
        ammoSpeed *= difficulty;
        bossFireRate /= difficulty;
        titleScreen.gameObject.SetActive(false);
        updateScore(0);
        updateAmmo(0);
        updateKills();
    }

    public void YouWon()
    {
        isGameActive = false;
        YouWonScreen.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void updateLevel()
    {
        level++;
        levelText.text = "Level : " + level;
    }

    public void updateAmmo(int ammoToAdd)
    {
        ammoLeft += ammoToAdd;
        ammoText.text = "Ammo : " + ammoLeft;
    }

    public void updateKills()
    {
        kills++;
        killsText.text = "Kills : " + kills;
    }

    public void ActivateBoss()
    {
        isBossActive = true;
        fireRate /= 2;
        levelText.text = "Level : BOSS";
        updateBossHealth();
        bossHealthText.gameObject.SetActive(true);
    }
}
