using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPlatformController : MonoBehaviour
{
    public GameObject currentPlatform;
    public GameObject player;

    public Transform platformGroup;
    // Start is called before the first frame update
    void Update() {
        if (player.GetComponent<Collider2D>().IsTouching(currentPlatform.GetComponent<Collider2D>())){
            // Debug.Log(currentPlatform.transform.position.y);
            if (currentPlatform.transform.position.y > 46f){
                player.transform.position = new Vector2(player.transform.transform.position.x,player.transform.transform.position.y -(500f * Time.deltaTime));
                foreach (Transform platform in platformGroup.transform)
                {
                    platform.position = new Vector2(platform.transform.position.x,platform.transform.position.y -(500f * Time.deltaTime));
                }
            }
        }
    }
}
