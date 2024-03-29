using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


// this is a class that represent a question that has ID, answers, correct answer
[System.Serializable]
public class QuestionAnswer 
{
    [SerializeField]
    public string question; // the title of the question
    [SerializeField]
    public List<string> answer = new List<string>(); // the List of answers e.g ['have']
    [SerializeField]
    public string mode;
    [SerializeField]

    public int section;
    [SerializeField]
    public string correctAnswer; // the correct answer e.g 'have'
    private string chosenAnswer; // the chosen answer e.g 'have'

    [SerializeField] private bool hintUsed;

    public QuestionAnswer(string inputQuestion, List<string> inputAnswer,string inputCorrectAnswer,string inputMode) {
        question = inputQuestion;
        correctAnswer = inputCorrectAnswer;
        answer = inputAnswer;
        mode = inputMode;
        section = 1;
        hintUsed = false;
        // dispalyAnswers();
    }

    public bool isCorrect() {
        return correctAnswer == chosenAnswer;
    }

    public void setChosen(string chosen) {
        chosenAnswer = chosen;
    }

    public void setSection(int inputSection) {
        section = inputSection;
    }

    public void toggleHint() {
        hintUsed = false;
    }

    public bool isHintUsed() {
        return hintUsed;
    }


    private string convertNumToLetter(int num) {
        Dictionary<int,string> convertList = new Dictionary<int, string>();

        convertList.Add(0, "A");
        convertList.Add(1, "B");
        convertList.Add(2, "C");
        convertList.Add(3, "D");
        convertList.Add(4, "E");

        return convertList[num];
    }

    

    public void setQuestion(string input) {
        question = input;
    }

    public void setCorrect(string input) { 
        correctAnswer = input;
    }
    public void setAnswers(List<string> input) {
        answer = input;
    }

    public List<string> getAnswer() {
        return answer;
    }

    

    public string getQuestion() {
        return question;
    }

    public string getCorrectAnswer(){
        return correctAnswer;
    }

    public string getChosenAnswer(){
        return chosenAnswer;
    }

    public void setMode(string inputMode){
        mode = inputMode;
    }

    public void printAnswer() {
        Debug.Log(answer);
        foreach(var value in answer)
            {
                Debug.Log(value);
            }
    }
    

}
