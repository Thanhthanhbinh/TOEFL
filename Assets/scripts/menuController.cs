using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class menuController : MonoBehaviour
{   
    private string joinCode;
    [SerializeField] private GameObject relayObject;
    [SerializeField] private Transform canvas;
    // Start is called before the first frame update
    public void changeToExam() {
        SceneController.changeToExam();
    }

    public void changeToTeacherDashboard() {
        SceneController.changeToTeacherDashboard();
    }
    
    public void updateJoinCode(string input){
        joinCode = input;
    }
    
    public void joinExam(){
        if (joinCode == null){
            MessageManager.createMessage("Enter an exam Code to join!",canvas);
            return;
        }
        relayObject.GetComponent<RelayController>().JoinGame(joinCode);
    }
}
