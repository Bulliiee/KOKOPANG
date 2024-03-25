using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserListElement : MonoBehaviour
{
    public GameObject UserInfoDetail;

    public LobbyManager lobbyManagerScript;

    public TMP_Text userNameText;
    public Button detailBtn;

    private int id;
    private string email;
    private new string name;


    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    public string Email
    {
        get
        {
            return email;
        }
        set
        {
            email = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }


    private void Start()
    {
        // �̺�Ʈ ���̱�
        detailBtn.onClick.AddListener(clickOpenUserDetailBtn);

        lobbyManagerScript = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
    }

    // ���� �� ������ ���� ��ư Ŭ�� ��
    public void clickOpenUserDetailBtn()
    {
        UserInfoDetail script = UserInfoDetail.GetComponent<UserInfoDetail>();
        UserInfoDetail.SetActive(true);

        script.Email = Email;
        script.Name = Name;
        script.Id = Id;

        script.detailUserNameText.text = Name;
        script.detailEmailText.text = Email;

        // ģ������ Ȯ�� �� ģ���߰� ��ư �ٲٱ�
        StartCoroutine(setFriendStatus());
    }

    // ģ�� ���º� ��ư �ٲٱ�
    private IEnumerator setFriendStatus()
    {
        string result = "";
        yield return StartCoroutine(lobbyManagerScript.FriendCheckRequest(Id, value => result = value));

        UserInfoDetail userInfoDetailScript = UserInfoDetail.GetComponent<UserInfoDetail>();

        // ���� ģ��
        if (result == "friend")
        {
            Debug.Log("friend");
            userInfoDetailScript.addFriendBtn.interactable = false;
            userInfoDetailScript.addFriendText.text = "ģ��";
        }
        // ���� ��û ������ ���� �ޱ� �����
        else if(result == "waiting")
        {
            Debug.Log("waiting");
            userInfoDetailScript.addFriendBtn.interactable = false;
            userInfoDetailScript.addFriendText.text = "�������";
        }
        // ģ�� ��û�� ���ͼ� ���� ���� �� �ִ� ����
        else if(result == "accept")
        {
            Debug.Log("accept");
            userInfoDetailScript.addFriendBtn.interactable = true;
            userInfoDetailScript.addFriendText.text = "����";
        }
        // �ƹ� ���µ� �ƴ�
        else if(result == "notFriend")
        {
            Debug.Log("notFriend����");
            userInfoDetailScript.addFriendBtn.interactable = true;
            userInfoDetailScript.addFriendText.text = "ģ���߰�";
            userInfoDetailScript.addFriendBtn.onClick.AddListener(() => StartCoroutine(clickAddFriendBtn()));
        }
    }

    // ģ�� �߰� ȣ��
    private IEnumerator clickAddFriendBtn()
    {
        yield return StartCoroutine(lobbyManagerScript.addFriend(Id));

        // ģ������ Ȯ�� �� ģ���߰� ��ư �ٲٱ�
        StartCoroutine(setFriendStatus());
    }

    // TODO: ģ�� ���� ȣ��
    private IEnumerator clickAcceptFriend()
    {
        yield break;
    }
}
