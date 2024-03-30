using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class shootGameController : MonoBehaviour,gameController
{

    public GameObject player;
    public Transform bulletGroup;

    public GameObject blocker;
    
    // void Start(){
    //     setup(0);
    // }

    
    public void setup(int total) {
        Debug.Log("setup");
        blocker.GetComponent<blockerController>().state = "done";
        blocker.transform.localPosition = new Vector2(100f,blocker.transform.localPosition.y);  
    }
    private void shootBullet() {
        foreach(Transform child in bulletGroup.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject bullet = Resources.Load<GameObject>("ShootGame/bullet");
        GameObject temp = Instantiate(bullet,bulletGroup);
        temp.transform.position = player.transform.position;

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
        return blocker.GetComponent<blockerController>().done && bulletGroup.childCount == 0;
    }
    
}
