using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public GameObject bullet;


    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
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
            //Create a bullet
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
    public void Lives(int newLives)
    {
        //lives -= 1;
        //lives = lives -1;
        lives--;

        if (lives == 0)
        {
            Destroy(this.gameObject);
        }

    }



}
