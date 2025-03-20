using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Move))]

public class Character : MonoBehaviourPun
{
    [SerializeField] Move move;
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] GameObject remoteCamera;
    // Start is called before the first frame update

    private void Awake()
    {
        move = GetComponent<Move>();
        rigidBody = GetComponent<Rigidbody>();

    }
    void Start()
    {
        DisableCamera();
    }
    private void Update()
    {
        move.OnKeyUpdate();
    }
    private void FixedUpdate()
    {
        move.OnMove(rigidBody);
    }
    public void DisableCamera()
    {
        // 현재 플레이어가 나 자신이라면?
        if(photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            remoteCamera.SetActive(false);
        }
    }
}
