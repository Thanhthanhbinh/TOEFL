using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.Collections;

public class MessageManager :MonoBehaviour
{
    public static void createMessage(string input,Transform container){
        GameObject messageItem = Resources.Load<GameObject>("message");
        GameObject temp = Instantiate(messageItem,container);
        temp.transform.localPosition = new Vector2(0,0);
        temp.GetComponentInChildren<messageController>().updateUI(input);
    }
}
