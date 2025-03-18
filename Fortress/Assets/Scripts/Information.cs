using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Information : MonoBehaviour
{
    private string roomName;

    [SerializeField] Text TitleText;

    public void ConnectRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void View(string name, int currentStaff, int maxStaff)
    {
        roomName = name;
        TitleText.text = roomName + " ( " + currentStaff + " / " + maxStaff + " ) ";
    }

}
