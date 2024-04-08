using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class finalController : MonoBehaviour
{
    [SerializeField] private GameObject pictureContainer;
    [SerializeField] private GameObject badgeContainer;
    [SerializeField] private GameObject questionContainer;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject grade;
    [SerializeField] private GameObject name;
    [SerializeField] private GameObject footnote;
    [SerializeField] private GameObject certificate;

    void Start(){
        setUpBadge();
        setUpPicture();
        setUpScoreAndMark();
        setUpQuestion();
        setUpCert();
    }
    public void showCert(){
        certificate.SetActive(true);
    }
    public void hideCert(){
        certificate.SetActive(false);
    }
    private void setUpCert(){
        string date = DateTime.Now.ToString("dd/MM/yyyy");
        name.GetComponent<Text>().text = UserData.Instance.name;
        footnote.GetComponent<Text>().text = "Issued on: " + date + "\n" + "Exam Code: " + UserData.Instance.joinCode;
    }
    private void setUpScoreAndMark(){
        score.GetComponentInChildren<TMP_Text>().SetText(ExamInfo.Instance.score + "/" + ExamInfo.Instance.total);
        grade.GetComponentInChildren<TMP_Text>().SetText(ExamInfo.Instance.grade + "/" + ExamInfo.Instance.total);

    }

    private void setUpQuestion(){
        //create prefab of a panel of question
        GameObject questionPanel = Resources.Load<GameObject>("questionPanel");
        //clear the existing questionPanel being shown
        foreach(Transform child in questionContainer.transform)
        {
            Destroy(child.gameObject);
        }
        //initialise the questionPanel and add their variables
        foreach (var item in ExamInfo.Instance.questionList)
        {   
            GameObject temp = Instantiate(questionPanel,questionContainer.transform);
            //assign them their own object so they can change UI
            temp.GetComponentInChildren<QuestionAnswerController>().questionPanel = temp;
            // assign the corresponding questionAnswer object
            temp.GetComponentInChildren<QuestionAnswerController>().content = item;
            temp.GetComponentInChildren<QuestionAnswerController>().quizController = null;
            temp.GetComponentInChildren<QuestionAnswerController>().allInfoUI();
            temp.GetComponentInChildren<QuestionAnswerController>().showResult();
        }
    }

    private void hasAllBadge(){
        bool returnVal = true;
        foreach (var badge in ExamInfo.Instance.badgeList.Keys)
        {
            if (ExamInfo.Instance.badgeList[badge] == 0 && badge !="allBadge"){
                returnVal = false;
            }
        }
        if (returnVal){
            ExamInfo.Instance.badgeList["allBadge"] = 1;
        }
    }
    private void setUpBadge(){
        hasAllBadge();
        foreach(Transform child in badgeContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var badge in ExamInfo.Instance.badgeList.Keys)
        {
            for (int i = 0; i < ExamInfo.Instance.badgeList[badge]; i++)
            {
                Debug.Log(badge);
                GameObject badgeItem = Resources.Load<GameObject>("Badges/" + badge);
                GameObject temp = Instantiate(badgeItem,badgeContainer.transform);
            }
            
        }
    }
    
    private void setUpPicture(){
        foreach(Transform child in pictureContainer.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (var picName in ExamInfo.Instance.pictureList)
        {
            GameObject picture = Resources.Load<GameObject>("resultImage");
            
            GameObject temp = Instantiate(picture,pictureContainer.transform);

            Sprite pic = Resources.Load<Sprite>(picName);
            temp.GetComponent<Image>().sprite = pic;
        }
    }
    public void exit(){
        ExamData.Instance.examQuestion = "";
        ExamData.Instance.questionList = new List<QuestionAnswer>();
        SceneController.changeToMenu();
        
    }
}



    
