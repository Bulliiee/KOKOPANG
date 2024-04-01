using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class TCPConnectManager : MonoBehaviour
{
    [Header("Commons")]
    public GameObject LobbyScene;
    public GameObject ChannelScene;
    public LobbyManager lobbyManagerScript;
    public LoginManager loginManagerScript;
    public ChannelManager channelManagerScript;

    [Header("Chat")]
    public TMP_Text MessageElement;   // ä�� �޽���
    public GameObject ChattingList; // ä�� ����Ʈ
    public TMP_InputField InputText;  // �Է� �޽���

    [Header("Channel")]
    public GameObject ScrollViewChannelList;    // ä�� ����Ʈ
    public GameObject ChannelListElement;       // ä�� ����Ʈ �׸�
    public ChannelListElement SelectedChannel;  // ���õ� ä�� ����Ʈ



    [Header("Connect")]
    private TcpClient _tcpClient;
    private NetworkStream _networkStream;
    private StreamReader reader;
    private StreamWriter writer;
    private User loginUserInfo;

    private string hostname = "j10c211.p.ssafy.io";
    //private string hostname = "172.30.1.4";
    private int port = 1370;

    private void Awake()
    {
        // TODO: ���� ����
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        // TCP ù ���� �Ŀ��� ��û 2�� �޾Ƽ� channel, session 2���� ����� �޾ƿ´�.
        ConnectToServer();
    }

    private void OnDisable()
    {
        // ä�� ���� �����


        // ���Ӱ��� �޸� ����
        OnApplicationQuit();
    }

    private void Update()
    {
        // �����Ͱ� ���� ���
        while (_networkStream != null && _networkStream.DataAvailable)
        {
            string response = ReadMessageFromServer();
            DispatchResponse(response);
        }
        //if (_networkStream.DataAvailable)
        //{
        //    string response = ReadMessageFromServer();
        //    DispatchResponse(response);
        //}

        // ä�� �Է� ����
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if(InputText.text != "")
            {
                MessageSendBtnClicked();
            }
        }
    }

    // ��û �й��ϱ�
    private void DispatchResponse(string response)
    {
        string type = getType(response);

        //Debug.Log("response: " + response);
        //Debug.Log("type: " + type);

        Debug.Log($"type: {type}");

        if (type == "chat")  // ä�� �޽���
        {
            showMessage(response);
        }
        else if (type == "channelList")  // ��ü ������ �� ���
        {
            setChannelList(response);
        }
        else if (type == "sessionList")  // ��ü ������ ���� ���
        {
            lobbyManagerScript.setAllUsers(response);
        }
        else if (type == "channelSessionList")   // �� ���� ���� ���
        {
            setChannelSessionList(response);
            channelManagerScript.showSessionList();
        }
        else if(type == "channelInfo")  // ä�� ����
        {
            setChannelInfo(response);
            channelManagerScript.showSessionList();
        }
        else
        {
            Debug.Log("Response ELSE!!!");
        }
    }

    // response ���� �޽��� Ÿ�� üũ�ϱ�
    private string getType(string response)
    {
        string[] words = response.Split('\"');
        return words[3];
    }

    // ============================= ���� ���� ���� =============================
    private void ConnectToServer()
    {
        try
        {
            // TCP ������ ����
            _tcpClient = new TcpClient(hostname, port);
            _networkStream = _tcpClient.GetStream();
            reader = new StreamReader(_networkStream);
            writer = new StreamWriter(_networkStream);

            loginUserInfo = loginManagerScript.loginUserInfo;
            Debug.Log("ConnectToServer");

            string json = "{" +
                    "\"channel\":\"lobby\"," +
                    $"\"userName\":\"{loginUserInfo.Name}\"," +
                    "\"data\":{" +
                        "\"type\":\"initial\"," +
                        $"\"userId\":\"{loginUserInfo.UserId}\"" +
                    "}" +
                "}";
            SendMessageToServer(json);

            // channel, session ��� �޾ƿ���
            // TCP ù ���� �Ŀ��� ��û 2�� �޾Ƽ� channel, session 2���� ����� �޾ƿ´�.
            //string response = ReadMessageFromServer();
            //DispatchResponse(response);

            //string response = ReadMessageFromServer();
            //Debug.Log(response);
        }
        catch (Exception e)
        {
            // ���� �� ���� �߻� ��
            Debug.Log($"Failed to connect to the server: {e.Message}");
        }
    }

    // ������ �޽��� ������
    private void SendMessageToServer(string message)
    {
        if (_tcpClient == null)
        {
            return;
        }
        writer.WriteLine(message);
        writer.Flush(); // �޽��� ��� ����
    }

    // �������� �޽��� �б�
    private string ReadMessageFromServer()
    {
        if(_tcpClient == null)
        {
            return null;
        }

        try
        {
            // �����κ��� ���� �б�
            //string response = reader.ReadLine();
            //return response;

            //StringBuilder message = new StringBuilder();
            //while (_networkStream.CanRead)
            //{
            //    int readByte = _networkStream.ReadByte();
            //    if (readByte == -1 || readByte == '\n') // '\n'�� �����ڷ� ���
            //    {
            //        break;
            //    }
            //    message.Append((char)readByte);
            //}
            //return message.ToString();

            //StringBuilder message = new StringBuilder();
            //using (BinaryReader reader = new BinaryReader(_networkStream, Encoding.UTF8))
            //{
            //    while (_networkStream.DataAvailable)
            //    {
            //        char readChar = reader.ReadChar();
            //        if (readChar == '\n') // '\n'�� �����ڷ� ���
            //        {
            //            break;
            //        }
            //        message.Append(readChar);
            //    }
            //}
            //return message.ToString();

            StringBuilder message = new StringBuilder();
            BinaryReader reader = new BinaryReader(_networkStream, Encoding.UTF8);
            while (_networkStream.DataAvailable)
            {
                char readChar = reader.ReadChar();
                if (readChar == '\n') // '\n'�� �����ڷ� ���
                {
                    break;
                }
                message.Append(readChar);
            }
            return message.ToString();
        }
        catch(Exception e)
        {
            Debug.Log("���� �б� ����: " + e.Message);
            return null;
        }
    }

    // ============================= ä�� ���� =============================
    // �޽��� ���� ��ư Ŭ�� ��
    public void MessageSendBtnClicked()
    {
        string message = InputText.text;

        if (message == "")
        {
            return;
        }

        string json = "{" +
            "\"channel\":\"lobby\"," +
            $"\"userName\":\"{loginUserInfo.Name}\"," +
            "\"data\":{" +
                "\"type\":\"chat\"," +
                "\"message\":" + "\"" + message + "\"" +
            "}" +
        "}";

        InputText.text = "";

        SendMessageToServer(json);
        InputText.Select();
        InputText.ActivateInputField();
    }


    // �޽��� ������ ��
    // TODO: �޽��� ������Ʈ Ǯ�� �����ϱ�
    public void showMessage(string message)
    {
        // ���� �θ� ������Ʈ
        Transform content = ChattingList.transform.Find("Viewport/Content");

        ChatMessage chatMessage = JsonUtility.FromJson<ChatMessage>(message);

        TMP_Text temp1 = Instantiate(MessageElement);
        temp1.text = chatMessage.UserName + ": " + chatMessage.Message;
        temp1.transform.SetParent(content, false);

        // 20�� �Ѿ�� ä�� ���������� �����
        // TODO: ������Ʈ Ǯ��
        if(content.childCount >= 20)
        {
            Destroy(content.GetChild(1).gameObject);
        }

        StartCoroutine(ScrollToBottom());
    }

    // ��ũ�� �� �Ʒ��� ������
    IEnumerator ScrollToBottom()
    {
        // ���� ������ ��ٸ�
        yield return null;

        Transform content = ChattingList.transform.Find("Viewport/Content");

        // Layout Group�� ������ ��� ������Ʈ
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content);

        // ��ũ�� �� �Ʒ��� ����
        ChattingList.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }

    // ============================= ��(channel), ����(session) ���� =============================
    [Serializable]
    class ChannelList
    {
        public string type;
        public ChannelInfo[] data;
    }
    [Serializable]
    class ChannelInfo
    {
        public int channelIndex;
        public string channelName;
        public int cnt;
        public bool isOnGame;
    }
    // �� ����Ʈ �ҷ�����
    public void setChannelList(string response)
    {
        //Debug.Log("���� setChannelList");
        // ä�� ����Ʈ �� ��ũ�Ѻ�(������ �θ�)
        Transform content = ScrollViewChannelList.transform.Find("Viewport/Content");
        // ���� ����Ʈ ����
        // TODO: ������Ʈ Ǯ��
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // JSON �Ľ�
        ChannelList channelList = JsonUtility.FromJson<ChannelList>(response);

        for(int i = 0; i < channelList.data.Length; i++)
        {
            // ���� �ο� ������ �Ⱥ��̰� �ϱ�
            if(channelList.data[i].isOnGame)
            {
                continue;
            }

            //Debug.Log("���̸�: " + channelList.data[i].channelName);
            // ������ �����
            GameObject channelListElement = Instantiate(ChannelListElement);
            ChannelListElement channelListElementScript = channelListElement.GetComponent<ChannelListElement>();
            channelListElementScript.ChannelIndex = channelList.data[i].channelIndex;
            channelListElementScript.ChannelName = channelList.data[i].channelName;
            channelListElementScript.Cnt = channelList.data[i].cnt;
            channelListElementScript.IsOnGame = channelList.data[i].isOnGame;

            // �θ� ���̱�
            channelListElement.transform.SetParent(content, false);
        }
    }

    // �� �����
    public void createChannel(string json)
    {
        //Debug.Log("ä�� �����: " + json);
        SendMessageToServer(json);

        // ������ �޾ƿ���
        //string response = ReadMessageFromServer();
        //DispatchResponse(response);

        LobbyScene.SetActive(false);
        ChannelScene.SetActive(true);

        channelManagerScript.gameObject.SetActive(true);
    }

    // �� ���� ����
    public void setChannelInfo(string response)
    {
        Debug.Log(response);

        // JSON �Ľ�
        ChannelList channelList = JsonUtility.FromJson<ChannelList>(response);

        DataManager.Instance.channelIndex = channelList.data[0].channelIndex;
        DataManager.Instance.channelName = channelList.data[0].channelName;
        DataManager.Instance.cnt = channelList.data[0].cnt;
        DataManager.Instance.isOnGame = channelList.data[0].isOnGame;

        // ���ӽ���
        if (channelList.data[0].isOnGame)
        {
            SceneManager.LoadScene("Game");
        }
        // ä�� ������(���ӽ���x)
        else
        {
            Debug.Log(channelList.data[0].channelName);
            // ä�� ���� ����
            channelManagerScript.channelIndex = channelList.data[0].channelIndex;
            channelManagerScript.channelName = channelList.data[0].channelName;
            channelManagerScript.cnt = channelList.data[0].cnt;
            channelManagerScript.isOnGame = channelList.data[0].isOnGame;
        }
    }

    // ��������
    public void quickEnter()
    {

    }

    // �� ���� ��
    public void selectChannelElement(ChannelListElement channelListElementScript)
    {
        // ������ ���õ� �� ���� ����
        if(SelectedChannel != null)
        {
            SelectedChannel.transform.Find("Border").GetComponent<Image>().color = Color.white;
        }

        // ���õ� ä�� ����
        SelectedChannel = channelListElementScript;
        SelectedChannel.transform.Find("Border").GetComponent<Image>().color = new Color(1f, 0.5707547f, 0.5707547f, 1f);

        //Debug.Log($"{SelectedChannel.channelIndex}, {SelectedChannel.channelName}, {SelectedChannel.cnt}, {SelectedChannel.isOnGame}");
    }

    // �� ���� ��
    public void joinChannel()
    {
        string json = "{" +
            "\"channel\":\"channel\"," +
            $"\"userName\":\"{loginUserInfo.Name}\"," +
            "\"data\": {" +
                "\"type\":\"join\"," +
                $"\"channelIndex\":\"{SelectedChannel.channelIndex}\"" +
            "}" +
        "}";

        //Debug.Log(json);

        SendMessageToServer(json);

        // ä�� ���� ����
        channelManagerScript.channelIndex = SelectedChannel.channelIndex;
        channelManagerScript.channelName = SelectedChannel.channelName;
        channelManagerScript.cnt = SelectedChannel.cnt + 1;
        channelManagerScript.isOnGame = SelectedChannel.isOnGame;

        LobbyScene.SetActive(false);
        ChannelScene.SetActive(true);
        channelManagerScript.gameObject.SetActive(true);
    }

    [Serializable]
    class ChannelSessionList
    {
        public string type;
        public SessionData[] data;
    }
    // ä�ο� ������ ���� ����Ʈ
    public void setChannelSessionList(string response)
    {
        //Debug.Log(response);
        ChannelSessionList channelSessionList = JsonUtility.FromJson<ChannelSessionList>(response);

        channelManagerScript.sessionList.Clear();
        foreach(SessionData d in channelSessionList.data)
        {
            channelManagerScript.sessionList.Add(d);
        }

        channelManagerScript.cnt = channelSessionList.data.Length;
    }

    // ���� ��ư Ŭ�� ��
    public void readyChannel(string json)
    {
        SendMessageToServer(json);
    }

    // �� ������ ��ư Ŭ�� ��
    public void leaveChannel(string json)
    {
        SendMessageToServer(json);

        ChannelScene.SetActive(false);
        LobbyScene.SetActive(true);
    }

    // ���� ���� ��ư Ŭ�� ��
    public void startGame(string json)
    {
        SendMessageToServer(json);
        SceneManager.LoadScene("Game");
    }
    
    [Serializable]
    class SessionResponse
    {
        public string type;
        public List<SessionData> data;
    }
    // ������ ��ü ���� ��� �޾ƿ���
    public User[] getConnectedUsers(string response)
    {
        //Debug.Log("GetAllConnectedUsers");
        //Debug.Log(response);
        SessionResponse sessionResponse = JsonUtility.FromJson<SessionResponse>(response);

        User[] userList = new User[sessionResponse.data.Count];
     
        int i = 0;
        foreach (SessionData sessinData in sessionResponse.data)
        {
            userList[i] = new User();
            userList[i].Name = sessinData.UserName;
            userList[i].UserId = sessinData.UserId;
            //Debug.Log(userList[i].Name + ", " + userList[i].UserId + ", " + response);
            i++;
        }

        return userList;
    }


    // ============================= ���� ���� =============================
    // ���� ��
    private void OnApplicationQuit()
    {
        if (_tcpClient != null)
        {
            DisconnectFromServer();
        }
    }

    public void DisconnectFromServer()
    {
        // ���� ����
        _networkStream.Close();
        _tcpClient.Close();
    }
}
