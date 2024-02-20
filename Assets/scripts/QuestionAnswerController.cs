using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionAnswerController : MonoBehaviour
{
    public GameObject questionPanel ; // the game object it is pointed to ?? do i need this
    public QuestionAnswer content; // the question it is associated with

    public int test;

    
    // Start is called before the first frame update
    void Start()
    {
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateUI() {
        
        TMP_Text questionObject = questionPanel.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        questionObject.SetText(content.getQuestion());

        GameObject answersObject = questionPanel.transform.GetChild(2).gameObject;
        int counter = 0;
        List<string> answerList = content.getAnswer();
        foreach (var answer in answerList)
        {
            TMP_Text textUI = answersObject.transform.GetChild(counter).gameObject.GetComponentInChildren<TMP_Text>();
            textUI.SetText(answer);
            counter = counter + 1;
        }
        
    }
}
