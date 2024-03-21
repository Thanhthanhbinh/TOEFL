using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpGameController : MonoBehaviour,gameController
{

    public GameObject player;
    public Transform platformGroup;
    private List<GameObject> platformList;
    // public GameObject currentPlatform;

    private int platformCounter;
    // Start is called before the first frame update
    private float yPos;

    public void setup(int total) {
        Debug.Log("setup");
        GameObject platform = Resources.Load<GameObject>("JumpGame/jumpPlatform");
        foreach(Transform child in platformGroup.transform)
        {
            Destroy(child.gameObject);
        }
        yPos = 46f;
        for (int i = 0; i < total; i++)
        {
            
            GameObject temp = Instantiate(platform,platformGroup);
            temp.transform.position = new Vector2(temp.transform.position.x,yPos);
            temp.GetComponent<jumpPlatformController>().currentPlatform = temp;
            temp.GetComponent<jumpPlatformController>().player = player;
            temp.GetComponent<jumpPlatformController>().platformGroup = platformGroup;
            yPos = yPos + 100f;
            // Debug.Log(temp);
            // platformList.Add(temp);
        }

        

    }
    public void correctRun() {
        
        Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
        playerRigid.AddForce(new Vector2(0f, 450f), ForceMode2D.Impulse);
    }

    public void incorrectRun() {
        
        Rigidbody2D playerRigid = player.GetComponent<Rigidbody2D>();
        playerRigid.AddForce(new Vector2(0f, 300f), ForceMode2D.Impulse);
    }
    
    public bool finish(){
        Debug.Log("is the game finished");
        Debug.Log(player.GetComponent<playerController>().touching);
        return player.GetComponent<playerController>().touching;
    }
}
