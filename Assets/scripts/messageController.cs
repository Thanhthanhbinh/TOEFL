using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class messageController : MonoBehaviour
{
    public GameObject current;
    public GameObject title;
    public void closeMessage(){
        Destroy(current);
    }
    public void updateUI(string input) {
        TMP_Text titleTxt = title.GetComponent<TMP_Text>();
        titleTxt.SetText(input);
    }
}
