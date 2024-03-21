using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallController : MonoBehaviour
{
    public GameObject currentPlatform;
    public GameObject block;
    public GameObject player;

    // Start is called before the first frame update

    
    void Update() {
        if (block.GetComponent<Collider2D>().IsTouching(currentPlatform.GetComponent<Collider2D>())){
            // Debug.Log("block");
            Destroy(currentPlatform);   
        }
        if (player.GetComponent<Collider2D>().IsTouching(currentPlatform.GetComponent<Collider2D>())){
            // Debug.Log("player");
            Destroy(currentPlatform);   
        }
        //275 for x
        
    }
}
