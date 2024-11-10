using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public int whatAmI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //calling the bullets
        if (whatAmI == 1)
        {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 8f);
        }

        //calling the enemy
        else if (whatAmI == 2)
        {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 6f);
        }

        //calling the clouds
        else if (whatAmI == 3)
        {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * Random.Range(3f, 10f));
        } 

        if ((transform.position.y > 9f || transform.position.y <= -9f) && whatAmI != 3)
        {
            Destroy(this.gameObject);
        }

        if (transform.position.y <= -9f && whatAmI == 3)
        {
            transform.position = new Vector3(Random.Range(-12f, 12f), 9f, 0); 
        }

    }
}
