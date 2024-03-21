using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // ���� �̸�
    public string gunName;
    // ���� �Ÿ�
    public float range;
    // ��Ȯ��
    public float accuracy;
    // ���� �ӵ�
    public float fireRate;
    // ������ �ӵ�
    public float reloadTime;

    // ���� ������
    public int damage;

    // �Ѿ� ������ ����
    public int reloadBulletCount;
    // ���� ź������ ���� �ִ� �Ѿ��� ����
    public int currentBulletCount;
    // �ִ� ���� ���� ����
    public int maxBulletCount;
    // ���� ���� �ϰ� �ִ� �Ѿ��� ����
    public int carryBulletCount;

    // �ݵ� ����
    public float retroActionForce;
    // ������ ���� �ݵ� ����
    public float retroActionFineSightForce;

    public Vector3 fineSightOriginPos;

    public Animator anim;

    // �ѱ� ������ ���� ��ƼŬ �ý���
    public ParticleSystem muzzleFlash;

    public AudioClip fire_Sound;
}
