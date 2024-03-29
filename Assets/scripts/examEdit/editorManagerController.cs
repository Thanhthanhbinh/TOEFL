using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class editorManagerController : MonoBehaviour
{
    //create prefab of a panel of question
    private GameObject editorPanel ;
    [SerializeField] private  Transform canvas;
    [SerializeField] private  Transform editorContainer;
    [SerializeField] private  GameObject examNameInput;
    [SerializeField] private  GameObject fileDropDown;
    private string pathStart = "C:/Users/loral/Desktop/unity project/2d platformer sample/PRJ-test/Assets/examJSON/";
    private string exameTitle;
    private GameObject editor;
    void Start()
    {
        editorPanel = Resources.Load<GameObject>("editor");
        setUpFileDropDown();
    }
    public void createEditor() {
        //clear existing editor
        foreach(Transform child in editorContainer)
        {
            Destroy(child.gameObject);
        }
        GameObject temp = Instantiate(editorPanel,editorContainer);
        editor = temp;
    }

    public void saveExam() {
        //check if file name, editor is there
        if (editor != null && exameTitle != null && exameTitle != ""){
            //get editor
            Transform content = editor.transform;
            List<QuestionAnswer> data= new List<QuestionAnswer>();
            //get the scroll view
            Transform questionAnswerContainer = content.GetComponentInChildren<questionEditorController>().parent;
            int questionNo = questionAnswerContainer.childCount;
            //iterate through the children to get the QuestionAnswer object of the gameobject
            for (int i = 0; i < questionNo; i++)
            {
                data.Add(questionAnswerContainer.GetChild(i).GetComponentInChildren<questionAnswerEditController>().content);
            }
            SerializableList temp = new SerializableList();
            temp.data = data;
            //turn to JSON
            string jsonData = JsonUtility.ToJson(temp);
            //save at location
            File.WriteAllText(pathStart+"/" + exameTitle + ".json", jsonData);
            Debug.Log("Saved");
            MessageManager.createMessage("File saved as "+exameTitle + ".json",canvas);
        }else{
            Debug.Log("Nothing to Save");
            MessageManager.createMessage("Missing information. \n Cannot save",canvas);
        }
        
    }

    
    public void updateExamName(string input){
        exameTitle = input;
    }

    public void setUpFileDropDown(){
        //create a list of file name to be dropdown options
        List<string> fileList = new List<string>();
        //look create a list of files with JSON
        DirectoryInfo directory = new DirectoryInfo(pathStart);
        FileInfo[] examFiles = directory.GetFiles("*.json");
        //add the file name into the dropdown
        foreach (var file in examFiles)
        {
            fileList.Add(file.Name);
        }
        //update dropdown by clearing it and add new
        fileDropDown.GetComponent<TMP_Dropdown>().ClearOptions();
        fileDropDown.GetComponent<TMP_Dropdown>().AddOptions(fileList);
    }
    public void setUpExam(){
        //get index of dropdown value
        int selectedIndex = fileDropDown.GetComponent<TMP_Dropdown>().value;
        //get dropdown value at the index
        string fileName = fileDropDown.GetComponent<TMP_Dropdown>().options[selectedIndex].text;
        //remake editor
        createEditor();
        //make a quiz manager with the given path 
        QuizManager manager = new QuizManager(pathStart + fileName);
        //get the list of questions
        List<QuestionAnswer> questionPanelList = manager.readJSON();
        //update file name
        examNameInput.GetComponent<TMP_InputField>().text  = fileName.Split('.')[0];
        foreach (var item in questionPanelList)
        { 
            //create prefab of a panel of question
            editor.GetComponentInChildren<questionEditorController>().addQuestion(item);
        
        }
        
    }
    public void changeToMenu() {
        SceneController.changeToMenu();
    }
}

[System.Serializable]
public class SerializableList {
    public List<QuestionAnswer> data;
}
