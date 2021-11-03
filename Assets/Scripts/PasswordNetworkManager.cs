using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MLAPI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PasswordNetworkManager : MonoBehaviour
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

    private void HandleClientDisconnected(ulong clientId)
    {
        if (clientId == NetworkingManager.Singleton.LocalClientId)
        {
            passwordEntriPanel.SetActive(true);
            leaveButton.SetActive(false);
        }
    }

    private void HandleClientConnected(ulong clientId)
    {
        if (clientId == NetworkingManager.Singleton.LocalClientId)
        {
            passwordEntriPanel.SetActive(false);
            leaveButton.SetActive(true);
        }
    }

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

    public void Host()
    {
        NetworkingManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkingManager.Singleton.StartHost(new Vector3(-4f,0,0), Quaternion.identity);
    }

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
        
        callback(true, null, approveConnection, spawnPos, spawnRot);
    }

    public void Client()
    {
        NetworkingManager.Singleton.NetworkConfig.ConnectionData = Encoding.ASCII.GetBytes(passwordinputField.text);
        NetworkingManager.Singleton.StartClient();
    }

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
}