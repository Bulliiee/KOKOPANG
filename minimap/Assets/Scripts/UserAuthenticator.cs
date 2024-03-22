using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UserAuthenticator : MonoBehaviour
{
    public InputField IdInputField;
    public InputField IdCheckInputField;
    public InputField PWInputField;
    public InputField PWCheckInputField;
    public Text FeedbackText;
    public Button YesButton;
    public Button NoButton;
    public Button LoginButton;

    private string Url = "http://localhost:3000/";

    public void Start()
    {
        if (YesButton != null)
        {
            YesButton.onClick.AddListener(signUp);
        }
        if (NoButton != null)
        {
            NoButton.onClick.AddListener(CancelSignUp);
        }
        if (LoginButton != null)
        {
            LoginButton.onClick.AddListener(() => Login());
        }
    }

    public void signUp()
    {
        string id = IdInputField.text;
        string idCheck = IdCheckInputField.text;
        string password = PWInputField.text;
        string pwCheck = PWCheckInputField.text;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(idCheck) ||
            string.IsNullOrEmpty(password) || string.IsNullOrEmpty(pwCheck))
        {
            FeedbackText.text = "��� �ʵ带 �Է����ּ���.";
            return;
        }

        if (id != idCheck)
        {
            FeedbackText.text = "ID�� ��ġ���� �ʽ��ϴ�.";
            return;
        }

        if (password != pwCheck)
        {
            FeedbackText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            return;
        }

        StartCoroutine(Register(id, password));
    }

    public void CancelSignUp()
    {
        // ȸ������ ��� �� �Է� �ʵ� �� �ǵ�� �ؽ�Ʈ �ʱ�ȭ
        IdInputField.text = "";
        IdCheckInputField.text = "";
        PWInputField.text = "";
        PWCheckInputField.text = "";
        FeedbackText.text = "";
    }

    public IEnumerator Register(string Id, string password)
    {
        string url = Url + "register";
        string jsonRequestBody = "{\"Id\":\"" + Id + "\",\"password\":\"" + password + "\"}";
        UnityWebRequest request = UnityWebRequest.Post(url, jsonRequestBody);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
            FeedbackText.text = "������ ������ �� �����ϴ�.";
            yield break;
        }

        string responseBody = request.downloadHandler.text;
        Debug.Log("Response: " + responseBody);
        FeedbackText.text = "ȸ�������� �Ϸ�Ǿ����ϴ�.";
    }

    public void Login()
    {
        SceneManager.LoadScene("ChatRoom");
        StartCoroutine(LoginRequest(IdInputField.text, PWInputField.text));
    }

    private IEnumerator LoginRequest(string username, string password)
    {
        string url = Url + "login";
        string jsonRequestBody = "{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}";
        UnityWebRequest request = UnityWebRequest.Post(url, jsonRequestBody);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
            FeedbackText.text = "������ ������ �� �����ϴ�.";
            yield break;
        }

        string responseBody = request.downloadHandler.text;
        Debug.Log("Response: " + responseBody);
        FeedbackText.text = "�α��� ����!";
    }

    public void LogOut()
    {
        StartCoroutine(LogoutRequest());
    }

    private IEnumerator LogoutRequest()
    {
        string url = Url + "logout";
        using (UnityWebRequest request = UnityWebRequest.Get(url)) // UnityWebRequest�� using ������� ���μ� Dispose() ȣ��
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
                FeedbackText.text = "�α׾ƿ� �� ������ �߻��߽��ϴ�.";
                yield break;
            }

            PlayerPrefs.DeleteKey("AuthToken");
            FeedbackText.text = "�α׾ƿ� �Ǿ����ϴ�.";
        }
    }
}
