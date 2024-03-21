using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool touching;
    void Start()
    {
        touching = true;
    }

    // Update is called once per frame
    void OnCollisionEnter2D (Collision2D collision){
        touching = true;
    }

    void OnCollisionExit2D (Collision2D collision){
        touching = false;
    }
}
