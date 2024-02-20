using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quizManagerController : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> questionList = new List<GameObject>(); // list of prefab Question Objects
    private string pathStart = "C:/Users/loral/Desktop/unity project/2d platformer sample/PRJ-test";
    
    public Transform quizContainer;
    void Start()
    {
        //make a quiz manager with the given path 
        //TODO: MAKES USER INPUT FOR THE PATH OF THE JSON
        QuizManager manager = new QuizManager(pathStart + "/Assets/examJSON/result.json");
        Debug.Log(quizContainer.gameObject);
        //get the list of questions
        List<QuestionAnswer> questionPanelList = manager.readJSON();
        //create prefab of a panel of question
        GameObject questionPanel = Resources.Load<GameObject>("questionPanel");
        
        foreach(Transform child in quizContainer.transform)
        {
            Destroy(child.gameObject);
        }
        int yPos = -143;
        foreach (var item in questionPanelList)
        {
            GameObject temp = Instantiate(questionPanel,quizContainer);
            //assign them their own object so they can change UI
            temp.GetComponentInChildren<QuestionAnswerController>().questionPanel = temp;
            // assign the corresponding questionAnswer object
            temp.GetComponentInChildren<QuestionAnswerController>().content = item;
            //
            // temp.transform.position = new Vector3(temp.transform.position.x, yPos, temp.transform.position.z);
            yPos = yPos - 262;
            questionList.Add(temp);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
