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
    public List<QuestionAnswer> questionList;

    public FixedString4096Bytes resultData;
    
    public List<FixedString4096Bytes> resultList;

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
