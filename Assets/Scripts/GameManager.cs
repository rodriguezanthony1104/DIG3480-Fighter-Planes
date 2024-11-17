using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemyOne;
    public GameObject cloud;
    public GameObject coin;
    public GameObject powerUp;

    public AudioClip powerUpSound;
    public AudioClip powerDownSound;
    
    public int cloudSpeed;
    private bool isPlayerAlive;
    
    //text mesh pro ui
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI powerUpText;


    [SerializeField] private int score;
    public int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        isPlayerAlive = true;
        InvokeRepeating("CreateEnemyOne", 1f, 3f);
        InvokeRepeating("CreateCoin", 3f, 8f);
        StartCoroutine(CreatePowerUp());
        CreateSky();
        score = 0;
        scoreText.text = "Score: " + score;
        lives = 3;
        livesText.text = "Lives: " + lives;
        cloudSpeed = 1;

        //InvokeRepeating("CreateEnemy2", 2f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
    }

    void CreateSky()
    {
        for(int i = 0; i < 30; i++)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
        }
    }

    public void EarnScore(int newScore)
    {
        score = score + newScore;
        scoreText.text = "Score: " + score;
    }

    public void LoseLives(int newLives)
    {
        lives = lives + newLives;
        livesText.text = "Lives: " + lives;
    }

    public void UpdatePowerUpText(string poweruptype)
    {
        powerUpText.text = poweruptype;
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOne, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.Euler(0, 0, 180));
    }

    void CreateCoin()
    {
        Instantiate(coin, transform.position, Quaternion.identity);
        transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-6f, 6f), 0);
    }

    IEnumerator CreatePowerUp()
    {
        Instantiate(powerUp, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(3f, 6f));
        StartCoroutine(CreatePowerUp());
    }

    public void PlayPowerUp()
    {
        AudioSource.PlayClipAtPoint(powerUpSound, Camera.main.transform.position);
    }

    public void PlayPowerDown()
    {
        AudioSource.PlayClipAtPoint(powerDownSound, Camera.main.transform.position);
    }

    public void GameOver()
    {
        isPlayerAlive = false;
        CancelInvoke();
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        cloudSpeed = 0;
    }

    void Restart()
    {
        if(Input.GetKeyDown(KeyCode.R) && isPlayerAlive == false)
        {
            //restart the game
            SceneManager.LoadScene("Game");
        }
    }

    /*void CreateEnemy2()
    {
        Instantiate(enemy2, new Vector3(11f, Random.Range(1f, 6.5f), 0), Quaternion.identity);
    } 
    */
}
