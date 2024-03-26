using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public static ExamInfo Instance;

    public bool hintBadge;
    
    public Dictionary<string,int> badgeList;
    public int score;
    public int grade;
    public int total;

    // show performance 
    public string state;

    public string reward;
    public string gameType;

    public Dictionary<int, string> section;
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        badgeList = new Dictionary<string, int>{{"luckyBadge",0},{"aboveTotalBadge",0},{"allCorrectBadge",0},{"noHintBadge",0},{"noLivesBadge",0}};
        section = new Dictionary<int, string> ();
        DontDestroyOnLoad(gameObject);
    }

    
}
