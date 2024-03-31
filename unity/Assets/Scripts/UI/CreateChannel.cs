using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class CreateChannel : MonoBehaviour
{
    [Header("Commons")]
    public GameObject LobbyScene;
    public GameObject ChannelScene;
    public LoginManager loginManagerScript;
    public TCPConnectManager TCPConnectManagerScript;

    [Header("Create Channel")]
    public TMP_InputField InputRoomName;    // �� �̸� �Է�ĭ
    public TMP_InputField InputRoomPassword;    // �� ��й�ȣ �Է�ĭ
    public Toggle isPrivateRoom;    // ��й� ������ ��۹�ư



    // ��й� üũ Ȯ��
    public void ToggleClick()
    {
        InputRoomPassword.text = "";
        InputRoomPassword.interactable = isPrivateRoom.isOn;
    }

    // �� ����� ��ư Ŭ�� ��
    public void CreateChannelBtnClicked()
    {
        string json = "{" +
            "\"channel\":\"channel\"," +
            $"\"userName\":\"{loginManagerScript.loginUserInfo.Name}\"," +
            "\"data\":{" +
                "\"type\":\"create\"," +
                $"\"channelName\":\"{InputRoomName.text}\"" +
            "}" +
        "}";
        //Debug.Log(json);

        TCPConnectManagerScript.createChannel(json);
    }

    // �� ����� �ݱ� ��ư Ŭ�� ��
    public void CloseBtnClicked()
    {
        InputRoomName.text = "";
        InputRoomPassword.text = "";
        gameObject.SetActive(false);
    }
}
