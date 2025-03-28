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
        // JoinLobby : 특정 로비를 생성하여 진입하는 함수
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
        // 연결이 될때까지 대기합니다. 
        while(!PhotonNetwork.IsConnectedAndReady)
        {
            yield return null;
        }

        // 연결이 준비되었으면 씬 로드
        PhotonNetwork.LoadLevel("Room");
    }
}
