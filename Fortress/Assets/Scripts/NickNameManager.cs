using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NickNameManager : MonoBehaviour
{
    private void Awake()
    {
        SetUserName("�� �ൿ �� ����");
    }

    // ���÷��� ���� ������Ʈ �Լ�
    private void SetUserName(string newDisplayName)
    {
        // ���÷��� ������ �����ϴ� ��û ��ü ����
        UpdateUserTitleDisplayNameRequest request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = newDisplayName // ���� ������ ���÷��� ����
        };

        // PlayFab API�� ���� ���÷��� ���� ���� ��û
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, Success, Failure);
    }

    // ���÷��� ���� ������Ʈ ���� �� ȣ��Ǵ� �ݹ�
    private void Success(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Display Name updated successfully to: " + result.DisplayName);
    }

    // ���÷��� ���� ������Ʈ ���� �� ȣ��Ǵ� �ݹ�
    private void Failure(PlayFabError error)
    {
        Debug.LogError("Error updating Display Name: " + error.GenerateErrorReport());
    }
}