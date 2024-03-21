using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class shootGameController : MonoBehaviour,gameController
{

    public GameObject player;
    public Transform bulletGroup;

    public GameObject blocker;
    
    // public GameObject currentPlatform;

    // Start is called before the first frame update

    
    public void setup(int total) {
        Debug.Log("setup");
        blocker.GetComponent<blockerController>().state = "done";
        blocker.transform.position = new Vector2(55f,blocker.transform.position.y);  
    }
    private void shootBullet() {
        foreach(Transform child in bulletGroup.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject bullet = Resources.Load<GameObject>("ShootGame/bullet");
        GameObject temp = Instantiate(bullet,bulletGroup);
        // temp.transform.position = new Vector2(171f,500f);

        temp.GetComponent<bulletController>().bullet=temp;
        // Debug.Log(temp);
    }
    public void correctRun() {
        //block = correct
        shootBullet();
        blocker.GetComponent<blockerController>().state = "correct";
    }

    public void incorrectRun() {
        shootBullet();
        blocker.GetComponent<blockerController>().state = "incorrect";
        //block = incorrect

    }
    public bool finish(){
        return bulletGroup.childCount == 0;
    }
    
}
