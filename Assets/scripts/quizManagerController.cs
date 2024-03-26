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
    // parent of the game to contain it
    public Transform gameContainer;

    public GameObject scoreContainer;
    // the game object
    public GameObject game;
    //number of correctly answered questions
    public int correctAnswersNum;
    // number of answered questions in general
    public int answeredQuestions;
    // the score
    public int score;
    //number of lives
    public int lives;

    private bool liveBadge;
    private bool hintBadge;
    // game type
    public string gameType;
    //number of total questions
    private int totalQuestions;
    // private bool hint;
    private int incorrectStreak;
    void Start()
    {   
        lives = 3;
        if (ExamInfo.Instance.reward == "lives") {
            lives = 4;
        }
        incorrectStreak = 0;
        answeredQuestions = 0;
        totalQuestions = -1;
        if (ExamInfo.Instance.gameType == "") {
            gameType = "RunGame/RunGame";
            ExamInfo.Instance.gameType = "RunGame/RunGame";
        }else {
            gameType = ExamInfo.Instance.gameType;
        }
        setUpQuestion();
        setUpLives();
        setUpGame();
        setUpBadge();
    }

    void Update(){
        // endGame();
    }
    
    //set up the questions in the quiz
    public void setUpQuestion(){
        //make a quiz manager with the given path 
        QuizManager manager = new QuizManager(pathStart + "/Assets/examJSON/trial.json");
        //get the list of questions
        List<QuestionAnswer> questionPanelList = manager.readJSON();
        
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
            Debug.Log(ExamInfo.Instance.section);
            if (ExamInfo.Instance.section.Count == 0){
                ExamInfo.Instance.section[item.section] = "current" ;
            }
            if (!ExamInfo.Instance.section.ContainsKey(item.section)){
                ExamInfo.Instance.section.Add(item.section ,"future"  );
            }
            if (ExamInfo.Instance.section[item.section] == "current"){
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
        //update the total number of questions
        totalQuestions = questionList.Count;
        TMP_Text scoreText = scoreContainer.GetComponentInChildren<TMP_Text>();
        scoreText.SetText(score + "/" + totalQuestions);
    }

    public void setUpBadge() {
        liveBadge= true;
        ExamInfo.Instance.hintBadge = true;
    }
    public void setUpGame() {
        GameObject gameTypeObject = Resources.Load<GameObject>(gameType);
        GameObject temp = Instantiate(gameTypeObject,gameContainer);
        temp.transform.localPosition = new Vector2(0,0);
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

    
    
    private void increaseScore(){
        score = score + 1;
        TMP_Text scoreText = scoreContainer.GetComponentInChildren<TMP_Text>();
        scoreText.SetText(score + "/" + totalQuestions);
        
    }
    //this increases the score
    public void updateScore(){
        increaseScore();
        game.GetComponentInChildren<gameController>().correctRun();
        
    }

    public void increaseAnsweredQuestions(){
        answeredQuestions = answeredQuestions + 1;
        var error = ((float)incorrectStreak / (float)totalQuestions)*100;
        if (error > 40f) {
            Debug.Log(incorrectStreak);
            moreLives();
            incorrectStreak = 0;
        }
    }
    public void increaseCorrectAnswersNums(){
        correctAnswersNum = correctAnswersNum + 1;
    }

    public void revive(){
        if (lives > 0) {
            liveBadge = false;
            increaseScore();
            lives = lives - 1;
            Destroy(livesContainer.transform.GetChild(0).gameObject);
        }
    }

    public void moreLives() {
        
        lives = lives + 1;
        GameObject live = Resources.Load<GameObject>("live");
        GameObject temp = Instantiate(live,livesContainer);
        
    }

    public void correctAnswer() {
        increaseAnsweredQuestions();
        updateScore();
        increaseCorrectAnswersNums();
    }
    public void incorrectAnswer() {
        incorrectStreak = incorrectStreak + 1;
        increaseAnsweredQuestions();
        game.GetComponentInChildren<gameController>().incorrectRun();
    }
    IEnumerator checkOne(GameObject item) {
            // Debug.Log(questionList.Count);
            // while (!game.GetComponentInChildren<gameController>().finish()) {
            //     Debug.Log("not yet");
            //     yield return new WaitForSeconds(1f);
            // }

            item.GetComponentInChildren<QuestionAnswerController>().checkAnswer();
            yield return new WaitForSeconds(2.0f);
    }
    IEnumerator checkAll(){
        foreach (var item in questionList)
        {   
            if (!item.GetComponentInChildren<QuestionAnswerController>().isAnswered()){
                yield return StartCoroutine(checkOne(item));
            }
            
        }
    }

    private void setUpResult(){
        ExamInfo.Instance.state = "YOU WIN";
        ExamInfo.Instance.score = score;
        ExamInfo.Instance.total = totalQuestions;
        ExamInfo.Instance.grade = correctAnswersNum;
        if (score > totalQuestions){
            ExamInfo.Instance.badgeList["aboveTotalBadge"] += 1;
        }
        if (correctAnswersNum == totalQuestions){
            ExamInfo.Instance.badgeList["allCorrectBadge"] += 1;
        }
        if (liveBadge){
            ExamInfo.Instance.badgeList["noLivesBadge"] += 1;
        }
        if (ExamInfo.Instance.hintBadge){
            ExamInfo.Instance.badgeList["noHintBadge"] += 1;
        }
        if (score < totalQuestions){
            ExamInfo.Instance.state = "YOU LOSE";
        }
    }
    IEnumerator changeScence(){
        setUpResult();
        string scence = "ResultScene";
        
        Debug.Log("Change Scene to result screen");
        try 
        {
            SceneManager.LoadScene(scence);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/" + scence+ ".unity");
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator ending(){
        yield return StartCoroutine(checkAll());
        yield return StartCoroutine(changeScence());
    }
    public void endGame() {
        // Debug.Log(answeredQuestions);
        // if (answeredQuestions != totalQuestions) {
        //     return;
        // }
        
        StartCoroutine(ending());
        
    }
}
