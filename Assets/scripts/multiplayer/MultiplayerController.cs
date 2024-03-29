using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using System;

public class MultiplayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public async Task<Lobby> startLobby(string lobbyName, int maxPlayers, List<QuestionAnswer> examQuestion){
        try {

            CreateLobbyOptions options = new CreateLobbyOptions();
            options.IsPrivate = false;

            var lobby = await Lobbies.Instance.CreateLobbyAsync(lobbyName, maxPlayers, options);

            // Send a heartbeat every 15 seconds to keep the room alive
            StartCoroutine(HeartbeatLobbyCoroutine(lobby.Id, 15));
            Debug.Log(lobby.Id);
            
            return lobby;
        }
        catch (Exception e) {
            Debug.LogFormat("Failed creating a lobby");
            Debug.Log(e);
            return null;
        }
        
    }
    private static IEnumerator HeartbeatLobbyCoroutine(string lobbyId, float waitTimeSeconds) {
        var delay = new WaitForSecondsRealtime(waitTimeSeconds);
        while (true) {
            Lobbies.Instance.SendHeartbeatPingAsync(lobbyId);
            yield return delay;
        }
    }
}
