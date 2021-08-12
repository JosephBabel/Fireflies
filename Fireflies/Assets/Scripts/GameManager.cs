/* Author: Joseph Babel
*  Description: Manages game with score, lives, waves, and updating text UI.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private EnemyManager enemyManager;
    private FoodManager foodManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject player;

    private bool gameRunning;

    [SerializeField] private float currentScore = 0.0f;
    [SerializeField] private float scorePerFood = 1.0f;
    [SerializeField] private int currentWave = 1;
    [SerializeField] private int maxWaves = 20;
    [SerializeField] private float timeBetweenWaves = 5.0f;
    [SerializeField] private int currentLives = 3;

    // Text UI
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;
    [SerializeField] private Text waveText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text anyKeyText;
    [SerializeField] private Text creditsText;

    void Start()
    {
        gameRunning = true;
        highScoreText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        anyKeyText.gameObject.SetActive(false);
        creditsText.gameObject.SetActive(false);
        enemyManager = GetComponent<EnemyManager>();
        foodManager = GetComponent<FoodManager>();
        InvokeRepeating("IncrementWave", timeBetweenWaves, timeBetweenWaves); // Increment wave after period of time
    }

    private void Update()
    {
        if (!gameRunning && Input.anyKey)
        {
            SceneManager.LoadScene("Game");
        }
    }

    public Vector2 GetPlayerPos()
    {
        return (Vector2) player.transform.position;
    }

    void IncrementWave()
    {
        if (gameRunning)
        {
            if (currentWave < maxWaves)
            {
                currentWave++;
                waveText.text = "Wave: " + currentWave;
                enemyManager.ReduceSpawnTimer();
                foodManager.ReduceSpawnTimer();
            }
        }
    }

    public void AddScore()
    {
        if (gameRunning)
        {
            currentScore += scorePerFood * currentWave;
            scoreText.text = "Score: " + currentScore;
        }
    }

    public void decreaseLife()
    {
        if (gameRunning)
        {
            if (currentLives > 1)
            {
                currentLives--;
                livesText.text = "Lives: " + currentLives;
            }
            else
            {
                GameOver();
            }
        }
    }

    public bool IsGameRunning()
    {
        return gameRunning;
    }

    private void GameOver()
    {
        gameRunning = false;
        highScoreText.text = "High Score: " + currentScore;
        highScoreText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        anyKeyText.gameObject.SetActive(true);
        creditsText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
        waveText.gameObject.SetActive(false);
        audioManager.playGameOverAudio();
    }

    public void SetAudioManager(AudioManager audioManager)
    {
        this.audioManager = audioManager;
    }
}
