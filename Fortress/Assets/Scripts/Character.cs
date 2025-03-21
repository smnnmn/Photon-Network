using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Move))]
[RequireComponent(typeof(Rotation))]
public class Character : MonoBehaviourPun
{
    [SerializeField] Move move;
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] Rotation rotation;
    [SerializeField] GameObject remoteCamera;
    // Start is called before the first frame update

    private void Awake()
    {
        move = GetComponent<Move>();
        rotation = GetComponent<Rotation>();
        rigidBody = GetComponent<Rigidbody>();

    }
    void Start()
    {
        DisableCamera();
    }
    private void Update()
    {
        if (photonView.IsMine == false) return;

        move.OnKeyUpdate();
        rotation.OnMouseUpdate();
        
    }
    private void FixedUpdate()
    {
        if (photonView.IsMine == false) return;

        move.OnMove(rigidBody);
        rotation.RotateY(rigidBody);
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
