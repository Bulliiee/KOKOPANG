using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class TCPConnectManager : MonoBehaviour
{
    [Header("Chat")]
    public TMP_Text MessageElement;   // ä�� �޽���
    public GameObject ChattingList; // ä�� ����Ʈ
    public TMP_InputField InputText;  // �Է� �޽���


    private TcpClient _tcpClient;
    private NetworkStream _networkStream;
    private StreamReader reader;
    private StreamWriter writer;

    private string hostname = "localhost";
    private int port = 1370;

    private void Start()
    {
        ConnectToServer();
    }

    private void Update()
    {
        // �����Ͱ� ���� ���
        if(_networkStream.DataAvailable)
        {
            string response = ReadMessageFromServer();
            string type = getType(response);
            
            if(type == "chat")  // ä�� �޽���
            {
                showMessage(response);
            }
            else if(type == "channelList")  // ��ü ������ �� ���
            {

            }
            else if(type == "sessionList")  // ��ü ������ ���� ���
            {

            }
            else if(type == "channelSessionList")   // �� ���� ���� ���
            {

            }
        }

        // ä�� �Է� ����
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if(InputText.text != "")
            {
                MessageSendBtnClicked();
            }
        }
    }

    // ============================= ���� ���� ���� =============================
    public void ConnectToServer()
    {
        try
        {
            // TCP ������ ����
            _tcpClient = new TcpClient(hostname, port);
            _networkStream = _tcpClient.GetStream();
            reader = new StreamReader(_networkStream);
            writer = new StreamWriter(_networkStream);

            string json =   "{" +
                "\"channel\":\"lobby\"," +
                "\"userName\":\"��������\"," +
                "\"userId\":0" +
            "}";
            SendMessageToServer(json);

            string response = ReadMessageFromServer();
            Debug.Log(response);
        }
        catch (Exception e)
        {
            // ���� �� ���� �߻� ��
            Debug.Log($"Failed to connect to the server: {e.Message}");
        }
    }

    // response ���� �޽��� Ÿ�� üũ�ϱ�
    private string getType(string response)
    {
        string[] words = response.Split('\"');
        return words[3];
    }

    // ������ �޽��� ������
    public void SendMessageToServer(string message)
    {
        if (_tcpClient == null)
        {
            return;
        }
        writer.WriteLine(message);
        writer.Flush(); // �޽��� ��� ����
    }

    // �������� �޽��� �б�
    public string ReadMessageFromServer()
    {
        if(_tcpClient == null)
        {
            return "";
        }

        try
        {
            // �����κ��� ���� �б�
            string response = reader.ReadLine();
            return response;
        }
        catch(Exception e)
        {
            Debug.Log("���� �б� ����: " + e.Message);
            return "";
        }
    }

    // ============================= ä�� ���� =============================
    // �޽��� ���� ��ư Ŭ�� ��
    public void MessageSendBtnClicked()
    {
        string message = InputText.text;

        string json = "{" +
            "\"channel\":\"lobby\"," +
            "\"userName\":\"��������\"," +
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
    // �� ����Ʈ �ҷ�����
    public void setChannelList()
    {

    }

    // �� �����
    public void createChannel()
    {

    }

    // ��������
    public void quickEnter()
    {

    }

    // �� ����
    public void participate()
    {

    }
    
    // ������ ��ü ���� ��� �޾ƿ���
    public string getConnectedUsers()
    {
        

        return "";
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
