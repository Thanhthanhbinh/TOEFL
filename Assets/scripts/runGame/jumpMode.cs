using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpMode : MonoBehaviour
{
    // Start is called before the first frame update
    public bool jump;

    
    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision){
        string value = collision.gameObject.name;
        if( value== "runPlatform"){
            jump = true;
        }
    }
}
