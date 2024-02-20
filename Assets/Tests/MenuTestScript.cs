using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuTestScript
{
    Mouse mouse;
    // A Test behaves as an ordinary method
    [Test]
    public void MenuTestTeacherButton()
    {
        // Use the Assert class to test conditions
        Setup();
        Button TeacherButton = GameObject.Find("Canvas/TeacherButton").GetComponent<Button>();
        mouse = InputSystem.AddDevice<Mouse>();
        string sceneName = SceneManager.GetActiveScene().name;
        Assert.That(sceneName, Is.EqualTo("Menu"));
        TeacherButton.onClick.Invoke();
        

        sceneName = SceneManager.GetActiveScene().name;
        Assert.That(sceneName, Is.EqualTo("TeacherDashboard"));
    }
    [Test]
    public void MenuTestStudentButton()
    {
        // Use the Assert class to test conditions
        Setup();
        Button StudentButton = GameObject.Find("Canvas/StudentButton").GetComponent<Button>();
        mouse = InputSystem.AddDevice<Mouse>();
        string sceneName = SceneManager.GetActiveScene().name;
        Assert.That(sceneName, Is.EqualTo("Menu"));
        StudentButton.onClick.Invoke();
        

        sceneName = SceneManager.GetActiveScene().name;
        Assert.That(sceneName, Is.EqualTo("Lobby"));
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
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity") ;
        
    }

    

    
    

}
