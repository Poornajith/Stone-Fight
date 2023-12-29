using UnityEngine;
using UnityEngine.UI;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class FusionConnection : MonoBehaviour, INetworkRunnerCallbacks
{
    public static FusionConnection instance;
    [HideInInspector] public NetworkRunner runner;

    [SerializeField] NetworkObject playerPrefab;

    public string _playerName = null;

    private List<SessionInfo> _sessions = new List<SessionInfo>();

    [Header("Session List")]
    public Button refreshButton;
    public Transform sessionListContent;
    public GameObject sessionEntryPrefab;
   // public GameObject roomListView;
    public GameObject gameOverView;
    public GameObject roomNameInputView;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;    
        }
    }

    public void ConnectToLobby(string playerName)
    {
        roomNameInputView.SetActive(true);

        _playerName = playerName;

        if (runner == null)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }
        runner.JoinSessionLobby(SessionLobby.Shared);
    }

    public async void ConnectToSession(string sessionName)
    {
        roomNameInputView.SetActive(false);

        if (runner == null)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }
        await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = sessionName,
           // SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    public async void CreateSession()
    {
       // roomListView.SetActive(false);

        int randomInt = UnityEngine.Random.Range(1000, 9999);
        string randomSessionName = "Room-" + randomInt.ToString();

        if (runner == null)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }
        await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = randomSessionName,
            PlayerCount = 4,
            // SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    // session names comes in room name logic
    public async void CreateSessionWithNewName(String sessionName)
    {
        roomNameInputView.SetActive(false);

        if (runner == null)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }
        await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = sessionName,
            PlayerCount = 4,
            // SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

    }

    // create new room with random name
    public async void CreateNewRoom()
    {
       // roomListView.SetActive(false);

        int randomInt = UnityEngine.Random.Range(1000, 9999);
        string randomSessionName = "Room-" + randomInt.ToString();

        if (runner == null)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }
        await runner.StartGame(new StartGameArgs
        {
            GameMode = GameMode.Shared,
            SessionName = randomSessionName,
            PlayerCount = 4,
            // SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("OnConnectedToServer");
        NetworkObject playerObject = runner.Spawn(playerPrefab, Vector3.zero);

        runner.SetPlayerObject(runner.LocalPlayer, playerObject);
    }

    public void OnPlayerDead()
    {
        // Destroy(playerPrefab);
        gameOverView.SetActive(true);       
    }

    public void RefreshSessionListUI()
    {
        // make sure when refreshing it not creating duplicates 
        foreach(Transform child  in sessionListContent)
        {
            Destroy(child.gameObject);
        }

        foreach (SessionInfo session in sessionListContent)
        {
            if (session.IsVisible)
            {
                GameObject entry = GameObject.Instantiate(sessionEntryPrefab, sessionListContent);
                SessionEntryPrefab script = entry.GetComponent<SessionEntryPrefab>();
                script.sessionName.text = session.Name;
                script.playerCount.text = session.PlayerCount + "/" + session.MaxPlayers;

                if(session.IsOpen == false || session.PlayerCount >= session.MaxPlayers)
                {
                    script.joinButton.interactable = false;
                }
                else
                {
                    script.joinButton.interactable = true;
                }
            }
        }
    }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerJoined");
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerLeft");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        _sessions.Clear();
        _sessions = sessionList;
        Debug.Log("Session list Updated");
        Debug.Log(_sessions);
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        
    }
    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }
    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        
    }
    
}
