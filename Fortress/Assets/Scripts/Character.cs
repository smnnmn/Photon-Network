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

    [SerializeField] Pause pausePanel;
    // Start is called before the first frame update

    private void Awake()
    {
        move = GetComponent<Move>();
        rotation = GetComponent<Rotation>();
        rigidBody = GetComponent<Rigidbody>();

        pausePanel = FindObjectOfType<Pause>(true);
    }
    void Start()
    {
        DisableCamera();
    }
    private void Update()
    {
        if (photonView.IsMine == false) return;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MouseManager.Instance.SetMouse(true);

            pausePanel.gameObject.SetActive(true);
        }

        move.OnKeyUpdate();
        rotation.OnMouseX();
        
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
    private void OnTriggerEnter(Collider other)
    {
        PhotonView clone = other.GetComponent<PhotonView>();
 
        if(clone.IsMine)
        {
            PhotonNetwork.Destroy(clone.gameObject);
        }
    }
}
