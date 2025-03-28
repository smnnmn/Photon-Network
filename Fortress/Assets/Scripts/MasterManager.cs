using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviourPunCallbacks
{
    [SerializeField] WaitForSeconds waitForSeconds = new WaitForSeconds(5f);
    [SerializeField] Vector3[] energyPosition;
    [SerializeField] bool[] energyCheck;

    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(Create());
        }

    }
    IEnumerator Create()
    {
        while(true)
        {
            if (PhotonNetwork.CurrentRoom != null)
            {
                int index = Random.Range(0, energyPosition.Length);
                PhotonNetwork.InstantiateRoomObject("Energy",
                   energyPosition[index],
                    Quaternion.identity);
                energyCheck[index] = true;
            }
            yield return waitForSeconds;
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);

        StartCoroutine(Create());
    }
}
