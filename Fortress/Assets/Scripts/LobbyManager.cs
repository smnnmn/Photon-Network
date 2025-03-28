using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Dropdown dropDown;

    public void Connect()
    {
        if(PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("Already connected to Photon. Current state: " + PhotonNetwork.NetworkClientState);
        }
    }

    public override void OnConnectedToMaster()
    {
        // JoinLobby : Ư�� �κ� �����Ͽ� �����ϴ� �Լ�
        PhotonNetwork.JoinLobby
            (
                new TypedLobby
                (
                    dropDown.options[dropDown.value].text,
                    LobbyType.Default
                )
            );
    
    }
    
    public override void OnJoinedLobby()
    {
        StartCoroutine(LoadRoom());
    }
    private IEnumerator LoadRoom()
    {
        // ������ �ɶ����� ����մϴ�. 
        while(!PhotonNetwork.IsConnectedAndReady)
        {
            yield return null;
        }

        // ������ �غ�Ǿ����� �� �ε�
        PhotonNetwork.LoadLevel("Room");
    }
}
