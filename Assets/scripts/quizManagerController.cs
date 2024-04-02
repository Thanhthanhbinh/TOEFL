using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using Unity.Collections;
using UnityEngine.UI;


public class quizManagerController : MonoBehaviour
{
    private List<GameObject> questionList = new List<GameObject>(); // list of prefab Question Objects
    //container for the questions
    [SerializeField] private Transform quizContainer;
    [SerializeField] private Transform canvas;
    //container for the lives
    [SerializeField] private Transform livesContainer;
    // reference to the current object
    [SerializeField] private GameObject quizController;
    // parent of the game to contain it
    [SerializeField] private Transform gameContainer;
    [SerializeField] private GameObject ReloadButton;
    [SerializeField] private GameObject scoreContainer;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject hostPanel;
    // the game object
    private GameObject game;
    //number of correctly answered questions
    private int correctAnswersNum;
    // number of answered questions in general
    private int answeredQuestions;
    // the score
    private int score;
    //number of lives
    private int lives ;
    //number of total questions
    private int totalQuestions;
    // private bool hint;
    private int incorrectStreak;
    private bool liveBadge;
    private bool hintBadge;
    // game type
    public string gameType;
    
    void Awake()
    {   
        startExam();
    }

    public void startExam() {
        if (ExamInfo.Instance.gameType == "") {
            gameType = "FlyGame/FlyGame";
            ExamInfo.Instance.gameType = "FlyGame/FlyGame";
        }else {
            gameType = ExamInfo.Instance.gameType;
        }
        if (UserData.Instance.playerType == "host"){
            hostPanel.SetActive(true);
            hostPanel.GetComponentInChildren<hostPanelController>().setUpHostPanel();
            return;
        }else{
            hostPanel.SetActive(false);
        }
        setUpAll();
        resultPanel.SetActive(false);
    }
    private void setUpAll(){
        setUpVar();
        setUpQuestion();
        setUpLives();
        setUpGame();
        setUpBadge();
        ReloadButton.SetActive(false);
    }
    
    private void setUpVar() {
        correctAnswersNum = 0;
        answeredQuestions = 0;
        score = 0;
        totalQuestions = 0;
        incorrectStreak = 0;
        lives = 3;
        if (ExamInfo.Instance.reward == "lives") {
            lives = 4;
        }
    }
    private List<QuestionAnswer> getQuestions(){
        // get the list of questions
        QuizManager manager = new QuizManager("");
        // get the list of questions fromt the FixedString4096Bytes
        List<QuestionAnswer> value = manager.readJSONString(ExamData.Instance.examQuestion);
        return new List<QuestionAnswer>(value);
    }

    //set up the questions in the quiz
    public void setUpQuestion(){
        //make a quiz manager with the given path 
        List<QuestionAnswer> questionPanelList = getQuestions();
        questionList = new List<GameObject>();
        //clear the existing questionPanel being shown
        foreach(Transform child in quizContainer.transform)
        {
            Destroy(child.gameObject);
        }
        // in case client screen is faster than the host to get the questions
        if (questionPanelList == null){
            MessageManager.createMessage("Reconnect with server to load questions",canvas);
            ReloadButton.SetActive(true);
            return;
        }
        //create prefab of a panel of question
        GameObject questionPanel = Resources.Load<GameObject>("questionPanel");
        
        //initialise the questionPanel and add their variables
        foreach (var item in questionPanelList)
        {   
            //get the section
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
                // assign the corresponding quizController object
                temp.GetComponentInChildren<QuestionAnswerController>().quizController = quizController;
                // add the question in the current section
                questionList.Add(temp);
            }
        }
        //update the total number of questions
        totalQuestions = questionList.Count;
        updateScore();
    }


    public void setUpBadge() {
        liveBadge= true;
        ExamInfo.Instance.hintBadge = true;
    }
    public void setUpGame() {
        //remove previous game
        foreach(Transform child in gameContainer.transform)
        {
            Destroy(child.gameObject);
        }
        //initialise the game and add it in
        GameObject gameTypeObject = Resources.Load<GameObject>(gameType);
        GameObject temp = Instantiate(gameTypeObject,gameContainer);
        temp.transform.localPosition = new Vector2(-1,-25);
        game = temp;
        //set up the game
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
        //initialise the lives elements 
        for (int i = 0; i < lives; i++)
        {
            GameObject temp = Instantiate(live,livesContainer);
        }
            
    }
    
    
    private void updateScore(){
        //change the score
        scoreContainer.GetComponentInChildren<Text>().text = score + "/" + totalQuestions;
    }
    //this increases the score and update it and play the game
    public void increaseScore(){
        score = score + 1;
        updateScore();
        game.GetComponentInChildren<gameController>().correctRun();
    }

    // increase the number of answered questions 
    // add more lives if incorrect streak is hit
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

    // use live
    public void revive(){
        if (lives > 0) {
            liveBadge = false;
            increaseScore();
            lives = lives - 1;
            Destroy(livesContainer.transform.GetChild(0).gameObject);
        }
    }

    //add new lives
    public void moreLives() {
        
        lives = lives + 1;
        GameObject live = Resources.Load<GameObject>("live");
        GameObject temp = Instantiate(live,livesContainer);
        
    }
    
    //run for correct question
    public void correctAnswer() {
        increaseAnsweredQuestions();
        increaseScore();
        increaseCorrectAnswersNums();
    }
    //run for incorrect question
    public void incorrectAnswer() {
        incorrectStreak = incorrectStreak + 1;
        increaseAnsweredQuestions();
        game.GetComponentInChildren<gameController>().incorrectRun();
    }

    //check only one question and wait for check to be done
    IEnumerator checkOne(GameObject item) {
            
            Debug.Log("cehcked");
            item.GetComponentInChildren<QuestionAnswerController>().checkAnswer();
            // yield return new WaitForSeconds(2.0f);
            yield return new WaitUntil(() => game.GetComponentInChildren<gameController>().finish() == true);
    }
    //check all question in exam
    IEnumerator checkAll(){
        foreach (var item in questionList)
        {   
            if (!item.GetComponentInChildren<QuestionAnswerController>().isAnswered()){
                yield return StartCoroutine(checkOne(item));
            }
            
        }
    }
    // decide results
    private void setUpResult(){
        ExamInfo.Instance.state = "YOU WIN";
        ExamInfo.Instance.score += score;
        ExamInfo.Instance.total += totalQuestions;
        ExamInfo.Instance.grade += correctAnswersNum;
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
    // show the result panel
    IEnumerator showResult(){
        setUpResult();
        resultPanel.SetActive(true);
        resultPanel.GetComponentInChildren<resultController>().setUpResult();
        yield return new WaitForSeconds(1f);
    }

    IEnumerator ending(){
        yield return StartCoroutine(checkAll());
        yield return StartCoroutine(showResult());
    }
    public void endGame() {
        StartCoroutine(ending());
    }
}
