using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpGameController : MonoBehaviour,gameController
{

    [SerializeField] private GameObject player;
    [SerializeField] private Transform platformGroup;
    [SerializeField] private string gameType;
    private List<GameObject> platformList;
    // public GameObject currentPlatform;
    private GameObject currentPlatform;
    private float yPos;

    public void setup(int total) {
        GameObject platform = Resources.Load<GameObject>(gameType+"/jumpPlatform");
        foreach(Transform child in platformGroup.transform)
        {
            Destroy(child.gameObject);
        }
        yPos = 65f;
        for (int i = 0; i < total+1; i++)
        {
            
            GameObject temp = Instantiate(platform,platformGroup);
            temp.transform.localPosition = new Vector2(temp.transform.localPosition.x,yPos);
            temp.GetComponent<jumpPlatformController>().currentPlatform = temp;
            temp.GetComponent<jumpPlatformController>().player = player;
            temp.GetComponent<jumpPlatformController>().platformGroup = platformGroup;
            
            yPos = yPos + 100f;
            
        }
    }
    
    public void correctRun() {
        if (player.GetComponent<playerController>().touching == false){
            Debug.Log("on air");
            return;
        }
        Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
        playerRigid.velocity = new Vector2 (0, 0);
        playerRigid.AddForce(new Vector2(0f, 550f), ForceMode2D.Impulse);
        player.GetComponent<playerController>().touching = false;
    }

    public void incorrectRun() {
        if (player.GetComponent<playerController>().touching == false){
            Debug.Log("on air");
            return;
        }
        Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
        playerRigid.velocity = new Vector2 (0, 0);
        playerRigid.AddForce(new Vector2(0f, 300f), ForceMode2D.Impulse);
        player.GetComponent<playerController>().touching = false;
    }
    
    public bool finish(){
        // Debug.Log("is the game finished");
        return player.GetComponent<playerController>().touching;
    }
}
