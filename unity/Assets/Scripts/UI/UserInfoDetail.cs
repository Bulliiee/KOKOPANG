using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInfoDetail : MonoBehaviour
{
    public TMP_Text detailUserNameText;
    public TMP_Text detailEmailText;
    public Button addFriendBtn;
    public TMP_Text addFriendText;
    public Button detailCloseBtn;

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
        detailCloseBtn.onClick.AddListener(clickCloseBtn);
    }

    // ���� �� ������ �ݱ� ��ư Ŭ�� ��
    public void clickCloseBtn()
    {
        gameObject.SetActive(false);
    }
}
