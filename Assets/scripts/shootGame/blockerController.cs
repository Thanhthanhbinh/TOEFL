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

    public bool done;
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
                done = true;
            }
        }else{
            done = false;
        }
        
        blocker.transform.localPosition = new Vector2(blocker.transform.localPosition.x + (speed*Time.deltaTime),blocker.transform.localPosition.y);  
        
        
    }
    private void showEffect() {
        GameObject wall = Resources.Load<GameObject>("effect");
        GameObject temp = Instantiate(wall,blocker.transform.parent);
        temp.transform.localPosition = new Vector2(56,-35);
        Destroy(temp, 1);
    }
    void OnCollisionEnter2D(Collision2D collision){
        state = "done";
        showEffect();
    }
}
