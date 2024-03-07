using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5.0f;
    [SerializeField]
    float rotateSpeed = 10.0f;

    Vector3 moveDirection;

    private CharacterController characterController;
    private Animator animator;

    private void Awake()
    {
        // CharacterController�� ���� Move
        characterController = GetComponent<CharacterController>();
        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // left, right, a, d Ű �Է�
        float h = Input.GetAxisRaw("Horizontal");
        // up, down, w, s Ű �Է�
        float v = Input.GetAxisRaw("Vertical");

        // �̵� ������ Ű �Է¿� ���� ����
        moveDirection = new Vector3(h, 0, v);

        // �ٶ󺸴� �������� ȸ�� �� �ٽ� ������ �ٶ󺸴� ������ ���� ���� ����
        if (!(h == 0 && v == 0))
        {
            // �̵��� ȸ���� �Բ� ó��
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            // ȸ���ϴ� �κ�, Point 1.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotateSpeed * Time.deltaTime);
        }

    }
}
