using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class QuizManager 
{

    private List<GameObject> questionList; // list of Question Objects
    private string jsonFileName = ""; //name of question file

    public QuizManager(string inputFileName) {
        jsonFileName = inputFileName;
    }

    // read jsonFileName to populate questionList
    public List<QuestionAnswer> readJSON() {
        // List<TestingQ> test = new List<TestingQ>() ;
        // TestingQ temp = new TestingQ();
        // temp.question = "What is past tense of 'have'?";
        // temp.answer = new List<string> {"had", "have", "haved", "haded"};
        // temp.correctAnswer = "had";

        // TempQuestionList trial = new TempQuestionList();
        // test.Add(temp);
        // trial.data = test;

        
        // string json = JsonUtility.ToJson(trial);
        // Debug.Log("json.ToString()");
        // Debug.Log(json.ToString());

        // TempQuestionList jsonData = JsonUtility.FromJson<TempQuestionList>(json);
        // Debug.Log(jsonData.data);
        // foreach (var item in jsonData.data)
        // {
        //     foreach (var i in item.answer)
        //         {
        //             Debug.Log(i);
                    
        //         }
            
        // }

        string jsonString = File.ReadAllText(jsonFileName);
        // Debug.Log(jsonString);
        TempQuestionList jsonData;
        jsonData = JsonUtility.FromJson<TempQuestionList>(jsonString);

        return jsonData.data;
        
        
    }

    // go through questionList and call generateUI on them
    public void generateQuestions() {
        
        // go though each read JSON and spawn new questionPanel prefab
    }
}
    //classes used to get data from JSON file

    //
    [System.Serializable]
    public class TestingQ
    {
        [SerializeField]
        public string question; // the title of the question
        [SerializeField]
        public List<string> answer = new List<string>(); // the List of answers e.g 'A':'have'
        
        [SerializeField]
        public string correctAnswer;

    }

    // the JSON file data is in the form of a list of QuestionAnswer
    [System.Serializable]
    public class TempQuestionList
    {
        public List<QuestionAnswer> data; 
        // the json must have 'data' as the key for the list of object

    }
