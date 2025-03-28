using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField titleInputField;
    [SerializeField] InputField capacityInputField;

    [SerializeField] Transform parentTransform;

    [SerializeField] Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.InLobby == false)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        // 연결이 완료되었으면 로비에 참가합니다.
        Debug.Log("Connected to Master Server!");
    }

    void Update()
    {
        if(PhotonNetwork.InLobby)
        {
            Debug.Log("Lobby Connect");
        }
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("클라이언트가 연결되었습니다.");
        }
        else
        {
            Debug.Log("클라이언트가 아직 마스터 서버에 연결되지 않았습니다.");
        }
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void OnCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = byte.Parse(capacityInputField.text);

        roomOptions.IsOpen = true;

        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(titleInputField.text, roomOptions);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject prefab;

        foreach(RoomInfo room in roomList)
        {
            // 룸이 삭제된 경우
            if(room.RemovedFromList == true)
            {
                dictionary.TryGetValue(room.Name, out prefab);

                Destroy(prefab);

                dictionary.Remove(room.Name);
            }
            else // 룸의 정보가 변경되는 경우
            {
                // 룸이 처음 생성되는 경우
                if (dictionary.ContainsKey(room.Name) == false)
                {
                    GameObject clone = Instantiate(
                        Resources.Load<GameObject>("Room"),
                        parentTransform);
                    
                    clone.GetComponent<Information>().View(
                        room.Name,
                        room.PlayerCount,
                        room.MaxPlayers);

                    dictionary.Add(room.Name, clone);
                }
                else // 룸의 정보가 변경되는 경우
                {
                    dictionary.TryGetValue(room.Name, out prefab);
                    prefab.GetComponent<Information>().View(
                        room.Name,
                        room.PlayerCount,
                        room.MaxPlayers);
                }
            }
        }
    }
}
