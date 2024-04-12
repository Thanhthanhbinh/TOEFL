using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.Collections;
using UnityEngine.SceneManagement;

// using UnityEditor;
using System;
public class SceneController 
{
    public static void changeToExam() {
        Debug.Log("Change Scene to ExamScene");
        try 
        {
            SceneManager.LoadScene("ExamScene");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            // UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/ExamScene.unity");
        }
    }

    public static void changeToTeacherDashboard() {
        Debug.Log("Change Scene to TeacherDashboard");
        try 
        {
            SceneManager.LoadScene("TeacherDashboard");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            // UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/TeacherDashboard.unity");
        }
    }

    public static void changeToExamCreation() {
        Debug.Log("Change Scene to ExamCreation");
        try 
        {
            SceneManager.LoadScene("ExamCreation");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            // UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/ExamCreation.unity");
        }
    }

    public static void changeToResultScene() {
        Debug.Log("Change Scene to ResultScene");
        try 
        {
            SceneManager.LoadScene("ResultScene");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            // UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/ResultScene.unity");
        }
    }

    public static void changeToMenu() {
        Debug.Log("Change Scene to Menu");
        try 
        {
            SceneManager.LoadScene("Menu");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            // UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity");
        }
    }

    public static void changeToFinal() {
        Debug.Log("Change Scene to Final");
        try 
        {
            SceneManager.LoadScene("Final");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            // UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/Final.unity");
        }
    }

    public static void changeToExamData() {
        Debug.Log("Change Scene to ExamData");
        try 
        {
            SceneManager.LoadScene("ExamData");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            // UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/ExamData.unity");
            
        }
    }
}
