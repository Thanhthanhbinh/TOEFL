using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class resultController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scoreObject;
    public GameObject title;
    public Transform badgeContainer;
    public Transform scenarioContainer;
    private List<string> rewardList;
    private Dictionary<string,string> gameTypeList ;
    private Dictionary<string,string> showingGameType;
    void Start()
    {   
        gameTypeList = new Dictionary<string,string>{{"JumpGame","JumpGame/JumpGame"},{"RunGame","RunGame/RunGame"},{"ShootGame","ShootGame/ShootGame"}};
        showingGameType = new Dictionary<string,string>();
        rewardList = new List<string>{"lives","hint","scenario","badge"};
        generateScenario();
        scoreObject.GetComponentInChildren<TMP_Text>().SetText(Result.Instance.score + "/" + Result.Instance.total);
        title.GetComponentInChildren<TMP_Text>().SetText(Result.Instance.state);
        setUpBadge();
        setUpScenario();

    }

    public void giveReward(){
        System.Random rnd = new System.Random();
        int r = rnd.Next(rewardList.Count);
        Result.Instance.reward = rewardList[r];
        Debug.Log(Result.Instance.reward);
        if (Result.Instance.reward == "scenario") {
            generateScenario();
            setUpScenario();
        }
        if (Result.Instance.reward == "badge") {
            setUpBadge();
        }
    }
    
    private void setUpBadge(){
        foreach(Transform child in badgeContainer.transform)
        {
            Destroy(child.gameObject);
        }
        Debug.Log(Result.Instance.hintBadge);
        Debug.Log(Result.Instance.liveBadge);
        Debug.Log(Result.Instance.overBadge);
        if (Result.Instance.hintBadge) {
            GameObject badge = Resources.Load<GameObject>("Badges/noHintBadge");
            GameObject temp = Instantiate(badge,badgeContainer);
            Debug.Log(temp);
        }
        if (Result.Instance.liveBadge) {
            GameObject badge = Resources.Load<GameObject>("Badges/noLivesBadge");
            GameObject temp = Instantiate(badge,badgeContainer);
            Debug.Log(temp);
        }
        if (Result.Instance.overBadge) {
            GameObject badge = Resources.Load<GameObject>("Badges/aboveTotalBadge");
            GameObject temp = Instantiate(badge,badgeContainer);
            Debug.Log(temp);
        }
        if (Result.Instance.correctBadge) {
            GameObject badge = Resources.Load<GameObject>("Badges/allCorrectBadge");
            GameObject temp = Instantiate(badge,badgeContainer);
            Debug.Log(temp);
        }
        if (Result.Instance.reward == "badge"){
            GameObject badge = Resources.Load<GameObject>("Badges/luckyBadge");
            GameObject temp = Instantiate(badge,badgeContainer);
            Debug.Log(temp);
        }
    }
    
    private void generateScenario(){
        System.Random rnd = new System.Random();
        List<string> keyList = new List<string>(gameTypeList.Keys);
        int r = rnd.Next(keyList.Count);
        string keyvalue = keyList[r];
        while (showingGameType.ContainsKey(keyvalue)){
            r = rnd.Next(keyList.Count);
            keyvalue = keyList[r];
        }
        showingGameType.Add(keyvalue,gameTypeList[keyvalue]);
    }
    private void setUpScenario(){
        foreach(Transform child in scenarioContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var gameType in showingGameType.Keys)
        {
            GameObject senarioButton = Resources.Load<GameObject>("senarioButton");
            GameObject temp = Instantiate(senarioButton,scenarioContainer);
            Button button = temp.GetComponent<Button>();
            TMP_Text textUI = temp.GetComponentInChildren<TMP_Text>();
            textUI.SetText(gameType);
            //add listener to update chosen answer
            button.onClick.AddListener(() => { 
                Result.Instance.gameType = gameTypeList[gameType];
                resetButtonColor();
                button.GetComponent<Image>().color = Color.yellow;
            });
        }
    }
    private void resetButtonColor(){
        for (int i = 0; i < scenarioContainer.childCount; i++)
        {
            Button scenarioButton = scenarioContainer.transform.GetChild(i).gameObject.GetComponent<Button>();
            scenarioButton.GetComponent<Image>().color = Color.white;
        }
    }
}
