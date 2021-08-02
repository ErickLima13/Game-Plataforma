using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Transform player;

    public int score;
    public Text scoreText;

    public GameObject pauseObj;
    public GameObject gameOverObj;
    

    public int totalScore;

    public Text healthText;
    

    public static GameController instance;

    private bool isPaused;

    public GameObject drop;
    public bool controle;

    //awake é inicializado antes de todos os metodos star() do seu projeto
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Start()
    {
        totalScore = PlayerPrefs.GetInt("score");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x >= 4)
        {
            if(controle == false)
            {
                GameObject littleSecret = Instantiate(drop, transform.position, transform.rotation);
                controle = true;
            }
        }

        PauseGame();

    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();


        PlayerPrefs.SetInt("score", score + totalScore);
    }

    public void UpdateLives(int value)
    {
        healthText.text ="x " + value.ToString();
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            pauseObj.SetActive(isPaused);
        }

        //pausa o jogo
        if (isPaused)
        {
            Time.timeScale = 0f;
        }

        //conitnua o jogo
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void GameOver()
    {
        gameOverObj.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    
   
}
