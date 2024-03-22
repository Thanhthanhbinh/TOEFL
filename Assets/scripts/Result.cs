using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    // Start is called before the first frame update
    public static Result Instance;

    public bool liveBadge;
    public bool hintBadge;
    public bool overBadge;

    public bool correctBadge;
    public int score;
    public int grade;
    public int total;
    public string state;

    public string reward;
    public string gameType;

    public int section;
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    
}
