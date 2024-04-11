using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class questionDataController : MonoBehaviour
{
    [SerializeField] private GameObject question;
    [SerializeField] private GameObject answerContainer;
    [SerializeField] private GameObject dataContainer;

    [SerializeField] private GameObject correctContainer;
    [SerializeField] private GameObject hintContainer;

    public void setCorrectData(int input){
        correctContainer.GetComponent<TMP_Text>().SetText(input + "%");
    }
    public void setHintData(int input){
        hintContainer.GetComponent<TMP_Text>().SetText(input + "%");
    }

    public void setQuestion(string input){
        question.GetComponent<TMP_Text>().SetText(input);
    }
    
    public void setAnswerData(int index, string answer, int data){
        GameObject currentAnswer = answerContainer.transform.GetChild(index).gameObject;
        GameObject currentData = dataContainer.transform.GetChild(index).gameObject;
        currentAnswer.GetComponentInChildren<TMP_Text>().SetText(answer);
        currentData.GetComponentInChildren<TMP_Text>().SetText(data+"%");
    }
}
