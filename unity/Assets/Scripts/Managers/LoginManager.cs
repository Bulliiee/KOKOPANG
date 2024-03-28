using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [Header ("Commons")]
    public GameObject Login;    // �α��� ������Ʈ
    public GameObject SignUpPopUp;  // ȸ������ �˾� ������Ʈ
    public GameObject Lobby; // �κ� ������Ʈ
    public LobbyManager LobbyManagerScript; // �κ�Ŵ��� ��ũ��Ʈ

    [Header("Login")]
    public TMP_InputField LoginEmail;   // �α��� �̸��� ��ǲ�ʵ�
    public TMP_InputField LoginPassword;    // �н����� ��ǲ�ʵ�
    public Button LoginSignUpBtn;   // ȸ������ �˾� ��� ��ư
    public Button LoginBtn; // �α��� ��ư
    public Button LoginExit;    // ���� ��ư

    [Header ("SignUp")]
    public TMP_InputField SignUpEmail;  // ȸ������ �̸��� ��ǲ�ʵ�
    public TMP_InputField SignUpName;   // ȸ������ �̸� ��ǲ�ʵ�
    public TMP_InputField SignUpPassword;   // ȸ������ ��й�ȣ ��ǲ�ʵ�
    public TMP_InputField SignUpPasswordCheck;  // ȸ������ ��й�ȣ ���Է� �ʵ�
    public TMP_Text SignUpFeedback; // ȸ������ ����/���� ���� �ǵ�� �ؽ�Ʈ
    public Button SignupConfirm;    // ȸ������ �ϱ� ��ư
    public Button SignupCancel; // ȸ������ ���� ��ư

    [Header ("LoginUserInfo")]
    public string accessToken; // access token
    public string refreshToken;    // refresh token
    public User loginUserInfo;

    private string url = "http://j10c211.p.ssafy.io:8080";   // ��û URL


    /* ======================== ȸ������ ======================== */
    // ȸ���������� ����
    public void GoSignUp() 
    {
        SignUpPopUp.SetActive(true);
        SignupConfirm.interactable = true;
    }

    // ȸ������ ��ư Ŭ�� ��
    public void SignUpConfirm() 
    {
        string email = SignUpEmail.text;
        string name = SignUpName.text;
        string password = SignUpPassword.text;
        string passwordCheck = SignUpPasswordCheck.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) ||
            string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordCheck)) {
            SignUpFeedback.text = "��� �ʵ带 �Է����ּ���.";
            return;
        }
        else if (password != passwordCheck) {
            SignUpFeedback.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            return;
        }
        else {
            SignUpFeedback.text = "";
        }

        SignupConfirm.interactable = false;
        // ȸ������ ��û ������
        StartCoroutine(SignUpRequest(email, password, name));
    }

    // ȸ������ ��� ��
    public void SignUpCancel() 
    {
        // ȸ������ ��� �� �Է� �ʵ� �� �ǵ�� �ؽ�Ʈ �ʱ�ȭ
        SignUpEmail.text = "";
        SignUpName.text = "";
        SignUpPassword.text = "";
        SignUpPasswordCheck.text = "";
        SignUpFeedback.text = "";

        // �α��� �������� �̵�
        SignUpPopUp.SetActive(false);
        //Login.SetActive(true);
    }

    // ȸ������ ��û ������
    private IEnumerator SignUpRequest(string email, string password, string name)
    {
        string requestUrl = url + "/user/signup";
        User user = new User
        {
            Email = email,
            Password = password,
            Name = name
        };
        string jsonRequestBody = JsonUtility.ToJson(user);
        //Debug.Log("Request Body: " + jsonRequestBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);

        // ��û ����
        using (UnityWebRequest request = new UnityWebRequest(requestUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            // ��û ���� ��
            if (request.result == UnityWebRequest.Result.Success)
            {
                SignUpFeedback.text = "ȸ�������� �Ϸ�Ǿ����ϴ�.";
            }
            // ��û ���� ��
            else
            {
                Debug.LogError("Error: " + request.error);
                // ���̵� �ߺ�
                if (request.responseCode == 409)
                {
                    SignUpFeedback.text = "�ߺ��� ���̵��Դϴ�.";
                }
                // �� ��
                else
                {
                    SignUpFeedback.text = "������ ������ �� �����ϴ�.";
                }
                //string responseBody = request.downloadHandler.text;

                SignupConfirm.interactable = true;
                yield break;
            }

            // �α��� �������� �̵�
            //SignUpCancel();
        }
    }

    /* ======================== �α��� ======================== */
    // �α��ι�ư Ŭ�� ��
    public void LoginConfirm()
    {
        string email = LoginEmail.text;
        string password = LoginPassword.text;

        // �Է� Ȯ��
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return;
        }

        LoginBtn.interactable = false;
        StartCoroutine(LoginRequest(email, password));
    }

    // �α��� ��û ������
    private IEnumerator LoginRequest(string email, string password)
    {
        string requestUrl = url + "/login";

        WWWForm form = new WWWForm();
        form.AddField("username", email);
        form.AddField("password", password);

        // ��û ����
        using (UnityWebRequest request = UnityWebRequest.Post(requestUrl, form))
        {
            yield return request.SendWebRequest();

            // ��û ���� ��
            if (request.result == UnityWebRequest.Result.Success)
            {
                //Debug.Log("�α��� ����");
                accessToken = request.GetResponseHeader("Authorization");
                refreshToken = request.GetResponseHeader("refreshToken");
                //Debug.Log("accessToken: " + accessToken);
                //Debug.Log("refreshToken: " + refreshToken);

                // �α��� �� ���� ���� �ҷ�����
                using (UnityWebRequest userProfileRequest = UnityWebRequest.Get(url + "/user/profile?email=" + email))
                {
                    userProfileRequest.SetRequestHeader("Authorization", accessToken);
                    yield return userProfileRequest.SendWebRequest();

                    // ���� ��û ���� ��
                    if(userProfileRequest.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(userProfileRequest.error);
                    }
                    // ���� ��û ���� ��
                    else
                    {
                        //Debug.Log(userProfileRequest.downloadHandler.text);
                        loginUserInfo = JsonUtility.FromJson<User>(userProfileRequest.downloadHandler.text);

                        // �κ� ������ �ʱ�ȭ ȣ��
                        LobbyManagerScript.LobbyInit(loginUserInfo.Name, loginUserInfo.Id);
                        //Debug.Log(userProfile.Id);
                    }
                }

                Login.SetActive(false);
                Lobby.SetActive(true);
            }
            // ��û ���� ��
            else
            {
                Debug.LogError("Error: " + request.error);
                // ���̵� �ߺ�
                if (request.responseCode == 400)
                {
                    Debug.Log("�α��� ����");
                }
                // �� ��
                else
                {
                    Debug.Log("������ ������ �� �����ϴ�.");
                }

                LoginBtn.interactable = true;
                yield break;
            }

        }
    }

}
