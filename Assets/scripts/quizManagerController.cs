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

    private bool liveBadge;
    private bool hintBadge;
    // game type
    public string gameType;
    //number of total questions
    private int totalQuestions;
    // private bool hint;
    private int incorrectStreak;
    void Awake()
    {   
        lives = 3;
        if (ExamInfo.Instance.reward == "lives") {
            lives = 4;
        }
        incorrectStreak = 0;
        answeredQuestions = 0;
        totalQuestions = -1;
        if (ExamInfo.Instance.gameType == "") {
            gameType = "JumpGame/JumpGame";
            ExamInfo.Instance.gameType = "JumpGame/JumpGame";
        }else {
            gameType = ExamInfo.Instance.gameType;
        }
        setUpAll();
    }

    
    public void setUpAll(){
        setUpQuestion();
        setUpLives();
        setUpGame();
        setUpBadge();
        ReloadButton.SetActive(false);
    }
    
    private List<QuestionAnswer> getQuestions(){
        // get the list of questions
        QuizManager manager = new QuizManager("");
        List<QuestionAnswer> value = manager.readJSONString(ExamData.Instance.examQuestion);
        
        return new List<QuestionAnswer>(value);
    }
    //set up the questions in the quiz
    public void setUpQuestion(){
        //make a quiz manager with the given path 
        List<QuestionAnswer> questionPanelList = getQuestions();
        //clear the existing questionPanel being shown
        foreach(Transform child in quizContainer.transform)
        {
            Destroy(child.gameObject);
        }
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
        updateScore();
    }

    public void setUpBadge() {
        liveBadge= true;
        ExamInfo.Instance.hintBadge = true;
    }
    public void setUpGame() {
        GameObject gameTypeObject = Resources.Load<GameObject>(gameType);
        GameObject temp = Instantiate(gameTypeObject,gameContainer);
        temp.transform.localPosition = new Vector2(-1,-25);
        game = temp;
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

    
    
    private void updateScore(){
        scoreContainer.GetComponentInChildren<Text>().text = score + "/" + totalQuestions;
    }
    //this increases the score
    public void increaseScore(){
        score = score + 1;
        updateScore();
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
            updateScore();
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
        increaseScore();
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
            Debug.Log("cehcked");
            item.GetComponentInChildren<QuestionAnswerController>().checkAnswer();
            // yield return new WaitForSeconds(2.0f);
            yield return new WaitUntil(() => game.GetComponentInChildren<gameController>().finish() == true);
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
