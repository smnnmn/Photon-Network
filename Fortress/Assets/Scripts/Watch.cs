using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Watch : MonoBehaviourPun
{
    [SerializeField] Text timeText;
    // Update is called once per frame
    void Update()
    {
        Debug.Log(PhotonNetwork.Time);
        // timeText.text = PhotonNetwork
    }
}
