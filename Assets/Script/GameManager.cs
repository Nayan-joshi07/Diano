using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public float initialGameSpeed = 5f;
    public float gameSpeedIncreases = 0.1f;
    public float gameSpeed { get; private set; }
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI highScoreTxt;
    public Button retryButton;
    private Player player;
    private Spawner spawner;
    private float score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
        spawner = FindAnyObjectByType<Spawner>();
        NewGame();
    }

    public void NewGame() {

        Obstacles[] obstacles = FindObjectsOfType<Obstacles>();
        foreach(var obstacle in obstacles){
            Destroy(obstacle.gameObject);
        }
        score = 0;
        gameSpeed = initialGameSpeed;
        enabled = true;
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
    }

    public void GameOver() {
        updateHighScore();
        gameSpeed = 0f;
        enabled = false;
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncreases * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreTxt.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void updateHighScore() {
        float highScore = PlayerPrefs.GetFloat("hiscore",0);
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetFloat("hiscore", highScore);
        }
        highScoreTxt.text = Mathf.FloorToInt(highScore).ToString("D5");
    }
}
