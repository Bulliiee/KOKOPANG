using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ���ǵ� ���� ����
    [SerializeField]
    private float walkSpeed = 5.0f;
    [SerializeField]
    private float runSpeed = 7.0f;

    private float applySpeed;

    [SerializeField]
    private float jumpForce = 7.0f;

    // ���� ����
    private bool isRun = false;
    private bool isGround = true;

    // ĸ�� �ݶ��̴��� �� meshCollider�� �浹 Ȯ��
    private CapsuleCollider capsuleCollider;

    // rigidbody�� ���� Player Control
    private Rigidbody myRigid;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        applySpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        Move();
        RotateToMouseDir();
    }
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }
    private void Jump()
    {
        myRigid.velocity = transform.up * jumpForce;
    }
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }
    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
    }
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }
    private void Move()
    {
        // A, D, Left, Right Ű �Է� => ������ ����Ű : 1, ���� ����Ű : -1, �Է� X : 0 return
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        // W, S, Up, Down Ű �Է� => ���� ����Ű : 1, �Ʒ��� ����Ű : -1, �Է� X : 0 reutrn
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }
    private void RotateToMouseDir()
    {
        // ���� ���콺 �����ǿ��� ���� ���� * 10d���� �̵��� ��ġ�� ���� ��ǥ ���ϱ�
        Vector3 mouseWorldPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

       // Atan2�� �̿��Ͽ� ���̿� �غ�(tan) ���� ����(Radian) ���ϱ�
       // Mathf.Rad2Deg�� ���ؼ� ���� (Radian) ���� ������(Degree)�� ��ȯ
       float angle = Mathf.Atan2(this.transform.position.y - mouseWorldPostion.y, 
           this.transform.position.x - mouseWorldPostion.x) * Mathf.Rad2Deg;

        // angle�� 0 ~ 180�� �����̱� ������ ����
        float final = -(angle + 90f);

        // Y�� ȸ��
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, final, 0f));
    }
}
