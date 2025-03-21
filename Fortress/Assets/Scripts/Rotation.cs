using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float mouseX;
    [SerializeField] Vector3 rotation;

    public void OnMouseUpdate()
    {
        // mouseX�� ���콺�� �Է��� ���� �����մϴ�.
        mouseX += Input.GetAxisRaw("Mouse X") * speed * Time.deltaTime;
    }

    public void RotateY(Rigidbody rigidBody)
    {
        rigidBody.transform.eulerAngles = new Vector3(0f, mouseX, 0f);
    }
}
