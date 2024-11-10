using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player")
        {
            //player hit
            whatIHit.GetComponent<Player>().Lives(-1);
            GameObject.Find("GameManager").GetComponent<GameManager>().LoseLives(-1);
            Instantiate (explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }

        else if (whatIHit.tag == "Weapon")
        {
            //bullet hit
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(1);
            Instantiate (explosion, transform.position, Quaternion.identity);
            Destroy(whatIHit.gameObject);
            Destroy(this.gameObject);
        }
    }

}
