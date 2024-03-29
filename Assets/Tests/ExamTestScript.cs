using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExamTestScript
{
    Mouse mouse;
    // A Test behaves as an ordinary method
    [Test]
    public void MenuTestUseLivesButton()
    {
        // Use the Assert class to test conditions
        Setup();
        Button useLivesButton = GameObject.Find("Canvas/UseLivesButton").GetComponent<Button>();
        GameObject quizController = GameObject.Find("QuizManager");
        mouse = InputSystem.AddDevice<Mouse>();
        // quizController.GetComponent<quizManagerController>().moreLives();
        int liveCount = GameObject.Find("Canvas/Lives").transform.childCount;
        int initialVal = liveCount;
        useLivesButton.onClick.Invoke();
        liveCount = GameObject.Find("Canvas/Lives").transform.childCount;
        Assert.That(liveCount, Is.EqualTo(initialVal-1));
    }
    [Test]
    public void MenuTestStudentButton()
    {
        // Use the Assert class to test conditions
        // Setup();
        // Button StudentButton = GameObject.Find("Canvas/StudentButton").GetComponent<Button>();
        // mouse = InputSystem.AddDevice<Mouse>();
        // string sceneName = SceneManager.GetActiveScene().name;
        // Assert.That(sceneName, Is.EqualTo("Menu"));
        // StudentButton.onClick.Invoke();
        

        // sceneName = SceneManager.GetActiveScene().name;
        // Assert.That(sceneName, Is.EqualTo("Lobby"));
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
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/ExamScene.unity") ;
        
    }

    

    
    

}
