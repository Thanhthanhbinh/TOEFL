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
    public GameObject questionInput;
    public GameObject sectionInput;
    public GameObject correctInput;
    public GameObject modeInput;
    private List<string> answerList;
    // Start is called before the first frame update
    void Start()
    {
        // content = new QuestionAnswer("", answerList ,"","easy");        
        answerList = content.getAnswer();
        setUpContent();
    }

    // Update is called once per frame

    private void setUpContent(){
        //use a copy because the list is always updating when changed
        List<string> temp = new List<string>(content.getAnswer());
        for (int i = 0; i < 4; i++)
        {
            if (i < temp.Count){
                changeInput(temp[i].Substring(3).Trim(),i);
            }
        }
        questionInput.GetComponent<TMP_InputField>().text = content.getQuestion();

        int maxVal = sectionInput.GetComponent<TMP_Dropdown>().options.Count;
        Debug.Log(content.section + "compare to" + maxVal);
        Debug.Log(content.section > maxVal-1);
        if (content.section > maxVal-1 ){
            for (int i = 0; i < content.section - (maxVal-1); i++)
            {
                sectionInput.GetComponent<TMP_Dropdown>().AddOptions( new List<string> {"Section " +maxVal }  );
            }
            maxVal = sectionInput.GetComponent<TMP_Dropdown>().options.Count;
        }
            
        sectionInput.GetComponent<TMP_Dropdown>().value = content.section;
        
        modeInput.GetComponent<TMP_Dropdown>().value = modeInput.GetComponent<TMP_Dropdown>().options.FindIndex(option => option.text == content.mode);
        
        correctInput.GetComponent<TMP_Dropdown>().value = correctInput.GetComponent<TMP_Dropdown>().options.FindIndex(option => option.text == content.getCorrectAnswer());


    }

    private void changeInput(string inputVal, int index){
        GameObject temp = answerContainer.GetChild(index).gameObject;
        temp.GetComponent<TMP_InputField>().text  = inputVal;
    }

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

    public void updateCorrectAnswer(GameObject dropdown) {
        int selectedIndex = dropdown.GetComponent<TMP_Dropdown>().value;
        string correct = dropdown.GetComponent<TMP_Dropdown>().options[selectedIndex].text;
        content.setCorrect(correct);
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

    public void updateAnswerOptions(GameObject dropdown){
        List<string> inputList = new List<string>();
        for (int i = 0; i < answerContainer.childCount; i++)
        {
            GameObject answer = answerContainer.GetChild(i).gameObject;
            string answerVal = answer.GetComponent<TMP_InputField>().text;
            inputList.Add(answerVal);
        }
        dropdown.GetComponent<TMP_Dropdown>().ClearOptions();
        dropdown.GetComponent<TMP_Dropdown>().AddOptions(inputList);
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
