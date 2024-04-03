using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;

public class hostPanelController : MonoBehaviour
{
    [SerializeField] private GameObject submitNumberText;
    [SerializeField] private GameObject relayObject;
    private GameObject player;
    private int submitNumber;

    public void setUpHostPanel(){
        player = GameObject.Find("Player(Clone)");
        submitNumber = 0;
        submitNumberText.GetComponent<Text>().text = submitNumber + " submited";
    }

    public void changeToExamData(){
        ExamData.Instance.resultList = new List<FixedString4096Bytes>(player.GetComponent<Player>().getStudentData());
        relayObject.GetComponent<RelayController>().EndGame();
        SceneController.changeToExamData();
    }
    // Update is called once per frame
    void Update()
    {
        submitNumber = player.GetComponent<Player>().getSubmitNumber();
        submitNumberText.GetComponent<Text>().text = submitNumber + " submited";
    }
}
