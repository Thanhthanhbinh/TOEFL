using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public static UserData Instance;
    public string playerType;
    public string joinCode;


    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null )
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        playerType = "";
        DontDestroyOnLoad(gameObject);
    }

    
}
