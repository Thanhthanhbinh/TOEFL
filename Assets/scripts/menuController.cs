using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class menuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void changeToLobby() {
        Debug.Log("Change Scene to Lobby");
        try 
        {
            SceneManager.LoadScene("Lobby");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/Lobby.unity");
        }  
    }
    public void changeToMenu() {
        Debug.Log("Change Scene to Menu");
        try 
        {
            SceneManager.LoadScene("Menu");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity");
        }    
    }
    public void changeToExam() {
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

    public void changeToTeacherDashboard() {
        Debug.Log("Change Scene to TeacherDashboard");
        try 
        {
            SceneManager.LoadScene("TeacherDashboard");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/TeacherDashboard.unity");
        }
    }
    public void changeToExamCreation() {
        Debug.Log("Change Scene to ExamCreation");
        try 
        {
            SceneManager.LoadScene("ExamCreation");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/ExamCreation.unity");
        }
    }

    
}
