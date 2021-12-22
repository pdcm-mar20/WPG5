using System.Text;
using MLAPI;
using MLAPI.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PasswordNetworkManager : NetworkedBehaviour
{
    [SerializeField] private InputField passwordinputField;
    [SerializeField] private GameObject passwordEntriPanel;
    [SerializeField] private GameObject leaveButton;

    private void Start()
    {
        NetworkingManager.Singleton.OnServerStarted += HandleServerStarted;
        NetworkingManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
        NetworkingManager.Singleton.OnClientDisconnectCallback += HandleClientDisconnected;
    }

    private void HandleServerStarted()
    {
        if (NetworkingManager.Singleton.IsHost)
        {
            HandleClientConnected(NetworkingManager.Singleton.LocalClientId);
        }
    }

    //untuk handle client ketika player disconnect dari multiplayer
    private void HandleClientDisconnected(ulong clientId)
    {
        if (clientId == NetworkingManager.Singleton.LocalClientId)
        {
            passwordEntriPanel.SetActive(true);
            leaveButton.SetActive(false);
        }
    }

    //untuk handle client ketika player connect ke multiplayer
    private void HandleClientConnected(ulong clientId)
    {
        if (clientId == NetworkingManager.Singleton.LocalClientId)
        {
            passwordEntriPanel.SetActive(false);
            leaveButton.SetActive(true);
        }
    }

    //if object network manager ke destroy
    private void OnDestroy()
    {
        if (NetworkingManager.Singleton == null)
        {
            return;
        }

        NetworkingManager.Singleton.OnServerStarted -= HandleServerStarted;
        NetworkingManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
        NetworkingManager.Singleton.OnClientDisconnectCallback -= HandleClientDisconnected;
    }

    //if player connect as host
    public void Host()
    {
        NetworkingManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkingManager.Singleton.StartHost(new Vector3(-4f,0,0), Quaternion.identity);
    }

    //if player success connect to multiplayer
    private void ApprovalCheck(byte[] connectionData, ulong clientId,
        NetworkingManager.ConnectionApprovedDelegate callback)
    {
        var password = Encoding.ASCII.GetString(connectionData);
        var approveConnection = password == passwordinputField.text;

        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;
      
        switch (NetworkingManager.Singleton.ConnectedClients.Count)
        {
            case 1:
                spawnPos = new Vector3(0, 0,0);
                spawnRot = Quaternion.identity;
                break;
            case 2:
                spawnPos = new Vector3(-2, 0,0);
                spawnRot = Quaternion.identity;
                break;
        }
     
        callback(true, null,approveConnection, spawnPos, spawnRot);
    }

    //if player connect as client
    public void Client()
    {
        NetworkingManager.Singleton.NetworkConfig.ConnectionData = Encoding.ASCII.GetBytes(passwordinputField.text);
        NetworkingManager.Singleton.StartClient();
    }

    //if player leave from lobby
    public void Leave()
    {
        if (NetworkingManager.Singleton.IsHost)
        {
            NetworkingManager.Singleton.StopHost();
            NetworkingManager.Singleton.ConnectionApprovalCallback -= ApprovalCheck;
        }else if (NetworkingManager.Singleton.IsClient)
        {
            NetworkingManager.Singleton.StopClient();
        }
        
        passwordEntriPanel.SetActive(true);
        leaveButton.SetActive(false);
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}