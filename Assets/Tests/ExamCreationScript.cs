using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// using QuestionAnswer.QuestionAnswer;
public class ExamCreationScript
{
    Mouse mouse;
    // A Test behaves as an ordinary method
    [Test]
    public void questionAnswer()
    {
        // List<string> answerList = new List<string>{"answer a","answer b","answer c","answer d"};
        // string question = "Question test";
        // string mode = "easy";
        // int index = 1;
        // QuestionAnswer testObj = new QuestionAnswer(question,answerList,"answer a",mode,index);
        // testObj.setChosen("answer a");
        // Assert.That(testObj.isCorrect(), Is.EqualTo(true));
    }
    
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MenuTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame
        
        yield return null;
    }

    public void Setup()
    {
        // base.Setup();
        // SceneManager.LoadScene("Menu");
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/ExamCreation.unity") ;
        
    }

    

    
    

}
