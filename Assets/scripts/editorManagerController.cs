using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class editorManagerController : MonoBehaviour
{
    //create prefab of a panel of question
    GameObject editorPanel ;
    public Transform container;

    private string pathStart = "C:/Users/loral/Desktop/unity project/2d platformer sample/PRJ-test/Assets/Scripts/";

    void Start()
    {
        editorPanel = Resources.Load<GameObject>("editor");
    }
    public void createEditor() {
        // Destroy(container.GetChild(container.childCount - 1).gameObject);
        GameObject temp = Instantiate(editorPanel,container);
    }

    public void saveExam() {
        Transform content = container.GetChild(container.childCount - 1);
        if (content.name == "editor(Clone)"){
            List<QuestionAnswer> data= new List<QuestionAnswer>();
            Transform questionAnswerContainer = content.GetComponentInChildren<questionEditorController>().parent;
            int questionNo = questionAnswerContainer.childCount;
            // Debug.Log(questionNo);
            for (int i = 1; i < questionNo; i++)
            {
                Debug.Log(questionAnswerContainer.GetChild(i));
                data.Add(questionAnswerContainer.GetChild(i).GetComponentInChildren<questionAnswerEditController>().content);
                // Debug.Log(data[i].getQuestion());
            }
            SerializableList temp = new SerializableList();
            temp.data = data;
            string jsonData = JsonUtility.ToJson(temp);
            Debug.Log(jsonData);
            File.WriteAllText(pathStart+"/result.json", jsonData);


        }
        Debug.Log("Nothing to Save");
    }
}

[System.Serializable]
public class SerializableList {
    public List<QuestionAnswer> data;
}
