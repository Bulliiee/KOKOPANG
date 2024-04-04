using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject otherPlayerPrefab;
    public Transform[] spawnPoints;

    private int myIdx;

    void Start()
    {
        checkLoginUser();

        SpawnPlayer();
    }

    // ���� �����ϱ�
    void SpawnPlayer()
    {
        if(myIdx == -1)
        {
            return;
        }

        // �����͸Ŵ����� �÷��̾� ���ӿ�����Ʈ �ֱ�
        DataManager.Instance.players.Clear();
        // �����͸Ŵ����� �÷��̾� �� �Ŵ��� �ֱ�
        //DataManager.Instance.weaponManagerScript.Clear();
        for(int i = 0; i < DataManager.Instance.cnt; i++)
        {
            GameObject player;
            if(myIdx == i)
            {
                player = Instantiate(playerPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
                player.transform.Find("Main Camera/Holder").GetComponent<ArmsControl>().isMine = true;
                player.transform.Find("Main Camera/Holder").GetComponent<AxeController>().isMine = true;
                player.transform.Find("Main Camera/Holder").GetComponent<PickAxeController>().isMine = true;
            }
            else
            {
                player = Instantiate(otherPlayerPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
                player.transform.Find("HPCanvas/PlayerNameText").GetComponent<TMPro.TMP_Text>().text = DataManager.Instance.sessionList[i].UserName;
            }
            DataManager.Instance.players.Add(player);
            //DataManager.Instance.weaponManagerScript.Add(player.transform.Find("Main Camera/Holder").GetComponent<WeaponManager>());
        }


        //int spawnIndex = Random.Range(0, spawnPoints.Length);
        //Transform spawnPoint = spawnPoints[spawnIndex];

        //GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);


    }

    // ���� ��� �� �α��� �� ���� �ε��� ã��
    void checkLoginUser()
    {
        for(int i = 0; i < DataManager.Instance.cnt; i++)
        {
            // ���� ��� �� �α��� �� ������ id�� ������
            if(DataManager.Instance.sessionList[i].UserId == DataManager.Instance.loginUserInfo.UserId)
            {
                myIdx = i;
                DataManager.Instance.myIdx = myIdx;
                return;
            }
        }

        myIdx = -1;
    }
}
