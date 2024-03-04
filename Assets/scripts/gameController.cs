using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void correctRun() {
        Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
        playerRigid.AddForce(new Vector2(200f, 500f), ForceMode2D.Impulse);
    }
}
