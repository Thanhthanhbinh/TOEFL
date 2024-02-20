using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class questionAnswerEditController : MonoBehaviour
{
    public GameObject questionPanel;
    public QuestionAnswer content = new QuestionAnswer("", new List<string>() ,"");
    public int answerNum = 4;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(questionPanel.transform.position.y);
        // get question
        GameObject question = questionPanel.transform.GetChild(1).gameObject;
        TMP_InputField questionTxt = question.GetComponent<TMP_InputField>();
        questionTxt.onEndEdit.AddListener(updateQuestion);

        GameObject answerContainer = questionPanel.transform.GetChild(2).gameObject;
        
        for (int i = 0; i < answerContainer.transform.childCount; i++)
        {
            int index = i;
            TMP_InputField answerTxt = answerContainer.transform.GetChild(i).GetComponent<TMP_InputField>();
            answerTxt.onEndEdit.AddListener((input)=>{updateAnswer(input,index);});
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }

// TODO: add function to generate JSON file
    private void updateQuestion(string input) {
        // Debug.Log(input);
        content.setQuestion(input);
    }

    private void updateAnswer(string input, int index) {
        
        content.addAnswers(input, index);
        var value = content.getAnswer();
        content.setCorrect(value[value.Count-1]);
        // foreach (var item in value)
        // {
        //     Debug.Log(item);
        // }
        
    }


    public void deleteQuestion() {
        Destroy(questionPanel);
    }
}
