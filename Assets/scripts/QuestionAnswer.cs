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
    public string correctAnswer; // the correct answer e.g 'have'
    private string chosenAnswer; // the chosen answer e.g 'A'

    [SerializeField]
    private Dictionary<string,string> answerList = new Dictionary<string, string>(); // the List of answers e.g 'A':'have'


    public QuestionAnswer(string inputQuestion, List<string> inputAnswer,string inputCorrectAnswer) {
        question = inputQuestion;
        correctAnswer = inputCorrectAnswer;
        answer = inputAnswer;
        setAnswers(inputAnswer);
        // dispalyAnswers();
    }

    public bool isCorrect() {
        return correctAnswer == answerList[chosenAnswer];
    }

    public void setChosen(string chosen) {
        chosenAnswer = chosen;
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

    //initialise the answer dictionary
    public void setAnswers(List<string> input) {
        answerList.Clear();
        for (var i = 0; i < input.Count; i++) {
            string key = convertNumToLetter(i);
            answerList.Add(key,input[i]);
        }
    }

    public void setQuestion(string input) {
        question = input;
    }

    public void setCorrect(string input) { 
        correctAnswer = input;
    }
    public void addAnswers(string input, int index) {
        if (answer.Count > 4) {
            answer.Clear();
        }
        answer.Add(input);
        string key = convertNumToLetter(index);
        if (answerList.ContainsKey(key)) {
            answerList[key] = input;
        }else {
            answerList.Add(key,input);
        }
        
    }

    public List<string> getAnswer() {
        return answer;
    }

    public Dictionary<string,string> getAnswerList() {
        return answerList;
    }

    public string getQuestion() {
        return question;
    }

    public void printAnswer() {
        Debug.Log(answer);
        foreach(var value in answer)
            {
                Debug.Log(value);
            }
    }
    
}
