using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PlayFab.ClientModels;
using UnityEngine.UI;
using PlayFab;

public class PlayfabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField emailInputField;
    [SerializeField] InputField passwordInputField;
    [SerializeField] GameObject failurePanel;

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInputField.text,
            Password = passwordInputField.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress
        (
            request,
            Succeed,
            Fail
        );

    }

    public void Succeed(LoginResult loginResult)
    {
        // 마스터 클라이언트가 씬을 이동할때 다른 remote클라이언트들이
        // 같이 이동될지 안될지
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = "1.0f";

        PhotonNetwork.LoadLevel("Lobby");
    }

    public void Fail(PlayFabError playFabError)
    {
        failurePanel.SetActive(true);
    }
    public void FaillClose()
    {
        failurePanel.SetActive(false);
    }

}
