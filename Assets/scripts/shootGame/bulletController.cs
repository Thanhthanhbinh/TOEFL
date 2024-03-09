using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    public GameObject bullet;

    // Update is called once per frame
    void Update()
    {
        bullet.transform.position = new Vector2(bullet.transform.position.x,bullet.transform.position.y + (500f*Time.deltaTime));  
    }

    void OnCollisionEnter2D (Collision2D collision){
        Destroy(bullet);
    }
}
