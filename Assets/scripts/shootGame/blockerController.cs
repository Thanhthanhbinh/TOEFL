using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockerController : MonoBehaviour
{
    // Start is called before the first frame update
    public string state;
    public GameObject blocker;

    private float speed;
    // Start is called before the first frame update
    void Update() {
        if (state=="incorrect"){//move to block
            if (blocker.transform.position.x <  171f) {
                speed = 500f;
            }else {
                speed = 0f;
            }
        }
        if (state=="correct"){//move to not block
            if (blocker.transform.position.x < 100f) {
                speed = 500f;
            }else {
                speed = 0f;
            }
        }
        if (state=="done"){
            if (blocker.transform.position.x > 55f) {
                speed = -500f;
            }else {
                speed = 0f;
            }
        }
        blocker.transform.position = new Vector2(blocker.transform.position.x + (speed*Time.deltaTime),blocker.transform.position.y);  
        // Debug.Log(state);
        //275 for x
        
    }
    void OnCollisionEnter2D(Collision2D collision){
        state = "done";
    }
}
