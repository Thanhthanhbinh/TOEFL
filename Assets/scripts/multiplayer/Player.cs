using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class Player : NetworkBehaviour

{
    [SerializeField] private GameObject player;
    public NetworkVariable<FixedString4096Bytes> questionList; //this is network object
    public NetworkVariable<int> submitNumber = new NetworkVariable<int>(0);
    public NetworkList<FixedString4096Bytes> resultList;
    public NetworkVariable<FixedString4096Bytes> result;
    public 

    void Awake()
    {
        resultList= new NetworkList<FixedString4096Bytes>();
    }
    public override void OnNetworkSpawn(){
        Debug.Log("a player has join");
        submitNumber.Value = 0;
        if (IsServer){
            questionList.Value = ExamData.Instance.examQuestion;
        } else {
            ExamData.Instance.examQuestion = questionList.Value;
        }
    }
    
    public bool updateResultList(FixedString4096Bytes input) {
        if (!IsServer){
            if (input != ""){
                updateResultListServerRpc(input);
                return true;
            }
            return false;
        }
        return false;
    }

    public List<FixedString4096Bytes> getStudentData() {
        List<FixedString4096Bytes> temp = new List<FixedString4096Bytes>();
        foreach (var item in resultList)
        {
            temp.Add(item);
        }
        return temp;
    }
    

    public int getSubmitNumber(){
        return resultList.Count;
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void updateResultListServerRpc(FixedString4096Bytes input)
    {   
        
        
            resultList.Add(input);
            Debug.Log(resultList[0]);
            submitNumber.Value += 1;
        
    }

    
}