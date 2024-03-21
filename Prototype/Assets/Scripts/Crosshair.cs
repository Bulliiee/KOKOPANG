using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    // ũ�ν� ��� ��Ȱ��ȭ�� ���� �θ� ��ü
    [SerializeField]
    private GameObject go_CrosshairHUD;

    public void WalkingAnimation(bool _flag)
    {
        animator.SetBool("Walking", _flag);
    }

    public void RunningAnimation(bool _flag)
    {
        animator.SetBool("Running", _flag);
    }
}
