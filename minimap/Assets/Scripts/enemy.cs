using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    // �� �ֺ��� ���� ã�� �˰��� ����
    [SerializeField] GameObject player;
    //Unity ���� ������Ʈ�� �׷�ȭ �ϰ� ������ �����ϰ� ��
    [SerializeField] LayerMask layer;
    // �ֺ��� ������ ���� ���� ����
    [SerializeField] float radius;
    // ���� ������ �ȿ� �浹�ϴ°� �ִ��� Ȯ��
    [SerializeField] Collider[] col;

    [SerializeField] Transform target;



    [Header("������")]
    [SerializeField] GameObject Cenemy;
    [SerializeField] Transform[] Menemy;
    [SerializeField] float creatTime;


    [Header("ī��Ʈ")]
    public int Count;
    [SerializeField] Text TextCount;
    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("EnemyAround", 0, 0.2f);
        InvokeRepeating("EnemyCreate", 0, creatTime);
    }

    // �ֺ��� �ִ� ���� ã�°�
    void EnemyAround()
    {
        col = Physics.OverlapSphere(player.transform.position, radius, layer);
        Transform minenemy = null;

        if (col.Length > 0)
        {
            float minDistance = Mathf.Infinity;

            foreach (Collider mCol in col)
            {
                float playerToDistance = Vector3.SqrMagnitude(player.transform.position - mCol.transform.position);
                if (playerToDistance < minDistance)
                {
                    minDistance = playerToDistance;
                    minenemy = mCol.transform;
                }
            }
        }
        target = minenemy;
    }





    void EnemyCreate()
    {
        int i = Random.Range(0,Menemy.Length);


        Instantiate(Cenemy, Menemy[i].position, Menemy[i].rotation);


    }

    private void Update()
    {
        // �ֺ��� Ÿ���� �ִ��� ������ Ȯ��
        if (target == null)
        // Ÿ���� ������ ���� ���� ���ư�
        {
            player.transform.Rotate(new Vector3(0, 60, 0) * Time.deltaTime);
        }
        else
        {
            // ���� ������ ���� �ִ� �������� ���� ����.
            Quaternion dir = Quaternion.LookRotation(target.position - player.transform.position);
           // ���ư��ٰ� �� ���� �ִ� �������� ȸ����
            Vector3 angle = Quaternion.RotateTowards(player.transform.rotation, dir, 200 * Time.deltaTime).eulerAngles;
            // 
            player.transform.rotation = Quaternion.Euler(0, angle.y, 0);
        
        }
    }




}
