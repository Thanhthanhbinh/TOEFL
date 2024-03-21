using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class resultController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scoreObject;
    public GameObject title;
    public Transform badgeContainer;
    public Transform scenarioContainer;

    private Dictionary<string,string> gameTypeList ;
    void Start()
    {   
        gameTypeList = new Dictionary<string,string>{{"JumpGame","JumpGame/JumpGame"},{"RunGame","RunGame/RunGame"},{"ShootGame","ShootGame/ShootGame"}};
        scoreObject.GetComponentInChildren<TMP_Text>().SetText(Result.Instance.score + "/" + Result.Instance.total);
        title.GetComponentInChildren<TMP_Text>().SetText(Result.Instance.state);
        setUpBadge();
        setUpScenario();

    }

    
    private void setUpBadge(){
        foreach(Transform child in badgeContainer.transform)
        {
            Destroy(child.gameObject);
        }
        Debug.Log(Result.Instance.hintBadge);
        Debug.Log(Result.Instance.liveBadge);
        Debug.Log(Result.Instance.overBadge);
        if (Result.Instance.hintBadge) {
            GameObject hintBadge = Resources.Load<GameObject>("Badges/noHintBadge");
            GameObject temp = Instantiate(hintBadge,badgeContainer);
            Debug.Log(temp);
        }
        if (Result.Instance.liveBadge) {
            GameObject hintBadge = Resources.Load<GameObject>("Badges/noLivesBadge");
            GameObject temp = Instantiate(hintBadge,badgeContainer);
            Debug.Log(temp);
        }
        if (Result.Instance.overBadge) {
            GameObject hintBadge = Resources.Load<GameObject>("Badges/aboveTotalBadge");
            GameObject temp = Instantiate(hintBadge,badgeContainer);
            Debug.Log(temp);
        }
        if (Result.Instance.correctBadge) {
            GameObject hintBadge = Resources.Load<GameObject>("Badges/allCorrectBadge");
            GameObject temp = Instantiate(hintBadge,badgeContainer);
            Debug.Log(temp);
        }
    }
    
    private void setUpScenario(){
        foreach(Transform child in scenarioContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var gameType in gameTypeList.Keys)
        {
            GameObject senarioButton = Resources.Load<GameObject>("senarioButton");
            GameObject temp = Instantiate(senarioButton,scenarioContainer);
            Button button = temp.GetComponent<Button>();
            TMP_Text textUI = temp.GetComponentInChildren<TMP_Text>();
            textUI.SetText(gameType);
            //add listener to update chosen answer
            button.onClick.AddListener(() => { 
                Result.Instance.gameType = gameTypeList[gameType];
            });
        }
    }
    
}
