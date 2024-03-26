using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class resultController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scoreObject;
    public GameObject title;
    public Transform badgeContainer;
    public Transform scenarioContainer;
    public GameObject rewardButton;
    public GameObject resultImage;

    private List<string> rewardList;
    private Dictionary<string,string> gameTypeList ;
    private Dictionary<string,string> showingGameType;
    private Button chosenScenario;
    void Start()
    {   
        gameTypeList = new Dictionary<string,string>{{"JellyServe","JumpGame/JumpGame"},{"GarlicAvoid","RunGame/RunGame"},{"SoccerKick","ShootGame/ShootGame"}};
        showingGameType = new Dictionary<string,string>();
        rewardList = new List<string>{"lives","scenario","badge"};
        setUpImage();
        generateScenario();
        scoreObject.GetComponentInChildren<TMP_Text>().SetText(ExamInfo.Instance.score + "/" + ExamInfo.Instance.total);
        title.GetComponentInChildren<TMP_Text>().SetText(ExamInfo.Instance.state);
        setUpBadge();
        setUpScenario();
        setUpReward();
    }

    public void setUpReward(){
        if (ExamInfo.Instance.state == "YOU LOSE"){
            Destroy(rewardButton);
        }
    }
    public void nextSection(){
        if (chosenScenario == null){
            createMessage("Choose a Scenario before continue to next section.");
            return;
        }
        int currentSection = 0;
        int nextSection = 0;
        foreach (var section in ExamInfo.Instance.section.Keys)
        {
            if (ExamInfo.Instance.section[section] == "current"){
                currentSection = section;
            }
            if (ExamInfo.Instance.section[section] == "future"){
                if (nextSection != 0){
                    continue;
                }else {
                    nextSection = section;
                }
            }
        }
        ExamInfo.Instance.section[currentSection] = "past";
        ExamInfo.Instance.section[nextSection] = "current";
        changeToExam();
    }
    public void giveReward(){
        System.Random rnd = new System.Random();
        int r = rnd.Next(rewardList.Count);
        ExamInfo.Instance.reward = rewardList[r];
        Debug.Log(ExamInfo.Instance.reward);
        if (ExamInfo.Instance.reward == "scenario") {
            generateScenario();
            setUpScenario();
        }
        if (ExamInfo.Instance.reward == "badge") {
            ExamInfo.Instance.badgeList["luckyBadge"] += 1;
            setUpBadge();
        }
        if (ExamInfo.Instance.reward == "lives"){
            createMessage("You will have an additional live next section");
        }
        Destroy(rewardButton);
    }
    
    private void setUpBadge(){
        foreach(Transform child in badgeContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var badge in ExamInfo.Instance.badgeList.Keys)
        {
            for (int i = 0; i < ExamInfo.Instance.badgeList[badge]; i++)
            {
                Debug.Log(badge);
                GameObject badgeItem = Resources.Load<GameObject>("Badges/" + badge);
                GameObject temp = Instantiate(badgeItem,badgeContainer);
            }
            
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
                ExamInfo.Instance.gameType = gameTypeList[gameType];
                resetButtonColor();
                button.GetComponent<Image>().color = Color.yellow;
                chosenScenario = button;
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

    private void setUpImage(){
        string state = "_lose";
        string game = ExamInfo.Instance.gameType.Split('/')[0];
        if (ExamInfo.Instance.state == "YOU WIN"){
            state = "_win";
        }
        Debug.Log("Result/"+game +state);
        Sprite win = Resources.Load<Sprite>("Result/"+game +state );
        resultImage.GetComponent<Image>().sprite = win;
    }

    private void changeToExam() {
        Debug.Log("Change Scene to ExamScene");
        try 
        {
            SceneManager.LoadScene("ExamScene");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/ExamScene.unity");
        }
    }

    private void createMessage(string input){
        GameObject messageItem = Resources.Load<GameObject>("message");
        GameObject temp = Instantiate(messageItem,scenarioContainer.parent);
        temp.transform.position = new Vector2(411,220);
        temp.GetComponentInChildren<messageController>().updateUI(input);
    }
}
