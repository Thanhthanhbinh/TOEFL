using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPlatformController : MonoBehaviour
{
    public GameObject currentPlatform;
    public GameObject player;
    public Transform platformGroup;
    void Update() {
        if (player.GetComponent<Collider2D>().IsTouching(currentPlatform.GetComponent<Collider2D>())){
            if (currentPlatform.transform.localPosition.y > 65f){
                player.transform.localPosition = new Vector2(player.transform.transform.localPosition.x,player.transform.transform.localPosition.y -(500f * Time.deltaTime));
                foreach (Transform platform in platformGroup.transform)
                {
                    platform.localPosition = new Vector2(platform.transform.localPosition.x,platform.transform.localPosition.y -(500f * Time.deltaTime));
                }
            }
        }
    }
}
