using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using System;
using UnityEngine.UI;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Collections;




public class teacherDashboardController : MonoBehaviour
{
    // Start is called before the first frame update
    private string pathStart = @"C:/Users/loral/Desktop/unity project/2d platformer sample/PRJ-test/Assets/examJSON/";
    [SerializeField] private Transform examContainer;
    [SerializeField] private GameObject studentText;
    [SerializeField] private GameObject examText;
    [SerializeField] private GameObject examInfo;
    [SerializeField] private GameObject examPanel;
    [SerializeField] private GameObject clientPanel;
    [SerializeField] private GameObject clientExamInfo;
    [SerializeField] private GameObject relayObject;
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject createExamButton;
    private int maxPlayers;
    private string filePath;
    // public List<QuestionAnswer> ExamData = new List<QuestionAnswer>();

    void Start()
    {
        listExams();
        //only show examPanel when an exam is happening
        examPanel.SetActive(false);
        if (UserData.Instance.playerType == "client"){
            clientPanel.SetActive(true);
        }
        if (UserData.Instance.playerType == "host"){
            clientPanel.SetActive(false);
        }
        if (UserData.Instance.playerType == ""){
            clientPanel.SetActive(false);
        }
        clientExamInfo.GetComponent<Text>().text = "Waiting for exam with code \n" + UserData.Instance.joinCode;
    }



    public void updateStudentNumber(System.Single input){
        maxPlayers = (int)input;
        studentText.GetComponent<Text>().text = maxPlayers + " Students";
    }

    private void updateLobbyData(string input) {
        filePath = pathStart + input;
        examText.GetComponent<Text>().text = "Exam: "+ input;
        //create the json string of all question data
        getExamQuestion();
    }

    private void getExamQuestion(){
        //create fixstring to be shared in the relay server
        FixedString4096Bytes jsonString = File.ReadAllText(filePath);
        QuizManager manager = new QuizManager(filePath);
        // get the list of questions from
        ExamData.Instance.questionList =  manager.readJSON();
        ExamData.Instance.examQuestion = jsonString;
    }

    public void changeToExamEditor() {
        SceneController.changeToExamCreation();
    }

    public void changeToMenu() {
        SceneController.changeToMenu();
    }
    

    private void listExams() {
        //go into the directory and get all file with JSON
        DirectoryInfo directory = new DirectoryInfo(pathStart);
        FileInfo[] examFiles = directory.GetFiles("*.json");
        // show all files name into the scrollview
        GameObject examItem = Resources.Load<GameObject>("examItem");
        // clear scroll view
        foreach(Transform child in examContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var file in examFiles)
        {
            GameObject temp = Instantiate(examItem,examContainer);
            //update file name text
            Text textItem = temp.transform.GetChild(1).gameObject.GetComponent<Text>();
            textItem.text = "> " + file.Name;
            Button selectButton = temp.GetComponentInChildren<Button>();
            // add listener to update the file name if button is clicked as they are selected
            selectButton.onClick.AddListener(() => { 
                updateLobbyData(file.Name);
            });
        }
    }
    
    
    //check if we got all data for a relay server
    private bool hasAllData(){
        if (filePath == null){
            
            MessageManager.createMessage("Choose an Exam in the list!",canvas);
            return false;
        }
        if (maxPlayers <= 0){
            MessageManager.createMessage("Choose student numbers larger than 0.",canvas);
            return false;
        }
        return true;
    }

    public void setUpExamPanel(){
        examInfo.GetComponent<Text>().text = "Exam Code \n" + UserData.Instance.joinCode;
        examPanel.SetActive(true);
    }
    public void createGame(){
        if (!hasAllData()){
            return;
        }
        try {
            relayObject.GetComponent<RelayController>().CreateGame(maxPlayers);
        }catch (Exception e)
        {
            Debug.Log(e);
            MessageManager.createMessage("There was an error when starting exam. \n Try again.",canvas);
        }
        
    }

    public void startGame() {
        relayObject.GetComponent<RelayController>().StartGame();
        examPanel.SetActive(false);
    }
    public void endGame() {
        relayObject.GetComponent<RelayController>().EndGame();
        createExamButton.SetActive(true);
        examPanel.SetActive(false);
        clientPanel.SetActive(false);
    }
    
}
