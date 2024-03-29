using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class resultController : MonoBehaviour
{
    [SerializeField] private GameObject scoreObject;
    [SerializeField] private GameObject title;
    [SerializeField] private Transform badgeContainer;
    [SerializeField] private Transform scenarioContainer;
    [SerializeField] private Transform canvas;

    [SerializeField] private GameObject rewardButton;
    [SerializeField] private GameObject resultImage;

    private Dictionary<string,string> showingGameType;
    private Button chosenScenario;
    void Start()
    {   
        showingGameType = new Dictionary<string,string>();
        setUpImage();
        generateScenario();
        scoreObject.GetComponentInChildren<Text>().text = ExamInfo.Instance.score + "/" + ExamInfo.Instance.total;
        title.GetComponent<Text>().text = ExamInfo.Instance.state;
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
            MessageManager.createMessage("Choose a Scenario before continue to next section.",canvas);
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
        if (nextSection == 0){
            changeToFinal();
            return;
        }
        changeToExam();
    }
    public void giveReward(){
        System.Random rnd = new System.Random();
        int r = rnd.Next(ExamInfo.Instance.rewardList.Count);
        ExamInfo.Instance.reward = ExamInfo.Instance.rewardList[r];
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
            MessageManager.createMessage("You will have an additional live next section",canvas);
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
        List<string> keyList = new List<string>(ExamInfo.Instance.gameTypeList.Keys);
        int r = rnd.Next(keyList.Count);
        for (int i = 0; i < 2; i++)
        {
            string keyvalue = keyList[r];
            while (showingGameType.ContainsKey(keyvalue)){
                r = rnd.Next(keyList.Count);
                keyvalue = keyList[r];
            }
            showingGameType.Add(keyvalue,ExamInfo.Instance.gameTypeList[keyvalue]);
            r = rnd.Next(keyList.Count);
        }
        
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
            temp.GetComponentInChildren<Text>().text = gameType;
            //add listener to update chosen answer
            button.onClick.AddListener(() => { 
                ExamInfo.Instance.gameType = ExamInfo.Instance.gameTypeList[gameType];
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
        ExamInfo.Instance.pictureList.Add("Result/"+game +state );
    }

    private void changeToExam() {
        SceneController.changeToExam();
    }

    private void changeToFinal() {
        SceneController.changeToFinal();
    }

    
}
