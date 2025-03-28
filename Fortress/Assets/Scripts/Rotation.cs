using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float mouseX;
    [SerializeField] float mouseY;
    [SerializeField] Vector3 rotation;

    public void OnMouseX()
    {
        // mouseX�� ���콺�� �Է��� ���� �����մϴ�.
        mouseX += Input.GetAxisRaw("Mouse X") * speed * Time.deltaTime;

    }
    public void OnMouseY()
    {
        mouseY += Input.GetAxisRaw("Mouse Y") * speed * Time.deltaTime;

    }

    public void RotateY(Rigidbody rigidBody)
    {
        rigidBody.transform.eulerAngles = new Vector3(0f, mouseX, 0f);
    }
    public void RotateX(GameObject clone)
    {
        // MouseY�� ���� -65 ~ 65 ������ ������ �����մϴ�
        mouseY = Mathf.Clamp(mouseY, -65, 65);

        clone.transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
        
    }
}
