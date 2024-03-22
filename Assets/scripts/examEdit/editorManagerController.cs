using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class editorManagerController : MonoBehaviour
{
    //create prefab of a panel of question
    GameObject editorPanel ;
    public Transform container;
    private string pathStart = "C:/Users/loral/Desktop/unity project/2d platformer sample/PRJ-test/Assets/examJSON/";

    private string exameTitle;
    void Start()
    {
        editorPanel = Resources.Load<GameObject>("editor");
    }
    public void createEditor() {
        // Destroy(container.GetChild(container.childCount - 1).gameObject);
        GameObject temp = Instantiate(editorPanel,container);
    }

    public void saveExam() {
        Debug.Log(container.childCount);
        Debug.Log(exameTitle);
        if (container.childCount > 0 && exameTitle != null && exameTitle != ""){
            Transform content = container.GetChild(0);
            List<QuestionAnswer> data= new List<QuestionAnswer>();
            Transform questionAnswerContainer = content.GetComponentInChildren<questionEditorController>().parent;
            int questionNo = questionAnswerContainer.childCount;
            // Debug.Log(questionNo);
            for (int i = 0; i < questionNo; i++)
            {
                // Debug.Log(questionAnswerContainer.GetChild(i));
                data.Add(questionAnswerContainer.GetChild(i).GetComponentInChildren<questionAnswerEditController>().content);
                // Debug.Log(data[i].getQuestion());
            }
            SerializableList temp = new SerializableList();
            temp.data = data;
            string jsonData = JsonUtility.ToJson(temp);
            Debug.Log(jsonData);
            File.WriteAllText(pathStart+"/" + exameTitle + ".json", jsonData);
            Debug.Log("Saved");
            createMessage("File saved as "+exameTitle + ".json");
        }else{
            Debug.Log("Nothing to Save");
            createMessage("ERROR:Something is missing");
        }
        
    }

    private void createMessage(string input){
        GameObject messageItem = Resources.Load<GameObject>("message");
        GameObject temp = Instantiate(messageItem,container.parent);
        temp.transform.position = new Vector2(411,220);
        temp.GetComponentInChildren<messageController>().updateUI(input);
    }
    public void updateExamName(string input){
        exameTitle = input;
    }
}

[System.Serializable]
public class SerializableList {
    public List<QuestionAnswer> data;
}
