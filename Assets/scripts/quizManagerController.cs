using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;


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

    public Transform gameContainer;
    public GameObject game;
    //number of correctly answered questions
    public int correctAnswersNum;

    public int answeredQuestions;
    // the score
    public int score;
    //number of lives
    public int lives;
    //number of total questions
    public string gameType;
    private int totalQuestions;
    private bool hint;
    void Start()
    {   
        lives = 3;
        hint = false;
        gameType = "RunGame";
        setUpQuestion();
        setUpLives();
        setUpGame();
        
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

    public void setUpGame() {
        GameObject gameTypeObject = Resources.Load<GameObject>(gameType);
        GameObject temp = Instantiate(gameTypeObject,gameContainer);
        temp.transform.position = new Vector2(170,188);
        game = temp;
        Debug.Log(game.GetComponentInChildren<gameController>());
        game.GetComponentInChildren<gameController>().setup(totalQuestions);
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
        score = score + 1;
        // Debug.Log(score);
        // Debug.Log(quizController.transform.parent.gameObject.transform.GetChild(1));
        TMP_Text scoreText = quizController.transform.parent.gameObject.transform.GetChild(1).GetComponentInChildren<TMP_Text>();
        scoreText.SetText(score + "/" + totalQuestions);

        game.GetComponentInChildren<gameController>().correctRun();
    }

    public void increaseAnsweredQuestions(){
        answeredQuestions = answeredQuestions + 1;
        Debug.Log((((float)answeredQuestions-(float)correctAnswersNum) / (float)totalQuestions)*100);
        Debug.Log(((6.0) / 17.0)*100.0);
        var error = (((float)answeredQuestions-(float)correctAnswersNum) / (float)totalQuestions)*100;
        if (error > 40f) {
            moreLives();
        }
    }
    public void increaseCorrectAnswersNums(){
        correctAnswersNum = correctAnswersNum + 1;
    }

    public void revive(){
        if (lives > 0) {
            increaseScore();
            lives = lives - 1;
            Destroy(livesContainer.transform.GetChild(0).gameObject);
        }
    }

    public void moreLives() {
        if (!hint) {
            lives = lives + 1;
            setUpLives();
        }
        
    }

    public void correctAnswer() {
        increaseScore();
        increaseCorrectAnswersNums();
    }
    public void incorrectAnswer() {
        game.GetComponentInChildren<gameController>().incorrectRun();
    }
    public void endGame() {
        if (answeredQuestions != totalQuestions) {
            return;
        }
        string scence = "WinScreen";
        if (score < totalQuestions){
            scence = "FailScreen";
        }
        Debug.Log("Change Scene to end screen");
        try 
        {
            SceneManager.LoadScene(scence);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/" + scence+ ".unity");
        }
    }
}
