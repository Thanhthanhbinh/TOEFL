using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class prepareExam : NetworkBehaviour

{
    public NetworkVariable<FixedString4096Bytes> questionList; //this is network object
    public NetworkVariable<int> maxPlayersVal = new NetworkVariable<int>(0);

    public NetworkList<FixedString4096Bytes> resultList;

    void Awake()
    {
        resultList= new NetworkList<FixedString4096Bytes>();
    }
    public override void OnNetworkSpawn(){
        Debug.Log("a player has join");
    }
    
    void Update(){
        if (IsServer){
            questionList.Value = ExamData.Instance.examQuestion;
            maxPlayersVal.Value = ExamData.Instance.maxPlayers;
        } else {
            ExamData.Instance.examQuestion = questionList.Value;
            ExamData.Instance.maxPlayers = maxPlayersVal.Value ;
            resultList.Add(ExamData.Instance.resultData);
        }

    }
    

    
}