using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class questionAnswerEditController : MonoBehaviour
{
    public GameObject questionPanel;
    public QuestionAnswer content;
    public int answerNum = 4;

    public Transform answerContainer;
    private List<string> answerList;
    // Start is called before the first frame update
    void Start()
    {
        content = new QuestionAnswer("", answerList ,"","easy");        
        answerList = new List<string>();
    }

    // Update is called once per frame

// TODO: add function to generate JSON file
    public void updateQuestion(string input) {
        // Debug.Log(input);
        content.setQuestion(input);
    }

    public void updateAnswer() {
        answerList.Clear();
        for (int i = 0; i < answerContainer.childCount; i++)
        {
            GameObject answer = answerContainer.GetChild(i).gameObject;
            string answerVal = answer.GetComponent<TMP_InputField>().text;
            answerList.Add(convertNumToLetter(i) + " " + answerVal);
        }
        content.setAnswers(answerList);
        // foreach (var item in content.getAnswer())
        // {
        //     Debug.Log(item.Substring(3));
        // }
    }

    public void updateCorrectAnswer(string input) {
        updateAnswer();
        content.setCorrect(input);
        Debug.Log(content.getCorrectAnswer());
    }

    public void updateMode(GameObject dropdown){
        // Debug.Log(dropdown.GetComponent<TMP_Dropdown>().value);
        
        content.setMode(convertNumToMode(dropdown.GetComponent<TMP_Dropdown>().value));
        Debug.Log(content.mode);
        
    }

    public void updateSection(GameObject dropdown){
        int value = dropdown.GetComponent<TMP_Dropdown>().value;
        string maxVal = dropdown.GetComponent<TMP_Dropdown>().options.Count.ToString();
        if (value == 0){
            dropdown.GetComponent<TMP_Dropdown>().AddOptions( new List<string> {"Section " +maxVal }  );
        }else {
            content.setSection(value);
        }
        Debug.Log(content.section);
    }

    private string convertNumToMode(int num) {
        Dictionary<int,string> convertList = new Dictionary<int, string>();

        convertList.Add(0, "easy");
        convertList.Add(1, "hard");

        return convertList[num];
    }
    private string convertNumToLetter(int num) {
        Dictionary<int,string> convertList = new Dictionary<int, string>();

        convertList.Add(0, "(A) ");
        convertList.Add(1, "(B) ");
        convertList.Add(2, "(C) ");
        convertList.Add(3, "(D) ");
        convertList.Add(4, "(E) ");

        return convertList[num];
    }

    public void deleteQuestion() {
        Destroy(questionPanel);
    }
}
