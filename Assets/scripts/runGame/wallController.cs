using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallController : MonoBehaviour
{
    public GameObject currentPlatform;
    public GameObject block;
    public GameObject player;

    // Start is called before the first frame update

    
    void Update()
    {
        block.transform.position = new Vector2(block.transform.position.x+ (400f*Time.deltaTime),block.transform.position.y );  
    }

    void OnCollisionEnter2D (Collision2D collision){
        Destroy(block);
    }
}
