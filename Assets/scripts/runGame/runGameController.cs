using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class runGameController : MonoBehaviour,gameController
{

    [SerializeField] private GameObject player;
    [SerializeField] private Transform enemyGroup;
    [SerializeField] private GameObject block;

    [SerializeField] private string projectile;
    // Start is called before the first frame update

    
    public void setup(int total) {
        //set jump mode to prevent excessive jumping
        player.GetComponent<jumpMode>().jump = true;
        // remove any remaining projectile
        foreach(Transform child in enemyGroup.transform)
        {
            Destroy(child.gameObject);
        }
        
    }

    private void shootEnem(float speed){
        // remove any remaining projectile
        foreach(Transform child in enemyGroup.transform)
        {
            Destroy(child.gameObject);
        }
        // load enemy
        GameObject wall = Resources.Load<GameObject>(projectile);
        GameObject temp = Instantiate(wall,enemyGroup);
        // correct position
        temp.transform.localPosition = new Vector2(0,0);
        // set speed and self reference
        temp.GetComponent<wallController>().speed=speed;
        temp.GetComponent<wallController>().block=temp;
    }
    public void correctRun() {
        // enemy will go slow and player will jump high
        // player and enemy only move when on ground
        if (player.GetComponent<jumpMode>().jump) {
            shootEnem(400f);
            player.GetComponent<jumpMode>().jump = false;
            Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
            playerRigid.AddForce(new Vector2(0f, 600f), ForceMode2D.Impulse);
        }
    }

    public void incorrectRun() {
        // enemy will go fast and player will jump low
        // enemy will go but player only jump if on ground
        shootEnem(900f);
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
