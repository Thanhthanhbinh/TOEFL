using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControllerScript
{
    Mouse mouse;
    // A Test behaves as an ordinary method
    [Test]
    public void ButtonSceneSwitch()
    {
        // Use the Assert class to test conditions
        
        List<string> sceneList = new List<string>{"Menu","TeacherDashboard","Final","ExamData","ExamCreation","ResultScene","ExamScene"};
        foreach (var item in sceneList)
        {
            Setup();
            string path = "Canvas/ScenceManager/"+item;
            Debug.Log(path);
            Button findButton = GameObject.Find(path).GetComponent<Button>();
            Debug.Log(findButton);
            mouse = InputSystem.AddDevice<Mouse>();
            string sceneName = SceneManager.GetActiveScene().name;
            findButton.onClick.Invoke();

            sceneName = SceneManager.GetActiveScene().name;
            Assert.That(sceneName, Is.EqualTo(item));
        }
        
    }
    [Test]
    public void messageButton()
    {
        // Use the Assert class to test conditions
        Setup();
        string path = "Canvas/Message";
        Debug.Log(path);
        Button findButton = GameObject.Find(path).GetComponent<Button>();
        Debug.Log(findButton);
        mouse = InputSystem.AddDevice<Mouse>();
        
        findButton.onClick.Invoke();
        GameObject messageObject = GameObject.Find("Canvas/message(Clone)");
        Debug.Log(messageObject);
        Assert.That(messageObject != null, Is.EqualTo(true));
        
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
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/test.unity") ;
        
    }

    

    
    

}
