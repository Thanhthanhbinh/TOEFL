using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class menuController : MonoBehaviour
{   
    private string joinCode;
    [SerializeField] private GameObject relayObject;
    [SerializeField] private GameObject button;
    [SerializeField] private Transform canvas;
    // Start is called before the first frame update
    public void changeToExam() {
        SceneController.changeToExam();
    }

    public void changeToTeacherDashboard() {
        SceneController.changeToTeacherDashboard();
    }
    public void updateName(string input){
        UserData.Instance.name = input;
    }
    
    public void updateJoinCode(string input){
        joinCode = input;
        UserData.Instance.joinCode = joinCode;
    }
    
    public void joinExam(){
        try
        {
            if (joinCode == null || joinCode == "" || UserData.Instance.name == "" || UserData.Instance.name == null){
                MessageManager.createMessage("Enter an exam Code and a Name to join!",canvas);
                return;
            }
            relayObject.GetComponent<RelayController>().JoinGame(joinCode);
        }
        catch (System.Exception)
        {
            MessageManager.createMessage("Error Joining Sever",canvas);
            button.SetActive(true);
        }
        
    }
}
