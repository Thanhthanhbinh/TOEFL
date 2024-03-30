using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool touching = true;
    [SerializeField] private GameObject player;

    void Update() {
        if (player.GetComponent<Rigidbody2D>().IsSleeping()){
            touching = true;
        }else{
            touching = false;
        }
    }
        
    

}
