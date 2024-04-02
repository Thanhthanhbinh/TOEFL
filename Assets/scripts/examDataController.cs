using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class examDataController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject questionContainer;
    [SerializeField] private GameObject gradeContainer;
    private List<GameObject> studentData;
    private List<QuestionAnswer> examQuestions = new List<QuestionAnswer>();
    private void setup(){
        QuizManager manager = new QuizManager("");
        // get the list of questions fromt the FixedString4096Bytes
        List<QuestionAnswer> value = manager.readJSONStringResult(ExamData.Instance.examQuestion);
        
        //go through all gameobject of student data
        //each has grade and total questions
        //each has a questionList with information 
        //  isCorrect()
        //  isHintUsed()
        //  getChosenAnswer()
    }
}
