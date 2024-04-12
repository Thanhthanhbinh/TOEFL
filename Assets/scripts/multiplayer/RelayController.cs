using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;

using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using ParrelSync;
#endif

public class RelayController : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject screenController;
    // private Lobby connectedLobby;
    // private QueryResponse lobbies;
    private UnityTransport transport;
    private string playerId;
    private Transform canvas;


    private void Awake() {
        Debug.Log("awake");
        transport = FindObjectOfType<UnityTransport>();
        canvas = GameObject.Find("Canvas").transform;
    }

    private async Task Authenticate() {
        var options = new InitializationOptions();

        #if UNITY_EDITOR
        options.SetProfile(ClonesManager.IsClone() ? ClonesManager.GetArgument() : "Primary");
        Debug.Log(ClonesManager.IsClone());
        #endif

        await UnityServices.InitializeAsync(options);
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        playerId = AuthenticationService.Instance.PlayerId;
        Debug.Log(playerId);

    }

    private void LogOut() {
        AuthenticationService.Instance.SignOut(true);
        playerId = "";
    }

    public async void CreateGame(int maxPlayer) {
        try
        {
            await Authenticate();
            button.SetActive(false);
            UserData.Instance.playerType = "host";
            //get an allocation on the relay service for the game
            Allocation allocatedRelay = await RelayService.Instance.CreateAllocationAsync(maxPlayer);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocatedRelay.AllocationId);
            Debug.Log(joinCode);
            //send relay information to the transport

            transport.SetHostRelayData(allocatedRelay.RelayServer.IpV4, 
                                        (ushort)allocatedRelay.RelayServer.Port,
                                        allocatedRelay.AllocationIdBytes, allocatedRelay.Key,
                                        allocatedRelay.ConnectionData);
            // NetworkManager.Singleton.StartServer();
            NetworkManager.Singleton.StartHost();
            UserData.Instance.joinCode =  joinCode;
            screenController.GetComponent<teacherDashboardController>().setUpExamPanel();
        }
        catch (System.Exception)
        {
            Debug.Log("An Error has Occure!");
            MessageManager.createMessage("An Error has occured",canvas);
            button.SetActive(true);
        }
        
    }

    public void StartGame(){
        NetworkManager.Singleton.SceneManager.LoadScene("ExamScene",LoadSceneMode.Single);
    }

    public void EndGame() {
        LogOut();
        UserData.Instance.playerType = "";
        UserData.Instance.joinCode = "";
        NetworkManager.Singleton.Shutdown();
    }

    public async void JoinGame(string joinCode) {
        try
        {
            await Authenticate();
            UserData.Instance.playerType = "client";
            UserData.Instance.joinCode = joinCode;
            button.SetActive(false);
            JoinAllocation allocatedRelay = await RelayService.Instance.JoinAllocationAsync(joinCode);
            //send relay information to the transport
            transport.SetClientRelayData(allocatedRelay.RelayServer.IpV4, 
                                        (ushort)allocatedRelay.RelayServer.Port, 
                                        allocatedRelay.AllocationIdBytes, allocatedRelay.Key, 
                                        allocatedRelay.ConnectionData, allocatedRelay.HostConnectionData);
            NetworkManager.Singleton.StartClient();
        }
        catch (System.Exception)
        {
            Debug.Log("An Error has Occure!");
            MessageManager.createMessage("An Error has occured",canvas);
            button.SetActive(true);
        }
    }

    void Cleanup()
    {
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
    }
    
}
