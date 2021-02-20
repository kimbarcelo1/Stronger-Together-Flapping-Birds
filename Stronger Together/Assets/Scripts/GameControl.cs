using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControl : MonoBehaviour
{
    public static GameControl instance; // associatied with the class and always available to other scripts
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextSummary;
    public bool gameOver = false;
    public float scrollSpeed = -1.5f;
    public int destroyBirdsAfter;
    public AudioClip flapSound;
    public AudioClip hitSound;
    public AudioClip scoreSound;
    public AudioClip startSound;
    //public AudioClip gameOverSound;
    public GameObject bird;
    public GameObject birdPickup;
    public GameObject pauseMenu;
    public GameObject instructionText;

    public int playerCount;
    public bool isPaused = false;

    [HideInInspector] public AudioSource audioSource;
    private int score = 0;

    private void Awake()
    {
        // enforce singleton pattern
        if(instance == null)
        {
            instance = this;
        } else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
        playerCount = playerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        audioSource.PlayOneShot(startSound, 0.5f);

        InvokeRepeating("SpawnPickups", 4f, Random.Range(5f, 10f));

        if(PlayerPrefs.GetInt("firstPlay") == 1)
        {
            PlayerPrefs.SetInt("firstPlay", 0);
            instructionText.SetActive(true);
            Destroy(instructionText, 5);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void BirdScored()
    {
        if (gameOver)
        {
            return; // return do not score
        }
        else
        {
            audioSource.PlayOneShot(scoreSound, 0.5f);
            score++;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void BirdDied()
    {
        scoreTextSummary.text = "Your Score is: " + score.ToString();
        gameOverPanel.SetActive(true);
        scoreText.gameObject.SetActive(false);
        gameOver = true;
        //audioSource.PlayOneShot(gameOverSound, 1f);
    }

    void SpawnPickups()
    {
        if (!gameOver)
        {
            Instantiate(birdPickup, (Vector2)birdPickup.transform.position + new Vector2(Random.Range(7f, 10f), Random.Range(-3.5f, 1.5f)), birdPickup.transform.rotation);
        }
        
    }

    public void RestartGame()
    {
        if (gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pauseMenu.SetActive(isPaused);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            pauseMenu.SetActive(isPaused);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
