using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMent : MonoBehaviour
{

    // �ڵ� �帧�� ���� -> �ʱ�ȭ-> ȣ��

    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<T> : �ڽ��� T(Ÿ��) ������Ʈ�� ������ �´�.
        rigid = GetComponent<Rigidbody>();
        // AddForce(Vec) : Vec �� ����� ũ��� ���� ��.
        //rigid.AddForce(Vector3.up * 50, ForceMode.Impulse);




        //velocity => ���� �̵��ӵ� ==> vector ���� ������
        // �̰� ������ �������� �ӷ��� ������
        //rigid.velocity = Vector3.right;


    }

    // Update is called once per frame
    void Update()
    {


        Vector3 vec = new Vector3(
            Input.GetAxisRaw("Horizontal") * Time.deltaTime,
            Input.GetAxisRaw("Jump") * Time.deltaTime,
            Input.GetAxisRaw("Vertical") * Time.deltaTime
            );
        transform.Translate(vec);



    }

    // ��ü�� ���鶧 ����Ǿ�� �� �ʼ� ��� 
    //Mesh,Material, Collider, RigidBody 
    //Friction => �������


    // ���������� �����ǰ� �ޱ� ���ؼ��� FixedUpdate �� �����ִ°� ����
    private void FixedUpdate()
    {
       
        //if (Input.GetButtonDown("Jump"))
        //{
        //    rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
        //}
        //Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"),
        //    0, Input.GetAxisRaw("Vertical"));

        //rigid.AddForce(vec * Time.deltaTime, ForceMode.Impulse);

        ////#3.ȸ���� 
        ////rigid.AddTorque(Vector3.down);

    }
}
