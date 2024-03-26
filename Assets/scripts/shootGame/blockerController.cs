using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockerController : MonoBehaviour
{
    // Start is called before the first frame update
    public string state;
    public GameObject blocker;
    public GameObject player;
    private float speed;
    // Start is called before the first frame update
    void Update() {
        
        if (state=="incorrect"){//move to block
            if (blocker.transform.localPosition.x <  player.transform.localPosition.x) {
                speed = 500f;
            }else {
                speed = 0f;
            }
        }
        if (state=="correct"){//move to not block
            if (blocker.transform.localPosition.x < -44f) {
                speed = 300f;
            }else {
                speed = 0f;
            }
        }
        if (state=="done"){
            if (blocker.transform.localPosition.x > -99f) {
                speed = -500f;
            }else {
                speed = 0f;
            }
        }
        blocker.transform.localPosition = new Vector2(blocker.transform.localPosition.x + (speed*Time.deltaTime),blocker.transform.localPosition.y);  
        // Debug.Log(state);
        //275 for x
        
    }
    void OnCollisionEnter2D(Collision2D collision){
        state = "done";
    }
}
