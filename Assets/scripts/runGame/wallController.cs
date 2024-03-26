using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallController : MonoBehaviour
{
    public GameObject block;
    public float speed;
    // Start is called before the first frame update
    private GameObject temp;
    
    void Update()
    {
        block.transform.position = new Vector2(block.transform.position.x+ (speed*Time.deltaTime),block.transform.position.y );  
    }

    private void showEffect(string name) {
        GameObject wall = Resources.Load<GameObject>("effect");
        temp = Instantiate(wall,block.transform.parent.parent);
        temp.transform.localPosition = new Vector2(56,-35);
        if( name== "Player"){
            Debug.Log("All");
            speed = 0;
            Destroy(temp, 1);
            Destroy(block, 1);
        }else{
            Destroy(temp);
            Destroy(block);
        }
    }
    void OnCollisionEnter2D (Collision2D collision){
        string value = collision.gameObject.name;
        showEffect(value);
        
    }
}
