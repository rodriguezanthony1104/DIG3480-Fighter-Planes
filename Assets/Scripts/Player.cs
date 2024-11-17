using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    // its access level: public or private
    // its type: int (5, 8, 36, etc.), float (2.5f, 3.7f, etc.)
    // its name: speed, playerSpeed --- Speed, PlayerSpeed
    // optional: give it an initial value
    private float speed;
    public int lives = 3;
    private float horizontalInput;
    private float verticalInput;
    private float horizontalScreenSize = 11.5f;
    private float verticalScreenSize = 7.5f;
    private int shooting;
    public bool hasShield;

    public GameManager gameManager;

    public GameObject bullet;
    public GameObject explosion;
    public GameObject thruster;
    public GameObject shield;


    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        shooting = 1;
        hasShield = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        // if (condition) { //do this }
        // else if (other condition { //do that }
        // else { //do this final }
        if (transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y >= verticalScreenSize || transform.position.y <= -verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    void Shooting()
    {
        //if I press SPACE
        //Create a bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch(shooting)
            {
                case 1:
                    //Create a bullet
                    Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    break;
                case 2:
                    //Create two bullets
                    Instantiate(bullet, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.identity);  
                    break;
                case 3:
                    //Create three bullets
                    Instantiate(bullet, transform.position + new Vector3(0.5f, 1, 0), Quaternion.Euler(0, 0, -30f));
                    Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.Euler(0, 0, 30));
                    break;
            }    
        }
    }

    public void Lives(int newLives)
    {
        //lives -= 1;
        //lives = lives -1;
        if (hasShield == false)
        {
            lives--;
        }
        else if (hasShield == true)
        {
            Destroy(shield.gameObject);
            gameManager.PlayPowerDown();
            hasShield = false;
        }

        if (lives == 0)
        {
            gameManager.GameOver();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        thruster.gameObject.SetActive(false);
        speed = 6f;
        gameManager.PlayPowerDown();
        gameManager.UpdatePowerUpText("");

    }

    IEnumerator ShootingDown()
    {
        yield return new WaitForSeconds(3f);
        shooting = 1;
        gameManager.PlayPowerDown();        
        gameManager.UpdatePowerUpText("");
    }

    IEnumerator ShieldDown()
    {
        yield return new WaitForSeconds(3f);
        gameManager.UpdatePowerUpText("");
        hasShield = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PowerUp")
        {
            gameManager.PlayPowerUp();
            //four types of powerups
            int poweruptype = Random.Range(1, 5); 
            switch(poweruptype)
            {
                //speed
                case 1:
                    speed = 12f;
                    gameManager.UpdatePowerUpText("Picked up Speed!");
                    thruster.gameObject.SetActive(true);
                    StartCoroutine(SpeedPowerDown());
                    break;
                //double shot
                case 2:
                    shooting = 2;
                    gameManager.UpdatePowerUpText("Picked up Double Shot!");
                    StartCoroutine(ShootingDown());
                    break;
                //triple shot
                case 3:
                    shooting = 3;
                    gameManager.UpdatePowerUpText("Picked up Triple Shot!");
                    StartCoroutine(ShootingDown());
                    break;
                //shield
                case 4:
                    hasShield = true;
                    gameManager.UpdatePowerUpText("Picked up Shield!");
                    shield.gameObject.SetActive(true);
                    StartCoroutine(ShieldDown());
                    break;
            }
            Destroy(other.gameObject);
        }
    }

}
