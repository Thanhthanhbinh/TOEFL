using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class badgeController : MonoBehaviour
{
    // Start is called before the first frame update
    public string badge;
    public GameObject title;

    private bool show;
    public void toggleName()
    {   
        if (!show){
            show = true;
            title.GetComponentInChildren<TMP_Text>().SetText(badge);
            Debug.Log(badge);
        }else{
            show = false;
            title.GetComponentInChildren<TMP_Text>().SetText("");
            Debug.Log("");
        }
        //If your mouse hovers over the GameObject with the script attached, output this message
        
    }

    
}
