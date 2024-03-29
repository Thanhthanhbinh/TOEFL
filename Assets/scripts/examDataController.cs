using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class examDataController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject questionContainer;
    public GameObject gradeContainer;
    private List<GameObject> studentData;
    private List<QuestionAnswer> examQuestions = new List<QuestionAnswer>();
    private void setup(){
        //go through all gameobject of student data
        //each has grade and total questions
        //each has a questionList with information 
        //  isCorrect()
        //  isHintUsed()
        //  getChosenAnswer()
    }
}
