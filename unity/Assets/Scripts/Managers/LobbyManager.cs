using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    [Header("Commons")]
    public LoginManager loginManagerScript; // �α��� �Ŵ��� ��ũ��Ʈ
    public TCPConnectManager TCPConnectManagerScript;   // TCP ��� ���� ��Ʈ��Ʈ

    [Header("Lobby")]
    public TMP_Text LobbyMyName;    // ������ �̸�
    public GameObject UserListElement;  // ���� ����Ʈ�� �� ���� ������Ʈ
    public Button LobbyAllListBtn;  // ��� ���� ����Ʈ ���� ��ư
    public Button LobbyFriendListBtn;   // ģ�� ����Ʈ ���� ��ư
    public GameObject ScrollViewLobbyList;  // ��� ���� ����Ʈ ���� ��ũ�Ѻ�
    public GameObject ScrollViewFriendList; // ģ�� ����Ʈ ���� ��ũ�Ѻ�
    public GameObject UserInfoDetail;   // ���� ����Ʈ���� ������ �����ϴ� ���� ������ â


    private string url = "http://j10c211.p.ssafy.io:8080";   // ��û URL


    /* ======================== �κ� ======================== */
    // �κ� ���� �� ������ �ʱ�ȭ(����� ���� ��)
    public void LobbyInit(string name, int id)
    {
        LobbyMyName.text = name + " #" + id;
        setAllUsers();
        setFriendUsers();
    }

    // ��ü ���� �ҷ�����
    private void setAllUsers()
    {
        // ���� ������ ���� ������ �ҷ�����
        // TODO: ���̵����� �������� �޾ƿ°ɷ� ����
        User[] userList = new User[3];
        userList[0] = new User
        {
            Id = 30,
            Email = "ww",
            Name = "������"
        };
        userList[1] = new User
        {
            Id = 31,
            Email = "ee",
            Name = "����"
        };
        userList[2] = new User
        {
            Id = 29,
            Email = "qq",
            Name = "ťť"
        };

        // ���� ����Ʈ �� ��ũ�Ѻ�(������ �θ�)
        Transform content = ScrollViewLobbyList.transform.Find("ScrollView/Viewport/Content");

        // ���� ����Ʈ�� �� ������ ������Ʈ ����
        GameObject[] userListElements = new GameObject[userList.Length];

        // ���� ����Ʈ ������ ������Ʈ�� �� ������
        //GameObject[] activeUserData = new GameObject[userList.Length];
        for (int i = 0; i < userList.Length; i++)
        {
            // ��� ���� �� �θ� ����
            userListElements[i] = Instantiate(UserListElement);
            userListElements[i].transform.SetParent(content, false);
            // ���� ������ �ʱ�ȭ
            UserListElement userListElementScript = userListElements[i].GetComponent<UserListElement>();
            userListElementScript.Id = userList[i].Id;
            userListElementScript.Email = userList[i].Email;
            userListElementScript.Name = userList[i].Name;
            userListElementScript.UserInfoDetail = UserInfoDetail;
            // ������ ���̱�
            userListElementScript.UserNameText.text = userList[i].Name;
        }
    }

    // ģ�� ����Ʈ �ҷ�����
    private void setFriendUsers()
    {
        // ���� ģ�� ���� ������ �ҷ�����
        StartCoroutine(getFriendList((User[] userList) =>
        {
            // ģ���� ������
            if (userList != null)
            {
                // ģ�� ����Ʈ �� ��ũ�Ѻ�(������ �θ�)
                Transform content = ScrollViewFriendList.transform.Find("ScrollView/Viewport/Content");

                // ģ�� ����Ʈ�� �� ������ ������Ʈ ����
                GameObject[] userListElements = new GameObject[userList.Length];

                // ���� ����Ʈ ������ ������Ʈ�� �� ������
                //GameObject[] activeUserData = new GameObject[userList.Length];
                for (int i = 0; i < userList.Length; i++)
                {
                    // ��� ���� �� �θ� ����
                    userListElements[i] = Instantiate(UserListElement);
                    userListElements[i].transform.SetParent(content, false);
                    // ���� ������ �ʱ�ȭ
                    UserListElement userListElementScript = userListElements[i].GetComponent<UserListElement>();
                    userListElementScript.Id = userList[i].Id;
                    userListElementScript.Email = userList[i].Email;
                    userListElementScript.Name = userList[i].Name;
                    userListElementScript.UserInfoDetail = UserInfoDetail;
                    // ������ ���̱�
                    userListElementScript.UserNameText.text = userList[i].Name;
                }
            }
        }));
        
    }

    // ��ü ���� ��� / ģ�� ��� ��ư Ŭ�� ��
    public void clickUserListBtn()
    {
        LobbyAllListBtn.interactable = !LobbyAllListBtn.IsInteractable();
        LobbyFriendListBtn.interactable = !LobbyFriendListBtn.IsInteractable();

        ScrollViewLobbyList.SetActive(!ScrollViewLobbyList.activeSelf);
        ScrollViewFriendList.SetActive(!ScrollViewFriendList.activeSelf);
    }

    [System.Serializable]
    class Friend
    {
        public int friendId;
        public string friendName;
    }
    [System.Serializable]
    class FriendList
    {
        public Friend[] friends;
    }
    // ģ�� ��� �ҷ�����
    private IEnumerator getFriendList(System.Action<User[]> callback)
    {
        int userId = loginManagerScript.loginUserInfo.Id;

        string requestUrl = url + "/friend/list?userId=" + userId ;

        using (UnityWebRequest friendCheckRequest = UnityWebRequest.Get(requestUrl))
        {
            friendCheckRequest.SetRequestHeader("Authorization", loginManagerScript.accessToken);
            yield return friendCheckRequest.SendWebRequest();

            // ��û ���� ��
            if (friendCheckRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(friendCheckRequest.error);
                yield break;
            }
            // ��û ���� ��
            else
            {
                string result = friendCheckRequest.downloadHandler.text;
                Debug.Log(result);
                string wrappedJson = "{ \"friends\": " + result + "}";
                FriendList friendList = JsonUtility.FromJson<FriendList>(wrappedJson);

                int i = 0;
                User[] userList = new User[friendList.friends.Length];
                foreach(Friend friend in friendList.friends)
                {
                    Debug.Log("id: " + friend.friendId + ", Name: " + friend.friendName);
                    userList[i++] = new User
                    {
                        Id = friend.friendId,
                        Name = friend.friendName
                    };
                }

                callback(userList);
            }
        }
    }

    // ģ������, ��������� ���� Ȯ��
    public IEnumerator FriendCheckRequest(int friendId, System.Action<string> callback)
    {
        int userId = loginManagerScript.loginUserInfo.Id;

        string requestUrl = url + "/friend/profile?userId=" + userId + "&friendId=" + friendId;

        using (UnityWebRequest friendCheckRequest = UnityWebRequest.Get(requestUrl))
        {
            friendCheckRequest.SetRequestHeader("Authorization", loginManagerScript.accessToken);
            yield return friendCheckRequest.SendWebRequest();

            // ��û ���� ��
            if(friendCheckRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(friendCheckRequest.error);
                yield break;
            }
            // ��û ���� ��
            else
            {
                string result = friendCheckRequest.downloadHandler.text;
                //Debug.Log(result);
                callback(result);
            }
        }
    }

    // ģ�� �߰� ��ư Ŭ�� ��
    public IEnumerator addFriend(int friendId)
    {
        int userId = loginManagerScript.loginUserInfo.Id;

        string requestUrl = url + "/friend/add";

        string jsonRequestBody =    "{" +
                                        "\"userId\": " + userId + "," +
                                        "\"friendId\": " + friendId + 
                                    "}";

        //Debug.Log(jsonRequestBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);

        // ��û ����
        using (UnityWebRequest addFriendRequest = new UnityWebRequest(requestUrl, "POST"))
        {
            addFriendRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            addFriendRequest.downloadHandler = new DownloadHandlerBuffer();
            addFriendRequest.SetRequestHeader("Content-Type", "application/json");
            addFriendRequest.SetRequestHeader("Authorization", loginManagerScript.accessToken);

            yield return addFriendRequest.SendWebRequest();

            // ��û ���� ��
            if (addFriendRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(addFriendRequest.error);
                yield break;
            }
            // ��û ���� ��
            else
            {
                setFriendUsers();
                //string result = addFriendRequest.downloadHandler.text;
                //Debug.Log(result);
            }
        }
    }

    // ���� ��ư Ŭ�� ��
    public IEnumerator acceptFriend(int friendId)
    {
        int userId = loginManagerScript.loginUserInfo.Id;

        string requestUrl = url + "/friend/accept";

        // DB���� ���� ��û�� ���: userId, �޴� ���: friendId��
        // �� �������� ���� �� �����ϴ� ���(�α��� �� ���)�� friendId�̰�, �α��� �� ������ userId�� ��������Ƿ�
        // ���� �ٲ㼭 ��û ������ �Ѵ�.
        string jsonRequestBody =    "{" +
                                        "\"userId\": " + friendId + "," +
                                        "\"friendId\": " + userId +
                                    "}";

        //Debug.Log(jsonRequestBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);

        // ��û ����
        using (UnityWebRequest acceptFriendRequest = new UnityWebRequest(requestUrl, "POST"))
        {
            acceptFriendRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            acceptFriendRequest.downloadHandler = new DownloadHandlerBuffer();
            acceptFriendRequest.SetRequestHeader("Content-Type", "application/json");
            acceptFriendRequest.SetRequestHeader("Authorization", loginManagerScript.accessToken);

            yield return acceptFriendRequest.SendWebRequest();

            // ��û ���� ��
            if (acceptFriendRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(acceptFriendRequest.error);
                yield break;
            }
        }
    }

}
