using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    [SerializeField] private Transform canvas;

    public void changeToExam() {
        SceneController.changeToExam();
    }

    public void changeToTeacherDashboard() {
        SceneController.changeToTeacherDashboard();
    }

    public void changeToExamCreation() {
        SceneController.changeToExamCreation();
    }

    public void changeToResultScene() {
        SceneController.changeToResultScene();
    }

    public void changeToMenu() {
        SceneController.changeToMenu();
    }

    public void changeToFinal() {
        SceneController.changeToFinal();
    }

    public void changeToExamData() {
        SceneController.changeToExamData();
    }

    public void showMessage() {
        MessageManager.createMessage("Message",canvas);
    }
}
