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

        // ������ �Ϸ�Ǿ����� �κ� �����մϴ�.
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
            Debug.Log("Ŭ���̾�Ʈ�� ����Ǿ����ϴ�.");
        }
        else
        {
            Debug.Log("Ŭ���̾�Ʈ�� ���� ������ ������ ������� �ʾҽ��ϴ�.");
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
            // ���� ������ ���
            if(room.RemovedFromList == true)
            {
                dictionary.TryGetValue(room.Name, out prefab);

                Destroy(prefab);

                dictionary.Remove(room.Name);
            }
            else // ���� ������ ����Ǵ� ���
            {
                // ���� ó�� �����Ǵ� ���
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
                else // ���� ������ ����Ǵ� ���
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
