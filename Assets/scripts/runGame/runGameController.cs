using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class runGameController : MonoBehaviour,gameController
{

    public GameObject player;
    public Transform platformGroup;
    private List<GameObject> wallList;
    // public GameObject currentPlatform;
    public GameObject block;

    public string projectile;
    // Start is called before the first frame update
    private float xPos;

    void Start(){
    }
    void Update() {

    }
    public void setup(int total) {
        Debug.Log("setup");
        player.GetComponent<jumpMode>().jump = true;
        GameObject platform = Resources.Load<GameObject>(projectile);
        foreach(Transform child in platformGroup.transform)
        {
            Destroy(child.gameObject);
        }
        
        }

    private void wallMove(float speed){

        foreach(Transform child in platformGroup.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject wall = Resources.Load<GameObject>(projectile);
        GameObject temp = Instantiate(wall,platformGroup);
        temp.transform.localPosition = new Vector2(-198,44);
        temp.GetComponent<wallController>().speed=speed;
        temp.GetComponent<wallController>().block=temp;
    }
    public void correctRun() {
        if (player.GetComponent<jumpMode>().jump) {
            wallMove(400f);
            player.GetComponent<jumpMode>().jump = false;
            Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
            playerRigid.AddForce(new Vector2(0f, 400f), ForceMode2D.Impulse);
        }
    }

    public void incorrectRun() {
        wallMove(900f);
        if (player.GetComponent<jumpMode>().jump) {
            player.GetComponent<jumpMode>().jump = false;
            Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
            playerRigid.AddForce(new Vector2(0f, 200f), ForceMode2D.Impulse);
        }
    }
    public bool finish(){
        return player.GetComponent<jumpMode>().jump;
    }
    
}
