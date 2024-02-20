using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionEditorController : MonoBehaviour
{
    public Transform parent;

    
    public void addNewQuestions() {
        // Transform parent = editorContainer.transform.GetChild(0).GetChild(1);
        GameObject questionEditPanel = Resources.Load<GameObject>("questionAnswerEdit");
        GameObject temp = Instantiate(questionEditPanel,parent);
        //assign them their own object so they can change UI
        temp.GetComponentInChildren<questionAnswerEditController>().questionPanel = temp;
        // Debug.Log(temp.transform.position.y);
        int siblingNo = parent.transform.childCount-1;
        temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y - 350*siblingNo, temp.transform.position.z);
        
    }
}
