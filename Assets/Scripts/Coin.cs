using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate((Random.Range(-11f, 11f)), (Random.Range(-7f, 7f)), 0);

        Destroy(this.gameObject, 5f);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //player hit
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(5);
            Destroy(this.gameObject);

        }
    }

}
