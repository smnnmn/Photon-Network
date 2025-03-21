using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CreateManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform[] transforms;
    [SerializeField] static int count = 0;
    private void Awake()
    {
        Create();
    }

    public void Create()
    {
        PhotonNetwork.Instantiate
            (
                "Character",
               // Vector3.zero,
               transforms[count++].position,
                Quaternion.identity
            );
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        count--;
    }

}
