using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// using QuestionAnswer.QuestionAnswer;
public class QuestionAnswerScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void questionAnswerIsCorrect()
    {
        QuestionAnswer testObj = setUp();
        testObj.setChosen("answer a");
        Assert.That(testObj.isCorrect(), Is.EqualTo(true));
    }
    [Test]
    public void questionAnswerIsInCorrect()
    {
        QuestionAnswer testObj = setUp();
        testObj.setChosen("answer b");
        Assert.That(testObj.isCorrect(), Is.EqualTo(false));
    }
    [Test]
    public void questionAnswerSetQuestion()
    {
        QuestionAnswer testObj = setUp();
        testObj.setQuestion("new question");
        Assert.That(testObj.getQuestion(), Is.EqualTo("new question"));
    }

    [Test]
    public void questionAnswerUseHint()
    {
        QuestionAnswer testObj = setUp();
        testObj.toggleHint();
        Assert.That(testObj.isHintUsed(), Is.EqualTo(true));
    }
    
    public QuestionAnswer setUp(){
        List<string> answerList = new List<string>{"answer a","answer b","answer c","answer d"};
        string question = "Question test";
        string mode = "easy";
        int index = 1;
        QuestionAnswer testObj = new QuestionAnswer(question,answerList,"answer a",mode,index);
        return testObj;
    }
    

    
    

}
