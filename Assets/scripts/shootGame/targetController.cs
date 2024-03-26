using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject blocker;
    void OnCollisionEnter2D(Collision2D collision){
        blocker.GetComponent<blockerController>().state = "done";
        Debug.Log("correct");
    }
}
