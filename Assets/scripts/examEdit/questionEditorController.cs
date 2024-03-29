using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class questionEditorController : MonoBehaviour
{
    public Transform parent;

    
    public void addNewQuestions() {
        GameObject questionEditPanel = Resources.Load<GameObject>("questionAnswerEdit");
        GameObject temp = Instantiate(questionEditPanel,parent);
        //assign them their own object so they can change UI
        temp.GetComponentInChildren<questionAnswerEditController>().questionPanel = temp;
        //assign empty questionAnswer object
        temp.GetComponentInChildren<questionAnswerEditController>().content = new QuestionAnswer("", new List<string>() ,"","easy");
        
    }
    public void addQuestion(QuestionAnswer input){
        GameObject questionEditPanel = Resources.Load<GameObject>("questionAnswerEdit");
        GameObject temp = Instantiate(questionEditPanel,parent);
        //assign them their own object so they can change UI
        temp.GetComponentInChildren<questionAnswerEditController>().questionPanel = temp;
        //assign the given questionAnswer object
        temp.GetComponentInChildren<questionAnswerEditController>().content = input;
        
    }
}
