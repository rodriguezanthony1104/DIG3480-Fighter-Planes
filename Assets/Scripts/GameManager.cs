using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemyOne;
    public GameObject cloud;
    public GameObject coin;
    
    //text mesh pro ui
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;


    [SerializeField] private int score;
    public int lives = 3;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemyOne", 1f, 3f);
        InvokeRepeating("CreateCoin", 3f, 8f);
        CreateSky();
        score = 0;
        scoreText.text = "Score: " + score;
        lives = 3;
        livesText.text = "Lives: " + lives;

        //InvokeRepeating("CreateEnemy2", 2f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void CreateEnemyOne()
    {
        Instantiate(enemyOne, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.Euler(0, 0, 180));
    }

    void CreateCoin()
    {
        Instantiate(coin, transform.position, Quaternion.identity);
        transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-6f, 6f), 0);
    }

    /*void CreateEnemy2()
    {
        Instantiate(enemy2, new Vector3(11f, Random.Range(1f, 6.5f), 0), Quaternion.identity);
    } 
    */
}
