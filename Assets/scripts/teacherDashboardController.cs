using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using System;



public class teacherDashboardController : MonoBehaviour
{
    // Start is called before the first frame update
    private string pathStart = @"C:/Users/loral/Desktop/unity project/2d platformer sample/PRJ-test/Assets/examJSON";
    public Transform examContainer;

    void Start()
    {
        listExams();
    }
    public void changeToExamEditor() {
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

    

    private void listExams() {
        DirectoryInfo directory = new DirectoryInfo(pathStart);
        FileInfo[] examFiles = directory.GetFiles("*.json");
        GameObject examItem = Resources.Load<GameObject>("examItem");
        foreach(Transform child in examContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var file in examFiles)
        {
            GameObject temp = Instantiate(examItem,examContainer);
            TMP_Text text = temp.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
            text.SetText(file.Name);
            temp.GetComponentInChildren<examItemController>().fileInformation = file;
        }
    }
}
