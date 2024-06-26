using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class QuestionAnswerController : MonoBehaviour
{
    public GameObject questionPanel ; // the game object it is pointed to 
    public QuestionAnswer content; // the question it is associated with
    public GameObject quizController; // the game object that control the logic of the game
    public GameObject hintButton;

    public GameObject checkButton;
    private Button chosenAnswer;
    private Button correctAnswer;
    private string correctVal;
    private bool answered;
    private bool hint;


    
    // Start is called before the first frame update
    

    public void setUpAll() {
        answered = false;
        hint = true;
        if (content.mode == "hard") {
            Destroy(hintButton);
            Destroy(checkButton);
        }
        updateUI();
    }

    public void updateUI() {
        
        //set up the question text
        TMP_Text questionObject = questionPanel.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        questionObject.SetText(content.getQuestion());

        //this is the parent with all question button
        GameObject answerButtonList = questionPanel.transform.GetChild(2).gameObject;
        int counter = 0;
        //get a list of all the answers
        List<string> answerList = content.getAnswer();
        //add answer text to all answer button
        foreach (var answer in answerList)
        {
            Button answerButton = answerButtonList.transform.GetChild(counter).gameObject.GetComponent<Button>();
            TMP_Text textUI = answerButtonList.transform.GetChild(counter).gameObject.GetComponentInChildren<TMP_Text>();
            textUI.SetText(answer);
            //add listener to update chosen answer
            if (ExamInfo.Instance.examMode == true){
                answerButton.onClick.AddListener(() => { 
                    if (answered == false) {
                        chosenAnswer = answerButton;
                        content.setChosen(answer.Substring(3).Trim());
                        resetButtonColor();
                        answerButton.GetComponent<Image>().color = Color.yellow;
                    }   
                });
            }
            //assign the button with the correct answer a ref
            if (answer.Substring(3).Trim() == content.getCorrectAnswer()){
                correctAnswer = answerButton;
                correctVal = answer;
            }
            counter = counter + 1;
        }
        
    }

    public void allInfoUI() {
        
        //set up the question text
        TMP_Text questionObject = questionPanel.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        questionObject.SetText(content.getQuestion());

        //this is the parent with all question button
        GameObject answerButtonList = questionPanel.transform.GetChild(2).gameObject;
        int counter = 0;
        //get a list of all the answers
        List<string> answerList = content.getAnswer();
        //add answer text to all answer button
        for (int i = 0; i < 4; i++)
        
        {
            string answer = answerList[i];
            Button answerButton = answerButtonList.transform.GetChild(i).gameObject.GetComponent<Button>();
            TMP_Text textUI = answerButtonList.transform.GetChild(i).gameObject.GetComponentInChildren<TMP_Text>();
            textUI.SetText(answer);
            string trimAnswer = answer.Substring(3).Trim();
            //assign the button with the correct answer a ref
            if (trimAnswer== content.getCorrectAnswer()){
                correctAnswer = answerButton;
                correctVal = answer;
            }
            if (trimAnswer == content.getChosenAnswer()){
                chosenAnswer = answerButton;
            }
            counter = counter + 1;
        }
        Debug.Log(counter);
        
    }
    // this reset the color of answer buttons
    private void resetButtonColor(){
        if (ExamInfo.Instance.examMode ){
            GameObject answerButtonList = questionPanel.transform.GetChild(2).gameObject;
            for (int i = 0; i < 4; i++)
            {
                Button answerButton = answerButtonList.transform.GetChild(i).gameObject.GetComponent<Button>();
                answerButton.GetComponent<Image>().color = Color.white;
            }
        }
        
    }
    //change color of the correct answer button
    private void showCorrect(){
        if (correctAnswer != null){
            correctAnswer.GetComponent<Image>().color = Color.green;
        }
        
    }
    // this checks the answer chosen by the player and change the score and color accordingly
    public void checkAnswer() {
        if (answered){
            return;
        }
        ExamInfo.Instance.questionList.Add(content);
        answered = true;
        bool result = content.isCorrect();
        if (chosenAnswer == null){
            quizController.GetComponent<quizManagerController>().incorrectAnswer();
            if (content.mode == "easy") {
                showCorrect();
            }
            
            return;
        }
        if (result) {
            chosenAnswer.GetComponent<Image>().color = Color.green;
            quizController.GetComponent<quizManagerController>().correctAnswer();
            
        }else {
            chosenAnswer.GetComponent<Image>().color = Color.red;
            quizController.GetComponent<quizManagerController>().incorrectAnswer();
            if (content.mode == "easy") {
                showCorrect();
            }
        }
        
    }
    public void showResult() {
        Destroy(checkButton);
        Destroy(hintButton);
        
        bool result = content.isCorrect();
        if (chosenAnswer == null){
            showCorrect();
            return;
        }
        if (result) {
            chosenAnswer.GetComponent<Image>().color = Color.green;
            
        }else {
            chosenAnswer.GetComponent<Image>().color = Color.red;
            showCorrect();
        }
    }

    public bool isAnswered()
    {
        return answered;
    }
    public void showHint() {
        if (!hint) {
            Debug.Log("hint used");
            return;
        }
        content.toggleHint();
        ExamInfo.Instance.hintBadge = false;
        hint = false;
        GameObject answerButtonList = questionPanel.transform.GetChild(2).gameObject;
        List<string> answerList = new List<string>(content.getAnswer());
        answerList.Remove(correctVal);
        System.Random r = new System.Random();
        int rInt = r.Next(0, answerList.Count);
        foreach (Transform item in answerButtonList.transform)
        {
            // Debug.Log(item.gameObject);
            if (item.gameObject.GetComponentInChildren<TMP_Text>().text == answerList[rInt]){
                item.gameObject.GetComponent<Image>().color = Color.red;
            }
        }
    }

    

}
