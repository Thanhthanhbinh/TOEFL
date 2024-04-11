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
public class MenuScript
{
    Mouse mouse;
    // A Test behaves as an ordinary method
    
    [Test]
    public void preventEmptyJoinCode()
    {
        Setup();
        string path = "Canvas/joinGroup/JoinButton";
        Debug.Log(path);
        Button findButton = GameObject.Find(path).GetComponent<Button>();
        Debug.Log(findButton);
        mouse = InputSystem.AddDevice<Mouse>();
        
        findButton.onClick.Invoke();

        GameObject messageObject = GameObject.Find("Canvas/message(Clone)");
        Debug.Log(messageObject);
        Assert.That(messageObject != null, Is.EqualTo(true));
        
    }
    

    public void Setup()
    {
        // base.Setup();
        // SceneManager.LoadScene("Menu");
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/Menu.unity") ;
        
    }

    

    
    

}
