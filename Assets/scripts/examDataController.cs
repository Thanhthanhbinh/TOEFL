using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using TMPro;

public class examDataController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject questionContainer;
    [SerializeField] private GameObject gradeContainer;
    private List<GameObject> studentData;
    private List<StudentData> examDataList = new List<StudentData>();
    private Dictionary<int,int> gradeList = new Dictionary<int,int>();

    void Awake(){
        setup();
        setUpQuestion();
        setUpGrade();
    }
    private void setup(){
        QuizManager manager = new QuizManager("");
        // get the list of questions fromt the FixedString4096Bytes
        foreach (var item in ExamData.Instance.resultList)
        {
            StudentData value = manager.readJSONStringResult(item);
            examDataList.Add(value);
        }
        
    }
    private void setUpGrade(){
        foreach (var item in examDataList)
        {
            if(gradeList.ContainsKey(item.grade)){
                gradeList[item.grade] +=1;
            }else{
                gradeList[item.grade] =1;
            }
        }
        foreach(Transform child in gradeContainer.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject gradeData = Resources.Load<GameObject>("GradeData");
        foreach (var item in gradeList.Keys)
        {
            GameObject temp = Instantiate(gradeData,gradeContainer.transform);
            string text = item + "/" + ExamData.Instance.questionList.Count + "-" + getPercentageGrade(item)+"%";
            temp.GetComponent<TMP_Text>().SetText(text);
        }
    }
    private void setUpQuestion(){
        foreach(Transform child in questionContainer.transform)
        {
            Destroy(child.gameObject);
        }
        //initialise the questionPanel and add their variables
        GameObject questionData = Resources.Load<GameObject>("questionData");
        foreach (var item in ExamData.Instance.questionList)
        {   

            GameObject temp = Instantiate(questionData,questionContainer.transform);
            temp.GetComponentInChildren<questionDataController>().setQuestion(item.getQuestion());
            int index = item.getIndex();
            for (int i = 0; i < 4; i++)
            {
                string answer = item.getAnswer()[i];
                int percentageValue = getPercentageChosen(answer,index);
                temp.GetComponentInChildren<questionDataController>().setAnswerData(i,answer,percentageValue);
            }
            int correct = getPercentageCorrect(index);
            temp.GetComponentInChildren<questionDataController>().setCorrectData(correct);
            int hint = getPercentageHint(index);
            temp.GetComponentInChildren<questionDataController>().setHintData(hint);
        }
    }
    private int getPercentageChosen(string chosen, int index){
        float counter = 0;
        float total = 0;
        foreach (var item in examDataList)
        {
            QuestionAnswer currentQuestion = getQuestionWithIndex(index,item.data);
            if (currentQuestion.getChosenAnswer() == chosen.Substring(3).Trim()){
                counter += 1;
            }
            total += 1;
        }
        
        return (int)((counter/total)*100);
    }
    private int getPercentageGrade(int grade){
        float counter = 0;
        float total = 0;
        foreach (var item in examDataList)
        {
            if (item.grade == grade){
                counter += 1;
            }
            total += 1;
        }
        return (int)((counter/total)*100);
    }
    private QuestionAnswer getQuestionWithIndex(int index, List<QuestionAnswer> input){
        foreach (var item in input)
        {
            if (item.getIndex() == index){
                return item;
            }
        }
        return null;
    }

    private int getPercentageCorrect(int index){
        float counter = 0;
        float total = 0;
        foreach (var item in examDataList)
        {
            QuestionAnswer currentQuestion = getQuestionWithIndex(index,item.data);
            if (currentQuestion.isCorrect()){
                counter += 1;
            }
            total += 1;
        }
        return (int)((counter/total)*100);
        
    }

    private int getPercentageHint(int index){
        float counter = 0;
        float total = 0;
        foreach (var item in examDataList)
        {
            QuestionAnswer currentQuestion = getQuestionWithIndex(index,item.data);
            if (currentQuestion.isHintUsed()){
                counter += 1;
            }
            total += 1;
        }
        return (int)((counter/total)*100);
        
    }
    public void changeToMenu(){
        SceneController.changeToMenu();
    }
}
