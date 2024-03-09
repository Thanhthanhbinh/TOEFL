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
    // Start is called before the first frame update

    void Update() {
        try{
            if (platformGroup.GetChild(0).transform.position.x < 163f){
                
                foreach (Transform platform in platformGroup.transform)
                {
                    platform.position = 
                    new Vector2(platform.position.x + (900f*Time.deltaTime),platform.position.y);            }
            }
            
        }
        catch (Exception e) {
            Debug.Log(e);
        }
    }
    public void setup(int total) {
        Debug.Log("setup");
        player.GetComponent<jumpMode>().jump = true;
        GameObject platform = Resources.Load<GameObject>("wall");
        foreach(Transform child in platformGroup.transform)
        {
            Destroy(child.gameObject);
        }
        float xPos = 163f;
        for (int i = 0; i < total; i++)
        {
            
            GameObject temp = Instantiate(platform,platformGroup);
            temp.transform.position = new Vector2(xPos,50);
            temp.GetComponent<wallController>().currentPlatform = temp;
            temp.GetComponent<wallController>().block = block;
            temp.GetComponent<wallController>().player = player;
            xPos = xPos - 100f;
            
        }
            }

    private void wallMove(){
        Rigidbody2D wall = platformGroup.GetChild(0).GetComponent<Rigidbody2D>();
        wall.AddForce(new Vector2(400f, 0f), ForceMode2D.Impulse);

    }
    public void correctRun() {
        if (!player.GetComponent<jumpMode>().jump) {
            return;
        }
        player.GetComponent<jumpMode>().jump = false;
        Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
        playerRigid.AddForce(new Vector2(0f, 400f), ForceMode2D.Impulse);
        wallMove();
    }

    public void incorrectRun() {
        if (!player.GetComponent<jumpMode>().jump) {
            return;
        }
        player.GetComponent<jumpMode>().jump = false;
        Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
        playerRigid.AddForce(new Vector2(0f, 250f), ForceMode2D.Impulse);
        wallMove();
    }
    
}
