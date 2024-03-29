using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class ExamData : MonoBehaviour
{
    // Start is called before the first frame update
    public static ExamData Instance;
    public FixedString4096Bytes examQuestion;

    public FixedString4096Bytes resultData;
    public int maxPlayers;
    public string joinCode;
    

    void Awake()
    {   
        if (Instance != null )
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
