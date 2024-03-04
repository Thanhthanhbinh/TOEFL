using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class quizManagerController : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> questionList = new List<GameObject>(); // list of prefab Question Objects
    //path to the JSON files
    private string pathStart = "C:/Users/loral/Desktop/unity project/2d platformer sample/PRJ-test";
    //container for the questions
    public Transform quizContainer;
    //container for the lives
    public Transform livesContainer;
    // reference to the current object
    public GameObject quizController;

    public GameObject gameObject;
    //number of correctly answered questions
    public int correctAnswersNum;
    // the score
    public int score;
    //number of lives
    public int lives;
    //number of total questions
    private int totalQuestions;
    void Start()
    {   
        lives = 3;
        setUpQuestion();
        setUpLives();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //set up the questions in the quiz
    public void setUpQuestion(){
        //make a quiz manager with the given path 
        QuizManager manager = new QuizManager(pathStart + "/Assets/examJSON/result.json");
        //get the list of questions
        List<QuestionAnswer> questionPanelList = manager.readJSON();
        //update the total number of questions
        totalQuestions = questionPanelList.Count;
        TMP_Text scoreText = quizController.transform.parent.gameObject.transform.GetChild(1).GetComponentInChildren<TMP_Text>();
        scoreText.SetText(score + "/" + totalQuestions);
        //create prefab of a panel of question
        GameObject questionPanel = Resources.Load<GameObject>("questionPanel");
        //clear the existing questionPanel being shown
        foreach(Transform child in quizContainer.transform)
        {
            Destroy(child.gameObject);
        }
        //initialise the questionPanel and add their variables
        foreach (var item in questionPanelList)
        {
            GameObject temp = Instantiate(questionPanel,quizContainer);
            //assign them their own object so they can change UI
            temp.GetComponentInChildren<QuestionAnswerController>().questionPanel = temp;
            // assign the corresponding questionAnswer object
            temp.GetComponentInChildren<QuestionAnswerController>().content = item;
            //
            temp.GetComponentInChildren<QuestionAnswerController>().quizController = quizController;
            questionList.Add(temp);
        }
    }
    //set up the lives UI
    public void setUpLives(){
        GameObject live = Resources.Load<GameObject>("live");
        //clear the existing lives being shown
        foreach(Transform child in livesContainer.transform)
        {
            Destroy(child.gameObject);
        }
        //initialise the live 
        for (int i = 0; i < lives; i++)
        {
            GameObject temp = Instantiate(live,livesContainer);
        }
            
    }

    //this increases the score
    public void increaseScore(){
        correctAnswersNum = correctAnswersNum + 1;
        score = score + 1;
        // Debug.Log(score);
        // Debug.Log(quizController.transform.parent.gameObject.transform.GetChild(1));
        TMP_Text scoreText = quizController.transform.parent.gameObject.transform.GetChild(1).GetComponentInChildren<TMP_Text>();
        scoreText.SetText(score + "/" + totalQuestions);

        gameObject.GetComponent<gameController>().correctRun();
    }

    public void revive(){
        if (lives > 0) {
            increaseScore();
            lives = lives - 1;
            Destroy(livesContainer.transform.GetChild(0).gameObject);
        }
    }
}
