using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ExamInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public static ExamInfo Instance;

    public bool hintBadge;
    
    public Dictionary<string,int> badgeList;
    public Dictionary<string,string> gameTypeList;
    public List<string> rewardList;
    public int score;
    public int grade;
    public int total;

    // show performance 
    public string state;

    public string reward;
    public string gameType;

    public Dictionary<int, string> section;

    public List<QuestionAnswer> questionList = new List<QuestionAnswer>();

    public List<string> pictureList = new List<string>();

    public bool examMode;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        badgeList = new Dictionary<string, int>{{"luckyBadge",0},{"aboveTotalBadge",0},{"allCorrectBadge",0},{"noHintBadge",0},{"noLivesBadge",0}};
        gameTypeList = new Dictionary<string,string>{{"Jelly Serve","JumpGame/JumpGame"},{"Fallen Bird","FlyGame/FlyGame"},{"Soccer Kick","ShootGame/ShootGame"},{"Honey Steal","AvoidGame/AvoidGame"}};
        rewardList = new List<string>{"lives","scenario","badge"};
        section = new Dictionary<int, string> ();
        examMode = true;
        DontDestroyOnLoad(gameObject);
    }

    

    
}
