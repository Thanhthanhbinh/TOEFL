using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.Collections;

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
        string jsonString = File.ReadAllText(jsonFileName);
        // Debug.Log(jsonString);
        TempQuestionList jsonData;
        jsonData = JsonUtility.FromJson<TempQuestionList>(jsonString);
        return jsonData.data;
        
    }

    public List<QuestionAnswer> readJSONString(FixedString4096Bytes jsonString) {
        string value = jsonString.ToString();
        TempQuestionList jsonData;
        jsonData = JsonUtility.FromJson<TempQuestionList>(value);
        return jsonData.data;
    }


    public StudentData readJSONStringResult(FixedString4096Bytes jsonString) {
        string value = jsonString.ToString();
        StudentData jsonData;
        jsonData = JsonUtility.FromJson<StudentData>(value);
        return jsonData;
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

    [System.Serializable]
    public class TempDataList
    {
        public List<StudentData> info; 
        // the json must have 'data' as the key for the list of object
    }

    // the JSON file data is in the form of a list of QuestionAnswer
    [System.Serializable]
    public class StudentData
    {
        public List<QuestionAnswer> data; 

        public int grade;
        public int maximumGrade;
        // the json must have 'data' as the key for the list of object
        public StudentData(List<QuestionAnswer> inputData, int inputGrade, int inputMax){
            data = inputData;
            grade = inputGrade;
            maximumGrade = inputMax;
        }

    }
    
